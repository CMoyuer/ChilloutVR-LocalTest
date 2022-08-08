#if UNITY_EDITOR
using ABI.CCK.Components;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static MoyuerLocalTest.LocalTestUtils;

namespace MoyuerLocalTest
{

    public class CVRLocalAvatarTest : EditorWindow
    {

        public const string BUILDPATH = "Build/LocalAvatarTest/";
        public const string PREFABPATH = "Assets/temp.prefab";
        public const string TEMPNAME = "temp";

        private Vector2 mainScrollPos;
        private CVRAvatar avatar;

        [MenuItem("Moyuer/CVR_LocalTest/Avatar", false, 0)]
        private static void ShowWindow()
        {
            GetWindow<CVRLocalAvatarTest>("LocalAvatarTest");
        }

        private void OnGUI()
        {
            mainScrollPos = GUILayout.BeginScrollView(mainScrollPos);

            ModByMoyuer();

            GUILayout.Space(10);
            EditorGUIUtility.labelWidth = 80f;
            avatar = (CVRAvatar)EditorGUILayout.ObjectField("Avatar", avatar, typeof(CVRAvatar), true);
            GUILayout.Space(10);

            if (avatar == null) 
                EditorGUILayout.HelpBox("Please select your Avatar.", MessageType.Warning);

            using (new EditorGUI.DisabledScope(avatar == null))
                if (GUILayout.Button("Build Test Avatar")) 
                    Build(avatar.gameObject);

            if (GUILayout.Button("Reload Previous Build"))
                Reload();

            GUILayout.EndScrollView();
        }

        private static void Build(GameObject avatar)
        {
            SaveTempPrefab(avatar);

            CreateBuildDirectory();

            RemovePreviousBuild();

            BuildNewAvatar();

            SendUDPAvatar();
            
            File.Delete(PREFABPATH);
        }
        private static void Reload()
        {
            var fileName = BUILDPATH + TEMPNAME;
            if (File.Exists(fileName))
            {
                SendUDPAvatar();
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "No previous avatar build found!", "OK");
            }
        }


        private static void SaveTempPrefab(GameObject avatar)
        {
            avatar.SetActive(true);
            if (PrefabUtility.SaveAsPrefabAsset(avatar, PREFABPATH) == null)
            {
                EditorUtility.DisplayDialog("Error", "An error occurred while saving prefab. Please check the console log.", "OK");
                return;
            }
        }
        private static void BuildNewAvatar()
        {
            var builds = new List<AssetBundleBuild>();
            var build = new AssetBundleBuild();
            build.assetBundleName = TEMPNAME;
            build.assetNames = new string[] { PREFABPATH };
            builds.Add(build);
            if (BuildPipeline.BuildAssetBundles(BUILDPATH, builds.ToArray(), BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows) == null)
            {
                EditorUtility.DisplayDialog("Error", "An error occurred while building asset bundle. Please check the console log.", "OK");
                return;
            }
        }
        private static void CreateBuildDirectory()
        {
            if (Directory.Exists(BUILDPATH)) Directory.Delete(BUILDPATH, true);
            Directory.CreateDirectory(BUILDPATH);
        }
        private static void RemovePreviousBuild()
        {
            var fileName = BUILDPATH + TEMPNAME;
            if (File.Exists(fileName)) File.Delete(fileName);
        }
        private static void SendUDPAvatar()
        {
            var m_Path = (Path.GetDirectoryName(Application.dataPath) + "/" + BUILDPATH).Replace("\\", "/");
            SendUDPPacket("{\"type\":\"change_local_avatar\",\"path\":\"" + (m_Path + TEMPNAME) + "\"}");
            EditorUtility.DisplayDialog("LocalAvatarTest", "Built local avatar successfully. Please go to the game to check the effect.", "OK");
        }

        private static void ModByMoyuer()
        {
            GUILayout.Space(10);
            GUI.skin.label.fontSize = 24;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label("CVR Avatar Test");
            GUI.skin.label.fontSize = 12;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label("ChilloutVR Local Avatar Test");
            GUI.skin.label.fontSize = 12;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label("by: 如梦(Moyuer)");
            GUILayout.Space(10);
        }
    }
}
#endif
