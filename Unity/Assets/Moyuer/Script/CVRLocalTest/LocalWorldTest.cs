#if UNITY_EDITOR
using ABI.CCK.Components;
using System.IO;
using System.Linq;
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
        public const string BUILDPATH = "Build/LocalWorldTest/";
        public const string TEMPNAME = "temp";

        [MenuItem("Moyuer/CVR_LocalTest/World", false, 1)]
        private static void StartTest()
        {
            var scenePath = SceneManager.GetActiveScene().path;

            if (IsSceneAValidWorld())
            {
                StartTestWorld(scenePath);
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "This scene does not contain a CVR World component or contains more than one! Build canceled.", "OK");
            }
        }
        public static void StartTestWorld(string scenePath)
        {
            try
            {
                CreateBuildDirectory();

                RemovePreviousBuild();

                var m_Path = (Path.GetDirectoryName(Application.dataPath) + "/" + BUILDPATH).Replace("\\", "/") + TEMPNAME;

                BuildNewWorld(scenePath, m_Path);

                SendUDPWorld(m_Path);

                EditorUtility.DisplayDialog("LocalWorldTest", "Built local world successfully. Please go to the game to check the effect.", "OK");
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                EditorUtility.DisplayDialog("Error", "An error occurred while building asset bundle. Please check the console log.", "OK");
            }
        }

        private static bool IsSceneAValidWorld()
        {
            var worldComponent = Object.FindObjectsOfType<CVRWorld>();

            return (worldComponent.Count() == 1);
        }
        private static void BuildNewWorld(string scenePath, string m_Path)
        {
            var report = BuildPipeline.BuildPlayer(new string[] { scenePath }, m_Path, BuildTarget.StandaloneWindows64, BuildOptions.BuildAdditionalStreamedScenes);
            if (report == null) throw new System.Exception("Build asset bundles failed!");
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
        private static void SendUDPWorld(string m_Path)
        {
            SendUDPPacket("{\"type\":\"change_local_world\",\"path\":\"" + m_Path + "\"}");
        }
    }
}
#endif
