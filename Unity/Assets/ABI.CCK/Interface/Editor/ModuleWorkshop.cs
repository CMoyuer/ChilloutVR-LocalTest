using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using Abi.Newtonsoft.Json;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace ABI.CCK.Interface
{
    
    public class ModuleWorkshop : EditorWindow
    {
        private static VisualTreeAsset tree;
        private static VisualElement row;

        private static List<ListObj> modules = new List<ListObj>();
        private static List<ListObj> installedModules = new List<ListObj>();
        
        private static ListRequest Request;
        private static List<string> InstalledPackages = new List<string>();

        private static ConcurrentDictionary<Guid, bool> SearchAction = new ConcurrentDictionary<Guid, bool>();
        
        private static bool loading = false;
        
        [MenuItem ("Alpha Blend Interactive/Module Workshop", false, 500)]
        public static void  ShowWindow () {
            EditorWindow w = EditorWindow.GetWindow(typeof(ModuleWorkshop), false, "CCK :: Module Workshop");

            w.maxSize = new Vector2(800, 600);
            w.minSize = w.maxSize;
            
            ((ModuleWorkshop) w).Configure(w);
        }

        public void Configure(EditorWindow w)
        {
            if (tree == null) tree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/ABI.CCK/Interface/module-workshop.uxml");
            row = tree.CloneTree();
            w.rootVisualElement.Add(row);

            row.Q(className: "obj-list").Clear();
            
            row.Q<Button>("RefreshList").clicked += GetList;
            row.Q<ToolbarSearchField>("Search").RegisterValueChangedCallback(SearchValueChanged);
            ReadInstalledModules();
            GetList();
        }
    
        private void OnEnable()
        {
            foreach (EditorWindow w in Resources.FindObjectsOfTypeAll<EditorWindow>())
            {
                if (w.titleContent.text == "CCK :: Module Workshop")
                {
                    ((ModuleWorkshop) w).Configure(w);
                }
            }
            Request = Client.List();
            EditorApplication.update += WriteInstalledPackageManagerPackages;
        }

        private void WriteInstalledPackageManagerPackages()
        {
            if (Request.IsCompleted)
            {
                if (Request.Status == StatusCode.Success)
                    foreach (var package in Request.Result)
                        InstalledPackages.Add(package.name);
                else if (Request.Status >= StatusCode.Failure)
                    Debug.Log(Request.Error.message);

                EditorApplication.update -= WriteInstalledPackageManagerPackages;
            }
        }

        private void OnDestroy()
        {
            try
            {
                row.Q<Button>("RefreshList").clicked -= GetList;
                row.Q<ToolbarSearchField>("Search").UnregisterValueChangedCallback(SearchValueChanged);
            } catch {}
        }

        private static void SearchValueChanged(ChangeEvent<string> evt)
        {
            foreach (var key in SearchAction.Keys)
            {
                SearchAction.TryUpdate(key, false, SearchAction[key]);
            }
            FilterList(evt.newValue);
        }

        private static VisualElement GetListObject(string id, string name, string author, string description, string currentVersion, string fileSize)
        {
            VisualElement e = tree.CloneTree().Q(className: "gui-object");
            
            e.name = Guid.NewGuid().ToString();
            e.Q<Label>("ObjectName").text = name;
            e.Q<Label>("Author").text = $"By {author}";
            e.Q<Label>("Description").text = description;
            e.Q<Label>("Version").text = $"Current: {currentVersion}";
            e.Q<Label>("ObjectSize").text = fileSize;
            
            return e;
        }

        private static async void FilterList(string filter)
        {
            Guid g = Guid.NewGuid();
            SearchAction.TryAdd(g, true);
            if (string.IsNullOrEmpty(filter))
            {
                GetList();
                return;
            }
            List<ListObj> filteredObj = modules.FindAll(x => x.ObjectName.ToLower().Contains(filter.ToLower()) || x.ObjectDescription.ToLower().Contains(filter.ToLower()) || x.ObjectAuthor.ToLower().Contains(filter.ToLower()));
            row.Q(className: "obj-list").Clear();
            if (filteredObj.Count > 0)
            {
                foreach (var listObj in filteredObj)
                {
                    VisualElement e = GetListObject(listObj.ObjectId, listObj.ObjectName, listObj.ObjectAuthor, listObj.ObjectDescription, listObj.ObjectVersion, listObj.ObjectFileSize);
                    ListObj obj = installedModules.Find(x => x.ObjectId == listObj.ObjectId);
                    bool installed = obj != null;
                    e.name = listObj.ObjectId;

                    Button install = e.Q<Button>("Install");
                    Button uninstall = e.Q<Button>("Uninstall");
                    Button update = e.Q<Button>("Update");

                    using (UnityWebRequest uwr = UnityWebRequest.Get($"https://files.abidata.io/platform_workshop/cck/{listObj.ObjectId}.png"))
                    {
                        uwr.downloadHandler = new DownloadHandlerTexture();
                        await uwr.SendWebRequest();
                        e.Q("AssetImage").style.backgroundImage = new StyleBackground(DownloadHandlerTexture.GetContent(uwr));
                    }

                    if (installed)
                    {
                        install.SetEnabled(false);
                        install.SendToBack();
                        update.clicked += () => DownloadModule(listObj);
                        uninstall.clicked += () => UninstallModule(listObj);
                        if (obj.ObjectVersion == listObj.ObjectVersion)  update.SetEnabled(false);
                        e.Q<Label>("Version").text = $"Installed: {obj.ObjectVersion} | Current: {listObj.ObjectVersion}";
                    }
                    else
                    {
                        install.clicked += () => DownloadModule(listObj);
                        uninstall.SetEnabled(false);
                        update.SetEnabled(false);
                    }

                    SearchAction.TryGetValue(g, out bool allowedToAdd);
                    if (!allowedToAdd) return;
                    row.Q(className: "obj-list").Add(e);
                }
            }

            SearchAction.TryRemove(g, out bool s);
        }

        private static async void GetList()
        {
            if (loading) return;
            loading = true;
            
            row.Q(className: "obj-list").Clear();
            modules.Clear();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = await client.GetAsync("https://gateway.abi.network/v1/IWorkshop/GetAvailableUnityExtensions");
                var result = JsonConvert.DeserializeObject<List<ListObj>>(await res.Content.ReadAsStringAsync());
                if (result == null) return;
                foreach (var listObj in result)
                {
                    ListObj obj = installedModules.Find(x => x.ObjectId == listObj.ObjectId);
                    if (CheckIfInstalled(listObj.ObjectId) && obj == null)
                    {
                        installedModules.Add(listObj);
                        WriteInstalledModules();
                    }
                    VisualElement e = GetListObject(listObj.ObjectId, listObj.ObjectName, listObj.ObjectAuthor, listObj.ObjectDescription, listObj.ObjectVersion, listObj.ObjectFileSize);
                    bool installed = obj != null;
                    e.name = listObj.ObjectId;

                    Button install = e.Q<Button>("Install");
                    Button uninstall = e.Q<Button>("Uninstall");
                    Button update = e.Q<Button>("Update");

                    using (UnityWebRequest uwr = UnityWebRequest.Get($"https://files.abidata.io/platform_workshop/cck/{listObj.ObjectId}.png"))
                    {
                        uwr.downloadHandler = new DownloadHandlerTexture();
                        await uwr.SendWebRequest();
                        e.Q("AssetImage").style.backgroundImage = new StyleBackground(DownloadHandlerTexture.GetContent(uwr));
                    }

                    if (installed)
                    {
                        install.SetEnabled(false);
                        install.SendToBack();
                        update.clicked += () => DownloadModule(listObj);
                        uninstall.clicked += () => UninstallModule(listObj);
                        if (obj.ObjectVersion == listObj.ObjectVersion)  update.SetEnabled(false);
                        e.Q<Label>("Version").text = $"Installed: {obj.ObjectVersion} | Current: {listObj.ObjectVersion}";
                    }
                    else
                    {
                        install.clicked += () => DownloadModule(listObj, true);
                        uninstall.SetEnabled(false);
                        update.SetEnabled(false);
                    }

                    row.Q(className: "obj-list").Add(e);
                    modules.Add(listObj);
                }
            }

            loading = false;
        }
    
        private static async void DownloadModule(ListObj module, bool installed = false)
        {
            string msg1 = string.Empty;
            string msg2 = string.Empty;
            string msg3 = string.Empty;
            if (!CheckDependenciesGeneric(module.ObjectDependenciesTypes, out msg1) ||
                !CheckDependenciesModule(module.ObjectDependenciesModules, out msg2) ||
                !CheckDependenciesPackage(module.ObjectDependenciesPackages, out msg3))
            {
                string message = $"{CCKLocalizationProvider.GetLocalizedText("ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_WARNING_PREFACE")}:\n\n";

                if (!string.IsNullOrEmpty(msg1)) message += msg1;
                if (!string.IsNullOrEmpty(msg2)) message += msg2;
                if (!string.IsNullOrEmpty(msg3)) message += msg3;
                
                message += $"\n{CCKLocalizationProvider.GetLocalizedText("ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_FINAL_WARNING")}";
                EditorUtility.DisplayDialog(CCKLocalizationProvider.GetLocalizedText("ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_TITLE"),message, CCKLocalizationProvider.GetLocalizedText("ABI_UI_MODULE_WORKSHOP_MISSING_DEPENDENCIES_DIALOG_ACCEPT"));
                return;
            }
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = await client.GetAsync($"https://files.abidata.io/platform_workshop/cck/{module.ObjectId}/{module.ObjectFileName}");
                string path = $"{Application.persistentDataPath}/{module.ObjectFileName}";
                var output = await res.Content.ReadAsByteArrayAsync();
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    await fs.WriteAsync(output, 0, output.Length);
                }
                if (installed) installedModules.RemoveAll(x => x.ObjectId == module.ObjectId);
                if ((ModuleUpdateStrategy) module.ObjectUpdateStrategy == ModuleUpdateStrategy.DeleteAndReImport)
                {
                    if (Directory.Exists($"Assets/ABI.MODS/{module.ObjectId}")) Directory.Delete($"Assets/ABI.MODS/{module.ObjectId}", true);
                }
                installedModules.Add(module);
                WriteInstalledModules();
                AssetDatabase.ImportPackage(path, false);
            }
        }

        private static bool CheckDependenciesGeneric(string deps, out string message)
        {
            bool depsMet = true;
            message = string.Empty;
            foreach (string dep in deps.Split(','))
            {
                Type foundType = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                    from type in assembly.GetTypes()
                    where type.Name == dep
                    select type).FirstOrDefault();
                
                if (!string.IsNullOrEmpty(dep) && foundType == null)
                {
                    message += $"Generic Type: {dep}\n";
                    depsMet = false;
                }
            }
            return depsMet;
        }

        private static bool CheckDependenciesModule(string deps, out string message)
        {
            bool depsMet = true;
            message = string.Empty;
            foreach (string dep in deps.Split(','))
            {
                if (!string.IsNullOrEmpty(dep) && installedModules.Find(x => x.ObjectId == dep) == null)
                {
                    message += $"CCK Module: {modules.Find(x => x.ObjectId == dep)?.ObjectName}\n";
                    depsMet = false;
                }
            }
            return depsMet;
        }
        
        private static bool CheckDependenciesPackage(string deps, out string message)
        {
            bool depsMet = true;
            message = string.Empty;
            foreach (string dep in deps.Split(','))
            {
                if (!string.IsNullOrEmpty(dep) && InstalledPackages.Find(x => x == dep) == null)
                {
                    message += $"Package Manager: {dep}\n";
                    depsMet = false;
                }
            }
            return depsMet;
        }

        private static bool CheckIfInstalled(string objectId)
        {
            return Directory.Exists($"Assets/ABI.MODS/{objectId}");
        }
        
        private static void UninstallModule(ListObj module)
        {
            installedModules.Remove(module);
            if (!string.IsNullOrEmpty(module.ObjectUninstallRemoveSymbols))
            {
                foreach (string sym in module.ObjectUninstallRemoveSymbols.Split(','))
                {
                    string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
                    if (defines.Contains(sym))
                    {
                        defines = defines.Replace(sym, "").Replace(";;", ";");
                        PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, defines);
                    }
                }
            }
            Directory.Delete($"Assets/ABI.MODS/{module.ObjectId}", true);
            WriteInstalledModules();
            AssetDatabase.Refresh();
        }

        private static void WriteInstalledModules()
        {
            File.WriteAllText($"{Application.persistentDataPath}/modules.json", JsonConvert.SerializeObject(installedModules));
        }

        private static void ReadInstalledModules()
        {
            if (!File.Exists($"{Application.persistentDataPath}/modules.json")) return;
            List<ListObj> inst = JsonConvert.DeserializeObject<List<ListObj>>(File.ReadAllText($"{Application.persistentDataPath}/modules.json"));
            installedModules.Clear();
            foreach (ListObj obj in inst)
            {
                if (CheckIfInstalled(obj.ObjectId)) installedModules.Add(obj);
            }
            WriteInstalledModules();
        }
    }

    [System.Serializable]
    class ListObj
    {
        public string ObjectId { get; set; }
        public string ObjectName { get; set; }
        public string ObjectDescription{ get; set; } 
        public string ObjectAuthor { get; set; }
        public string ObjectVersion { get; set; }
        public string ObjectFileName { get; set; }
        public string ObjectFileSize { get; set; }
        public int ObjectUpdateStrategy { get; set; }
        public string ObjectUninstallRemoveSymbols { get; set; }
        public string ObjectDependenciesTypes { get; set; }
        public string ObjectDependenciesModules { get; set; }
        public string ObjectDependenciesPackages { get; set; }
    }
    
    public class UnityWebRequestAwaiter : INotifyCompletion
    {
        private UnityWebRequestAsyncOperation asyncOp;
        private Action continuation;

        public UnityWebRequestAwaiter(UnityWebRequestAsyncOperation asyncOp)
        {
            this.asyncOp = asyncOp;
            asyncOp.completed += OnRequestCompleted;
        }

        public bool IsCompleted { get { return asyncOp.isDone; } }

        public void GetResult() { }

        public void OnCompleted(Action continuation)
        {
            this.continuation = continuation;
        }

        private void OnRequestCompleted(AsyncOperation obj)
        {
            continuation();
        }
    }

    public static class ExtensionMethods
    {
        public static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
        {
            return new UnityWebRequestAwaiter(asyncOp);
        }
    }

    public enum ModuleUpdateStrategy
    {
        OverImport = 0,
        DeleteAndReImport = 1
    }
    
}