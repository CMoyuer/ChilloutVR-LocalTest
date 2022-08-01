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
        private Vector2 mainScrollPos;
        private CVRAvatar avatar;
        [MenuItem("Moyuer/CVR_LocalTest/Avatar", false, 0)]
        private static void ShowWindow()
        {
            GetWindow<CVRLocalAvatarTest>();
        }
        private void OnGUI()
        {
            mainScrollPos = GUILayout.BeginScrollView(mainScrollPos);
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
            GUILayout.Space(10);
            avatar = (CVRAvatar)EditorGUILayout.ObjectField("Avatar", avatar, typeof(CVRAvatar), true);
            GUILayout.Space(10);
            if (avatar == null) EditorGUILayout.HelpBox("Please select your Avatar.", MessageType.Warning);
            if (GUILayout.Button("Start Test") && avatar != null)
            {
                Fix(avatar.gameObject);
            }
            GUILayout.EndScrollView();
        }

        private static void Fix(GameObject avatar)
        {
            avatar.SetActive(true);
            var prefabPath = "Assets/temp.prefab";
            if (PrefabUtility.SaveAsPrefabAsset(avatar, prefabPath) == null)
            {
                EditorUtility.DisplayDialog("Error", "An error occurred, please check the Console log", "OK");
                return;
            }
            var path = Application.dataPath.Replace("\\", "/");
            if (path.Contains("/")) path = path.Substring(0, path.LastIndexOf("/"));
            path += "/Build/";
            if (Directory.Exists(path)) Directory.Delete(path, true);
            Directory.CreateDirectory(path);
            var fileName = path + "temp";
            if (File.Exists(fileName)) File.Delete(fileName);

            var builds = new List<AssetBundleBuild>();
            var build = new AssetBundleBuild();
            build.assetBundleName = "temp";
            build.assetNames = new string[] { prefabPath };
            builds.Add(build);
            if (BuildPipeline.BuildAssetBundles(path, builds.ToArray(), BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows) == null)
            {
                EditorUtility.DisplayDialog("Error", "Built local avatar failed, please check the console log", "OK");
                return;
            }
            SendUDPPacket("{\"type\":\"change_local_avatar\",\"path\":\"" + (path + "temp") + "\"}");
            EditorUtility.DisplayDialog("Tip", "Built local avatar successfully, please go to the game to check the effect", "OK");
            File.Delete(prefabPath);
        }
    }
}
#endif