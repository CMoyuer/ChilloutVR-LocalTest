using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using ABI.CCK.Components;
using ABI.CCK.Scripts.Runtime;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace ABI.CCK.Scripts.Editor
{
    [InitializeOnLoad]
    public class CCK_BuildManagerWindow : EditorWindow
    {
        public static string Version = "3.3 RELEASE";
        public static int BuildID = 90;
        private const string CCKVersion = "3.3 RELEASE (Build 90)";

        private string[] SupportedUnityVersions = new[]
        {
            "2019.4.0f1", "2019.4.1f1", "2019.4.2f1", "2019.4.3f1", "2019.4.4f1", "2019.4.5f1", "2019.4.6f1", 
            "2019.4.7f1", "2019.4.8f1", "2019.4.9f1", "2019.4.10f1", "2019.4.11f1", "2019.4.12f1", 
            "2019.4.13f1", "2019.4.14f1", "2019.4.15f1", "2019.4.16f1", "2019.4.17f1", "2019.4.18f1", 
            "2019.4.19f1", "2019.4.20f1", "2019.4.21f1", "2019.4.22f1", "2019.4.23f1", "2019.4.24f1", 
            "2019.4.25f1", "2019.4.26f1", "2019.4.27f1", "2019.4.28f1", "2019.4.29f1", "2019.4.30f1", 
            "2019.4.31f1"
        };
        
        string _username;
        string _key;

        public Texture2D abiLogo;
        private bool _attemptingToLogin;
        private bool _loggedIn;
        private bool _hasAttemptedToLogin;
        private bool _allowedToUpload;
        private string _apiUserRank;
        private string _apiCreatorRank;
        Vector2 scrollPosAvatar;
        Vector2 scrollPosSpawnable;
        Vector2 scrollPosWorld;
        UnityWebRequest _webRequest;
        
        private int _tab;
        private Vector2 _scroll;

        private static PropertyInfo _legacyBlendShapeImporter;
        
        private static PropertyInfo legacyBlendShapeImporter
        {
            get
            {
                if(_legacyBlendShapeImporter != null)
                {
                    return _legacyBlendShapeImporter;
                }

                Type modelImporterType = typeof(ModelImporter);
                _legacyBlendShapeImporter = modelImporterType.GetProperty(
                    "legacyComputeAllNormalsFromSmoothingGroupsWhenMeshHasBlendShapes",
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public
                );

                return _legacyBlendShapeImporter;
            }
        }
        
        [MenuItem("Alpha Blend Interactive/Control Panel (Builder and Settings)", false, 200)]
        static void Init()
        {
            CCK_BuildManagerWindow window = (CCK_BuildManagerWindow)GetWindow(typeof(CCK_BuildManagerWindow), false, $"CCK :: Control Panel");
            window.Show();
        }

        void OnDisable()
        {
            EditorApplication.update -= EditorUpdate;
            EditorApplication.playModeStateChanged -= OnEditorStateUpdated;
        }

        void OnEnable()
        {
            abiLogo = (Texture2D) AssetDatabase.LoadAssetAtPath("Assets/ABI.CCK/GUIAssets/abibig.png", typeof(Texture2D));
            
            EditorApplication.update -= EditorUpdate;
            EditorApplication.update += EditorUpdate;
            EditorApplication.playModeStateChanged -= OnEditorStateUpdated;
            EditorApplication.playModeStateChanged += OnEditorStateUpdated;

            _username = EditorPrefs.GetString("m_ABI_Username");
            _key = EditorPrefs.GetString("m_ABI_Key");
            Login();
        }

        void OnEditorStateUpdated(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode)
            {
                EditorPrefs.SetBool("m_ABI_isBuilding", false);
                EditorPrefs.SetString("m_ABI_TempVersion", Version);
                if (File.Exists(Application.dataPath + "/ABI.CCK/Resources/Cache/_CVRAvatar.prefab")) File.Delete(Application.dataPath + "/ABI.CCK/Resources/Cache/_CVRAvatar.prefab");
                if (File.Exists(Application.dataPath + "/ABI.CCK/Resources/Cache/_CVRSpawnable.prefab")) File.Delete(Application.dataPath + "/ABI.CCK/Resources/Cache/_CVRSpawnable.prefab");
                if (File.Exists(Application.dataPath + "/ABI.CCK/Resources/Cache/_CVRWorld.prefab")) File.Delete(Application.dataPath + "/ABI.CCK/Resources/Cache/_CVRWorld.prefab");
                if (File.Exists(Application.persistentDataPath + "/bundle.cvravatar")) File.Delete(Application.persistentDataPath + "/bundle.cvravatar");
                if (File.Exists(Application.persistentDataPath + "/bundle.cvravatar.manifest")) File.Delete(Application.persistentDataPath + "/bundle.cvravatar.manifest");
                if (File.Exists(Application.persistentDataPath + "/bundle.cvrprop")) File.Delete(Application.persistentDataPath + "/bundle.cvrprop");
                if (File.Exists(Application.persistentDataPath + "/bundle.cvrprop.manifest")) File.Delete(Application.persistentDataPath + "/bundle.cvrprop.manifest");
                if (File.Exists(Application.persistentDataPath + "/bundle.cvrworld")) File.Delete(Application.persistentDataPath + "/bundle.cvrworld");
                if (File.Exists(Application.persistentDataPath + "/bundle.cvrworld.manifest")) File.Delete(Application.persistentDataPath + "/bundle.cvrworld.manifest");
                if (File.Exists(Application.persistentDataPath + "/bundle.png")) File.Delete(Application.persistentDataPath + "/bundle.png");
                if (File.Exists(Application.persistentDataPath + "/bundle.png.manifest")) File.Delete(Application.persistentDataPath + "/bundle.png.manifest");
                if (File.Exists(Application.persistentDataPath + "/bundle_pano_1024.png")) File.Delete(Application.persistentDataPath + "/bundle_pano_1024.png");
                if (File.Exists(Application.persistentDataPath + "/bundle_pano_1024.png.manifest")) File.Delete(Application.persistentDataPath + "/bundle_pano_1024.png.manifest");
                if (File.Exists(Application.persistentDataPath + "/bundle_pano_4096.png")) File.Delete(Application.persistentDataPath + "/bundle_pano_4096.png");
                if (File.Exists(Application.persistentDataPath + "/bundle_pano_4096.png.manifest")) File.Delete(Application.persistentDataPath + "/bundle_pano_4096.png.manifest");
                AssetDatabase.Refresh();
            }

            if (state == PlayModeStateChange.EnteredPlayMode && EditorPrefs.GetBool("m_ABI_isBuilding"))
            {
                var ui = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/ABI.CCK/GUIAssets/CCK_UploaderHead.prefab"));
                OnGuiUpdater up = ui.GetComponentInChildren<OnGuiUpdater>();
                if (File.Exists(Application.dataPath + "/ABI.CCK/Resources/Cache/_CVRAvatar.prefab"))up.asset = Resources.Load<GameObject>("Cache/_CVRAvatar").GetComponent<CVRAssetInfo>();
                if (File.Exists(Application.dataPath + "/ABI.CCK/Resources/Cache/_CVRSpawnable.prefab"))up.asset = Resources.Load<GameObject>("Cache/_CVRSpawnable").GetComponent<CVRAssetInfo>();
                if (File.Exists(Application.dataPath + "/ABI.CCK/Resources/Cache/_CVRWorld.prefab"))up.asset = Resources.Load<GameObject>("Cache/_CVRWorld").GetComponent<CVRAssetInfo>();
            }
        }
        
        void OnGUI()
        {
            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperCenter;
            
            GUILayout.Label(abiLogo, centeredStyle);
            EditorGUILayout.BeginVertical();
            
            _tab = GUILayout.Toolbar (_tab, new string[] {CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_HEADING_BUILDER"), CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_HEADING_SETTINGS")});

            _scroll = EditorGUILayout.BeginScrollView(_scroll);
            
            switch (_tab)
            {
                case 0:
                    if (!_loggedIn)
                    {
                        Tab_LogIn();
                    }
                    else
                    {
                        Tab_LoggedIn();
                    }
                    break;
                
                case 1:
                    Tab_Settings();
                    break;
            }
            
            EditorGUILayout.EndScrollView();

            EditorGUILayout.EndVertical();
        }

        private void Tab_LogIn()
        {
            EditorGUILayout.LabelField("ABI Community Hub Access", EditorStyles.boldLabel);
            EditorGUILayout.LabelField(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN1"));
            EditorGUILayout.LabelField(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN2"));
            EditorGUILayout.LabelField(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_INFOTEXT_SIGNIN3"));
            EditorGUILayout.Space();
            _username = EditorGUILayout.TextField(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_LOGIN_TEXT_USERNAME"), _username);
            _key = EditorGUILayout.PasswordField(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_LOGIN_TEXT_ACCESSKEY"), _key);

            if (GUILayout.Button(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_LOGIN_BUTTON")))
            {
                Login();
            }

            if (_hasAttemptedToLogin && !_loggedIn)
            {
                GUILayout.Label(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_LOGIN_CREDENTIALS_INCORRECT"));
            }
        }

        private void Tab_LoggedIn()
        {
            EditorGUILayout.LabelField(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_HEADING_ACCOUNT_INFO"), EditorStyles.boldLabel);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_INFOTEXT_AUTHENTICATED_AS"), _username);
            EditorGUILayout.LabelField(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_INFOTEXT_USER_RANK"), _apiUserRank);
            EditorGUILayout.LabelField("CCK version    ", CCKVersion);
            EditorGUILayout.Space();
            if (GUILayout.Button(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_LOGOUT_BUTTON"))){ 
                bool logout = EditorUtility.DisplayDialog(
                    CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_LOGOUT_DIALOG_TITLE"),
                    CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_LOGOUT_DIALOG_BODY"),
                    CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_LOGOUT_DIALOG_ACCEPT"), 
                    CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_LOGOUT_DIALOG_DECLINE"));
                if (logout) Logout();
            }
            EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_INFOTEXT_DOCUMENTATION"), MessageType.Info);
            if (GUILayout.Button(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_HEADING_DOCUMENTATION"))) Application.OpenURL("https://docs.abinteractive.net");
            EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_WARNING_FEEDBACK"), MessageType.Info);
            if (GUILayout.Button(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_HEADING_FEEDBACK"))) Application.OpenURL("https://hub.abinteractive.net/feedback");
            EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_WARNING_FOLDERPATH"), MessageType.Warning);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_HEADING_FOUNDCONTENT"), EditorStyles.boldLabel);
            List<CVRAvatar> avatars = new List<CVRAvatar>();
            List<CVRSpawnable> spawnables = new List<CVRSpawnable>();
            List<CVRWorld> worlds = new List<CVRWorld>();
            
            foreach (CVRWorld w in Resources.FindObjectsOfTypeAll<CVRWorld>())
            {
                if (w.gameObject.activeInHierarchy) worlds.Add(w);
            }
            
            foreach (CVRSpawnable s in Resources.FindObjectsOfTypeAll<CVRSpawnable>())
            {
                if (s.gameObject.activeInHierarchy) spawnables.Add(s);
            }

            foreach (CVRAvatar a in Resources.FindObjectsOfTypeAll<CVRAvatar>())
            {
                if (a.gameObject.activeInHierarchy) avatars.Add(a);
            }

            if (worlds.Count <= 0 && avatars.Count > 0 && SupportedUnityVersions.Contains(Application.unityVersion))
            {
                if (avatars.Count <= 0) EditorGUILayout.LabelField(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_UPLOADER_NO_AVATARS_FOUND"));
                else
                {
                    if (avatars.Count > 0)
                    {
                        var counter = 0;
                        scrollPosAvatar = EditorGUILayout.BeginScrollView(scrollPosAvatar);
                        foreach (CVRAvatar a in avatars)
                        {
                            counter++;
                            EditorGUI.BeginChangeCheck();
                            EditorGUILayout.Space();
                            GUILayout.Label("Avatar Object #" + counter);
                            OnGUIAvatar(a);
                        }

                        EditorGUILayout.EndScrollView();
                    }
                }
            }
            if (worlds.Count <= 0 && spawnables.Count > 0 && SupportedUnityVersions.Contains(Application.unityVersion))
            {
                if (spawnables.Count <= 0) EditorGUILayout.LabelField(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_UPLOADER_NO_SPAWNABLES_FOUND"));
                else
                {
                    if (spawnables.Count > 0)
                    {
                        var counter = 0;
                        scrollPosSpawnable = EditorGUILayout.BeginScrollView(scrollPosSpawnable);
                        foreach (CVRSpawnable s in spawnables)
                        {
                            counter++;
                            EditorGUI.BeginChangeCheck();
                            EditorGUILayout.Space();
                            GUILayout.Label("Spawnable Object #" + counter);
                            OnGUISpawnable(s);
                        }

                        EditorGUILayout.EndScrollView();
                    }
                }
            }
            if (avatars.Count <= 0 && worlds.Count == 1 && SupportedUnityVersions.Contains(Application.unityVersion))
            {
                int errors = 0;
                int overallMissingScripts = 0;
                
                overallMissingScripts = CCK_Tools.CleanMissingScripts(CCK_Tools.SearchType.Scene , false, null);
                if (overallMissingScripts > 0) errors++;
                
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_INFOTEXT_WORLDS_NO_AVATARS"), MessageType.Info);

                //Error
                if (overallMissingScripts > 0) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_ERROR_WORLD_MISSING_SCRIPTS"), MessageType.Error);
                
                //Warning
                if (worlds[0].spawns.Length == 0) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_WORLDS_WARNING_SPAWNPOINT"), MessageType.Warning);

                //Info
                if (worlds[0].referenceCamera == null) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_WORLDS_INFO_REFERENCE_CAMERA"), MessageType.Info);
                if (worlds[0].respawnHeightY <= -500) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_WORLDS_INFO_RESPAWN_HEIGHT"), MessageType.Info);
                
                if (GUILayout.Button(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_UPLOAD_WORLD_BUTTON")) && errors <= 0)
                {
                    CCK_BuildUtility.BuildAndUploadMapAsset(EditorSceneManager.GetActiveScene(), worlds[0].gameObject);
                }
                if (overallMissingScripts > 0) if (GUILayout.Button(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_UTIL_REMOVE_MISSING_SCRIPTS_BUTTON"))) CCK_Tools.CleanMissingScripts(CCK_Tools.SearchType.Scene , true, null);
            }
            if (avatars.Count <= 0 && worlds.Count > 1 && SupportedUnityVersions.Contains(Application.unityVersion))
            {
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_WORLDS_ERROR_MULTIPLE_CVR_WORLD"), MessageType.Error);
            }
            if (avatars.Count > 0 && worlds.Count > 0 && SupportedUnityVersions.Contains(Application.unityVersion))
            {
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_WORLDS_ERROR_WORLD_CONTAINS_AVATAR"), MessageType.Error);
            }
            if (avatars.Count <= 0 && worlds.Count <= 0 && SupportedUnityVersions.Contains(Application.unityVersion))
            {
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_WORLDS_ERROR_NO_CONTENT"), MessageType.Info);
            }
            if (!SupportedUnityVersions.Contains(Application.unityVersion))
            {
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_WORLDS_ERROR_UNITY_UNSUPPORTED"), MessageType.Error);
            }
        }

        void Tab_Settings()
        {
            EditorGUILayout.LabelField(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_SETTINGS_HEADER"), EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_SETTINGS_CONTENT_REGION"));
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.BeginVertical();
            var region = EditorGUILayout.Popup(EditorPrefs.GetInt("ABI_PREF_UPLOAD_REGION", 0), new []{"Europe", "United States", "Asia"});
            EditorPrefs.SetInt("ABI_PREF_UPLOAD_REGION", region);
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_SETTINGS_HINT_CONTENT_REGION"), MessageType.Info);
            
            EditorGUILayout.Space();
            
            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_SETTINGS_CCK_LANGUAGE"));
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.BeginVertical();
            string selectedLanguage = EditorPrefs.GetString("ABI_CCKLocals", "English");
            int selectedIndex = CCKLocalizationProvider.GetKnownLanguages().FindIndex(match => match == selectedLanguage);
            if (selectedIndex < 0) selectedIndex = 0;
            selectedIndex = EditorGUILayout.Popup(selectedIndex, CCKLocalizationProvider.GetKnownLanguages().ToArray());
            EditorPrefs.SetString("ABI_CCKLocals", CCKLocalizationProvider.GetKnownLanguages()[selectedIndex]);
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_SETTINGS_HINT_CCK_LANGUAGE"), MessageType.Info);
            
            EditorGUILayout.Space();
        }
        
        void OnGUIAvatar(CVRAvatar avatar)
        {
            GameObject avatarObject = avatar.gameObject;
            GUI.enabled = true;
            EditorGUILayout.InspectorTitlebar(avatarObject.activeInHierarchy, avatarObject);
            int errors = 0;
            int overallPolygonsCount = 0;
            int overallSkinnedMeshRenderer = 0;
            int overallUniqueMaterials = 0;
            int overallMissingScripts = 0;
            foreach (MeshFilter filter in avatar.gameObject.GetComponentsInChildren<MeshFilter>())
            {
                if (filter.sharedMesh != null) overallPolygonsCount = overallPolygonsCount + filter.sharedMesh.triangles.Length / 3;
            }
            foreach (SkinnedMeshRenderer renderer in avatar.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                overallSkinnedMeshRenderer++;
                if (renderer.sharedMaterials != null) overallUniqueMaterials = overallUniqueMaterials + renderer.sharedMaterials.Length;
            }
            overallMissingScripts = CCK_Tools.CleanMissingScripts(CCK_Tools.SearchType.Selection ,false,avatarObject);
            if (overallMissingScripts > 0) errors++;

            //Errors
            if (overallMissingScripts > 0) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_ERROR_AVATAR_MISSING_SCRIPTS"), MessageType.Error);
            var animator = avatar.GetComponent<Animator>();
            if (animator == null)
            {
                errors++;
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_AVATAR_ERROR_ANIMATOR"), MessageType.Error);
            }
            if (animator != null && animator.avatar == null)
            {
                //errors++;
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_AVATAR_WARNING_GENERIC"), MessageType.Warning);
            }
            
            //Warnings
            if (overallPolygonsCount > 100000) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_AVATAR_WARNING_POLYGONS").Replace("{X}", overallPolygonsCount.ToString()), MessageType.Warning);
            if (overallSkinnedMeshRenderer > 10) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_AVATAR_WARNING_SKINNED_MESH_RENDERERS").Replace("{X}", overallSkinnedMeshRenderer.ToString()), MessageType.Warning);
            if (overallUniqueMaterials > 20) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_AVATAR_WARNING_MATERIALS").Replace("{X}", overallUniqueMaterials.ToString()), MessageType.Warning);
            if (avatar.viewPosition == Vector3.zero) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_AVATAR_WARNING_VIEWPOINT"), MessageType.Warning);
            if (animator != null && animator.avatar != null && !animator.avatar.isHuman) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_AVATAR_WARNING_NON_HUMANOID"), MessageType.Warning);
            
            var avatarMeshes = getAllAssetMeshesInAvatar(avatarObject);
            if (CheckForLegacyBlendShapeNormals(avatarMeshes))
            {
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_AVATAR_WARNING_LEGACY_BLENDSHAPES"), MessageType.Warning);
                if(GUILayout.Button(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_FIX_IMPORT_SETTINGS")))
                {
                    FixLegacyBlendShapeNormals(avatarMeshes);
                }
            }

            //Info
            if (overallPolygonsCount >= 50000 && overallPolygonsCount <= 100000) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_AVATAR_INFO_POLYGONS").Replace("{X}", overallPolygonsCount.ToString()), MessageType.Info);
            if (overallSkinnedMeshRenderer >= 5 && overallSkinnedMeshRenderer <= 10) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_AVATAR_INFO_SKINNED_MESH_RENDERERS").Replace("{X}", overallSkinnedMeshRenderer.ToString()), MessageType.Info);
            if (overallUniqueMaterials >= 10 && overallUniqueMaterials <= 20) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_AVATAR_INFO_MATERIALS").Replace("{X}", overallUniqueMaterials.ToString()), MessageType.Info);
            if (avatar.viewPosition.y <= 0.5f) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_AVATAR_INFO_SMALL"), MessageType.Info);
            if (avatar.viewPosition.y > 3f) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_AVATAR_INFO_HUGE"), MessageType.Info);

            if (errors <= 0) if (GUILayout.Button(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_AVATAR_UPLOAD_BUTTON"))) CCK_BuildUtility.BuildAndUploadAvatar(avatarObject);
            if (overallMissingScripts > 0) if (GUILayout.Button(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_UTIL_REMOVE_MISSING_SCRIPTS_BUTTON"))) CCK_Tools.CleanMissingScripts(CCK_Tools.SearchType.Selection ,true,avatarObject);

        }
        
        void OnGUISpawnable(CVRSpawnable s)
        {
            GameObject spawnableObject = s.gameObject;
            GUI.enabled = true;
            EditorGUILayout.InspectorTitlebar(spawnableObject.activeInHierarchy, spawnableObject);
            int errors = 0;
            int overallPolygonsCount = 0;
            int overallSkinnedMeshRenderer = 0;
            int overallUniqueMaterials = 0;
            int overallMissingScripts = 0;
            foreach (MeshFilter filter in s.gameObject.GetComponentsInChildren<MeshFilter>())
            {
                if (filter.sharedMesh != null) overallPolygonsCount = overallPolygonsCount + filter.sharedMesh.triangles.Length / 3;
            }
            foreach (SkinnedMeshRenderer renderer in s.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                overallSkinnedMeshRenderer++;
                if (renderer.sharedMaterials != null) overallUniqueMaterials = overallUniqueMaterials + renderer.sharedMaterials.Length;
            }
            overallMissingScripts = CCK_Tools.CleanMissingScripts(CCK_Tools.SearchType.Selection ,false, spawnableObject);
            if (overallMissingScripts > 0) errors++;

            //Errors
            if (overallMissingScripts > 0) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_PROPS_ERROR_MISSING_SCRIPT"), MessageType.Error);
            
            //Warnings
            if (overallPolygonsCount > 100000) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_PROPS_WARNING_POLYGONS").Replace("{X}", overallPolygonsCount.ToString()), MessageType.Warning);
            if (overallSkinnedMeshRenderer > 10) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_PROPS_WARNING_SKINNED_MESH_RENDERERS").Replace("{X}", overallSkinnedMeshRenderer.ToString()), MessageType.Warning);
            if (overallUniqueMaterials > 20) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_PROPS_WARNING_MATERIALS").Replace("{X}", overallUniqueMaterials.ToString()), MessageType.Warning);

            var spawnableMeshes = getAllAssetMeshesInAvatar(spawnableObject);
            if (CheckForLegacyBlendShapeNormals(spawnableMeshes))
            {
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_PROPS_WARNING_LEGACY_BLENDSHAPES"), MessageType.Warning);
                if(GUILayout.Button(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_FIX_IMPORT_SETTINGS")))
                {
                    FixLegacyBlendShapeNormals(spawnableMeshes);
                }
            }

            //Info
            if (overallPolygonsCount >= 50000 && overallPolygonsCount <= 100000) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_PROPS_INFO_POLYGONS").Replace("{X}", overallPolygonsCount.ToString()), MessageType.Info);
            if (overallSkinnedMeshRenderer >= 5 && overallSkinnedMeshRenderer <= 10) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_PROPS_INFO_SKINNED_MESH_RENDERERS").Replace("{X}", overallSkinnedMeshRenderer.ToString()), MessageType.Info);
            if (overallUniqueMaterials >= 10 && overallUniqueMaterials <= 20) EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_PROPS_INFO_MATERIALS").Replace("{X}", overallUniqueMaterials.ToString()), MessageType.Info);

            if (errors <= 0 && overallMissingScripts <= 0) if (GUILayout.Button(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_PROPS_UPLOAD_BUTTON"))) CCK_BuildUtility.BuildAndUploadSpawnable(spawnableObject);
            if (overallMissingScripts > 0) if (GUILayout.Button(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_UTIL_REMOVE_MISSING_SCRIPTS_BUTTON"))) CCK_Tools.CleanMissingScripts(CCK_Tools.SearchType.Selection ,true, spawnableObject);
        }

        private List<String> getAllAssetMeshesInAvatar(GameObject avatar)
        {
            var assetPathList = new List<String>();

            foreach (var sMeshRenderer in avatar.GetComponentsInChildren<SkinnedMeshRenderer>(true))
            {
                if(sMeshRenderer == null)
                {
                    continue;
                }
                
                var currentMesh = sMeshRenderer.sharedMesh;
                
                if(currentMesh == null)
                {
                    Debug.LogWarning(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_WARNING_MESH_FILTER_MESH_EMPTY") + $": {sMeshRenderer.transform.name}");
                    continue;
                }
                
                if(!AssetDatabase.Contains(currentMesh))
                {
                    continue;
                }

                string meshAssetPath = AssetDatabase.GetAssetPath(currentMesh);
                if(string.IsNullOrEmpty(meshAssetPath))
                {
                    continue;
                }
                
                if (assetPathList.Contains(meshAssetPath))
                {
                    continue;
                }
                
                assetPathList.Add(meshAssetPath);
            }
            
            foreach (var meshFilter in avatar.GetComponentsInChildren<MeshFilter>(true))
            {
                if(meshFilter == null)
                {
                    continue;
                }
                
                var currentMesh = meshFilter.sharedMesh;
                
                if(currentMesh == null)
                {
                    Debug.LogWarning(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_WARNING_MESH_FILTER_MESH_EMPTY") + $": {meshFilter.transform.name}");
                    continue;
                }
                
                if(!AssetDatabase.Contains(currentMesh))
                {
                    continue;
                }

                string meshAssetPath = AssetDatabase.GetAssetPath(currentMesh);
                if(string.IsNullOrEmpty(meshAssetPath))
                {
                    continue;
                }
                
                if (assetPathList.Contains(meshAssetPath))
                {
                    continue;
                }
                
                assetPathList.Add(meshAssetPath);
            }

            foreach (var pRenderer in avatar.GetComponentsInChildren<ParticleSystemRenderer>(true))
            {
                if(pRenderer == null)
                {
                    continue;
                }

                var particleMeshes = new Mesh[pRenderer.meshCount];
                pRenderer.GetMeshes(particleMeshes);

                foreach (var particleMesh in particleMeshes)
                {
                    if(particleMesh == null)
                    {
                        Debug.LogWarning(CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDPANEL_WARNING_MESH_FILTER_MESH_EMPTY") + $": {pRenderer.transform.name}");
                        continue;
                    }
                    
                    if(!AssetDatabase.Contains(particleMesh))
                    {
                        continue;
                    }

                    string meshAssetPath = AssetDatabase.GetAssetPath(particleMesh);
                    if(string.IsNullOrEmpty(meshAssetPath))
                    {
                        continue;
                    }
                
                    if (assetPathList.Contains(meshAssetPath))
                    {
                        continue;
                    }
                
                    assetPathList.Add(meshAssetPath);
                }
            }
            
            return assetPathList;
        }
        
        private bool CheckForLegacyBlendShapeNormals(List<String> assetPaths)
        {
            foreach (var assetPath in assetPaths)
            {

                var modelImporter = AssetImporter.GetAtPath(assetPath) as ModelImporter;
                if(modelImporter == null)
                {
                    continue;
                }

                if(modelImporter.importBlendShapeNormals != ModelImporterNormals.Calculate)
                {
                    continue;
                }
                
                if((bool)legacyBlendShapeImporter.GetValue(modelImporter))
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        private void FixLegacyBlendShapeNormals(List<String> assetPaths)
        {
            foreach (var assetPath in assetPaths)
            {
                var modelImporter = AssetImporter.GetAtPath(assetPath) as ModelImporter;
                if(modelImporter == null)
                {
                    continue;
                }

                if(modelImporter.importBlendShapeNormals != ModelImporterNormals.Calculate)
                {
                    continue;
                }

                legacyBlendShapeImporter.SetValue(modelImporter, true);
                modelImporter.SaveAndReimport();
            }
        }
        
        private void EditorUpdate()
        {
            if (!_attemptingToLogin || _webRequest is null || !_webRequest.isDone) return;

            if (_webRequest.isNetworkError || _webRequest.isHttpError)
            {
                Debug.LogError("[ABI:CCK] Web Request Error while trying to authenticate.");
                return;
            }

            var result = _webRequest.downloadHandler.text;
            if (string.IsNullOrEmpty(result)) return;

            LoginResponse usr = Abi.Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponse>(result);
            if (usr == null) return;
            
            if (usr.IsValidCredential)
            {
                _apiUserRank = usr.UserRank;
                Debug.Log("[ABI:CCK] Successfully authenticated as " + _username + " using AlphaLink Public API.");
                EditorPrefs.SetString("m_ABI_Username", _username);
                EditorPrefs.SetString("m_ABI_Key", _key);
                _loggedIn = true;
                _hasAttemptedToLogin = false;
            }
            else
            {
                Debug.Log("[ABI:CCK] Unable to authenticate using provided credentials. API responded with: " + usr.ApiMessage + ".");
                _loggedIn = false;
                _hasAttemptedToLogin = true;
                _username = _key = string.Empty;
            }

            _webRequest = null;
            _attemptingToLogin = false;
        }
        
        private void Logout()
        {
            _loggedIn = false;
            _username = _key = string.Empty;
            EditorPrefs.SetString("m_ABI_Username", _username);
            EditorPrefs.SetString("m_ABI_Key", _key);
        }

        public void Login()
        {
            if (_attemptingToLogin || string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_key)) return;
            var values = new Dictionary<string, string> {{"user", _username}, {"accesskey", _key}};
            _webRequest = UnityWebRequest.Post("https://gateway.abi.network/v1/IContentCreation/ValidateKey", values);
            _webRequest.SendWebRequest();
            _attemptingToLogin = true;
        }
    }
    
    public class LoginResponse
    {
        public bool IsValidCredential { get; set; }
        public bool IsAccountUnlocked { get; set; }
        public string ApiMessage { get; set; }
        public string UserId { get; set; }
        public string UserRank { get; set; }
    }
}