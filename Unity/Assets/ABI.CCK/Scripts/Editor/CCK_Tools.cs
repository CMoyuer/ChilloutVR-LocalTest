using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml.Linq;
using System.Xml.XPath;
using ABI.CCK.Components;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace ABI.CCK.Scripts.Editor
{
    public class CCK_Tools
    {

        public enum SearchType
        {
            Scene = 1, 
            Selection = 2
        }
        
        public static int CleanMissingScripts (SearchType searchType, bool remove, GameObject givenObject)
        {
            List<GameObject> allFoundObjects = new List<GameObject>();
            GameObject[] rootObjects;

            if (searchType == SearchType.Scene)
            {
                rootObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
            }
            else
            {
                rootObjects = new GameObject[1];
                rootObjects[0] = givenObject;
            }

            foreach (var item in rootObjects)
            {
                allFoundObjects.AddRange(item.GetComponentsInChildren<Transform>(true).Select(go => go.gameObject).ToArray());
            }

            int scriptCount = 0;
            int goCount = 0;
            foreach (var go in allFoundObjects)
            {
                int count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);
                if (count > 0)
                {
                    if (remove)
                    {
                        Undo.RegisterCompleteObjectUndo(go, "Remove missing scripts");
                        GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
                    }
                    scriptCount += count;
                    goCount++;
                }
            }

            if (remove)
            {
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            }

            if (remove) Debug.Log($"[CCK:Tools] Found and removed {scriptCount} missing scripts from {goCount} GameObjects");

            return scriptCount;
        }

        [MenuItem("Assets/Create/CVR Override Controller")]
        private static void CreateCVROverrideController()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject) + "/New Override Controller.overrideController";
            string[] guids = AssetDatabase.FindAssets("AvatarAnimator t:animatorController", null);

            if (guids.Length < 1)
            {
                Debug.LogError("No Animator controller with the name \"AvatarAnimator\" was found. Please make sure that you CCK is installed properly.");
                return;
            }
            var overrideController = new AnimatorOverrideController();
            overrideController.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath<AnimatorController>(AssetDatabase.GUIDToAssetPath(guids[0]));
            
            AssetDatabase.CreateAsset (overrideController, path);
        }

    }
}
