#if UNITY_EDITOR
using ABI.CCK.Components;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MoyuerLocalTest.LocalTestUtils;

namespace MoyuerLocalTest
{
    public class CVRLocalWorldTest
    {
        private Vector2 mainScrollPos;
        [MenuItem("Moyuer/CVR_LocalTest/World", false, 1)]
        private static void StartTest()
        {
            var scenePath = SceneManager.GetActiveScene().path;
            StartTestWorld(scenePath);
            EditorUtility.DisplayDialog("Tip", "Built local world successfully , please go to the game to check the effect", "OK");
        }
        public static void StartTestWorld(string scenePath)
        {
            var path = Application.dataPath.Replace("\\", "/");
            if (path.Contains("/")) path = path.Substring(0, path.LastIndexOf("/"));
            path += "/Build/";
            if (Directory.Exists(path)) Directory.Delete(path, true);
            Directory.CreateDirectory(path);
            var fileName = path + "temp";
            if (File.Exists(fileName)) File.Delete(fileName);

            var report = BuildPipeline.BuildPlayer(new string[] { scenePath }, fileName, BuildTarget.StandaloneWindows64, BuildOptions.BuildAdditionalStreamedScenes);
            if (report == null)
            {
                EditorUtility.DisplayDialog("Error", "Built local world failed, please check the console log", "OK");
                return;
            }
            SendUDPPacket("{\"type\":\"change_local_world\",\"path\":\"" + fileName + "\"}");
        }
    }
}
#endif