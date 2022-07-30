using System;
using System.Collections.Generic;
using ABI.CCK.Components;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ABI.CCK.Scripts.Editor
{
    public class CCK_BuildUtility
    {
        public static PreAvatarBundleEvent PreAvatarBundleEvent = new PreAvatarBundleEvent();
        public static PrePropBundleEvent PrePropBundleEvent = new PrePropBundleEvent();
        
        public static void BuildAndUploadAvatar(GameObject avatarObject)
        {
            //GameObject avatarCopy = null;
            var origInfo = avatarObject.GetComponent<CVRAssetInfo>();
            
            /*try
            {
                avatarCopy = GameObject.Instantiate(avatarObject);
                PrefabUtility.UnpackPrefabInstance(avatarCopy, PrefabUnpackMode.Completely, InteractionMode.UserAction);
                Debug.Log("[CCK:BuildUtility] To prevent problems, the prefab has been unpacked. Your game object is no longer linked to the prefab instance.");
            }
            catch
            {
                Debug.Log("[CCK:BuildUtility] Object is not a prefab. No need to unpack.");
            }*/

            //CVRAssetInfo info = avatarCopy.GetComponent<CVRAssetInfo>();
            if (string.IsNullOrEmpty(origInfo.objectId))
            {
                origInfo.objectId = Guid.NewGuid().ToString();
                //origInfo.guid = info.guid;
                try
                {
                    PrefabUtility.ApplyPrefabInstance(avatarObject, InteractionMode.UserAction);
                }
                catch
                {
                    Debug.Log("[CCK:BuildUtility] Object is not a prefab. No need to Apply To Instance.");
                }
            }
            
            PreAvatarBundleEvent.Invoke(avatarObject);

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            
            PrefabUtility.SaveAsPrefabAsset(avatarObject, "Assets/ABI.CCK/Resources/Cache/_CVRAvatar.prefab");
            //GameObject.DestroyImmediate(avatarCopy);
            
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

            AssetBundleBuild assetBundleBuild = new AssetBundleBuild();
            assetBundleBuild.assetNames = new[] {"Assets/ABI.CCK/Resources/Cache/_CVRAvatar.prefab"};
            assetBundleBuild.assetBundleName = "bundle.cvravatar";

            BuildPipeline.BuildAssetBundles(Application.persistentDataPath, new[] {assetBundleBuild},
                BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
            
            AssetDatabase.Refresh();
            
            EditorPrefs.SetBool("m_ABI_isBuilding", true);
            EditorApplication.isPlaying = true;
        }
        
        public static void BuildAndUploadSpawnable(GameObject s)
        {
            GameObject sCopy = null;
            var origInfo = s.GetComponent<CVRAssetInfo>();
            var spawnable = s.GetComponent<CVRSpawnable>();
            spawnable.spawnableType = CVRSpawnable.SpawnableType.StandaloneSpawnable;
            
            PrePropBundleEvent.Invoke(s);
            
            try
            {
                sCopy = GameObject.Instantiate(s);
                PrefabUtility.UnpackPrefabInstance(sCopy, PrefabUnpackMode.Completely, InteractionMode.UserAction);
                Debug.Log("[CCK:BuildUtility] To prevent problems, the prefab has been unpacked. Your game object is no longer linked to the prefab instance.");
            }
            catch
            {
                Debug.Log("[CCK:BuildUtility] Object is not a prefab. No need to unpack.");
            }

            CVRAssetInfo info = sCopy.GetComponent<CVRAssetInfo>();
            if (string.IsNullOrEmpty(info.objectId))
            {
                info.objectId = Guid.NewGuid().ToString();
                origInfo.objectId = info.objectId;
                try
                {
                    PrefabUtility.ApplyPrefabInstance(s, InteractionMode.UserAction);
                }
                catch
                {
                    Debug.Log("[CCK:BuildUtility] Object is not a prefab. No need to Apply To Instance.");
                }
            }
            
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            
            PrefabUtility.SaveAsPrefabAsset(sCopy, "Assets/ABI.CCK/Resources/Cache/_CVRSpawnable.prefab");
            GameObject.DestroyImmediate(sCopy);
            
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

            AssetBundleBuild assetBundleBuild = new AssetBundleBuild();
            assetBundleBuild.assetNames = new[] {"Assets/ABI.CCK/Resources/Cache/_CVRSpawnable.prefab"};
            assetBundleBuild.assetBundleName = "bundle.cvrprop";

            BuildPipeline.BuildAssetBundles(Application.persistentDataPath, new[] {assetBundleBuild},
                BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
            
            AssetDatabase.Refresh();
            
            EditorPrefs.SetBool("m_ABI_isBuilding", true);
            EditorApplication.isPlaying = true;
        }

        public static void BuildAndUploadMapAsset(Scene scene, GameObject descriptor)
        {
            SetupNetworkUUIDs();

            EditorSceneManager.MarkSceneDirty(scene);
            
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            
            CVRAssetInfo info = descriptor.GetComponent<CVRAssetInfo>();
            if (string.IsNullOrEmpty(info.objectId)) info.objectId = Guid.NewGuid().ToString();
            
            PrefabUtility.SaveAsPrefabAsset(descriptor, "Assets/ABI.CCK/Resources/Cache/_CVRWorld.prefab");
            
            AssetBundleBuild assetBundleBuild = new AssetBundleBuild();
            assetBundleBuild.assetNames = new[] {scene.path};
            assetBundleBuild.assetBundleName = "bundle.cvrworld";
            
            BuildPipeline.BuildAssetBundles(Application.persistentDataPath, new[] {assetBundleBuild},
                BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);

            AssetDatabase.Refresh();
            
            EditorPrefs.SetBool("m_ABI_isBuilding", true);
            EditorApplication.isPlaying = true;
        }

        public static void SetupNetworkUUIDs()
        {
            CVRInteractable[] interactables = Resources.FindObjectsOfTypeAll<CVRInteractable>();
            CVRObjectSync[] objectSyncs = Resources.FindObjectsOfTypeAll<CVRObjectSync>();
            CVRVideoPlayer[] videoPlayers = Resources.FindObjectsOfTypeAll<CVRVideoPlayer>();
            
            CVRSpawnable[] spawnables = Resources.FindObjectsOfTypeAll<CVRSpawnable>();
            
            GameInstanceController[] gameInstances = Resources.FindObjectsOfTypeAll<GameInstanceController>();
            
            GunController[] gunControllers = Resources.FindObjectsOfTypeAll<GunController>();
            
            List<string> UsedGuids = new List<string>();

            foreach (var interactable in interactables)
            {
                foreach (var action in interactable.actions)
                {
                    string guid;
                    do
                    {
                        guid = Guid.NewGuid().ToString();
                    } while (!string.IsNullOrEmpty(UsedGuids.Find(match => match == guid)));
                    UsedGuids.Add(guid);

                    action.guid = guid;
                }
            }
            
            foreach (var objectSync in objectSyncs)
            {
                string guid;
                do
                {
                    guid = Guid.NewGuid().ToString();
                } while (!string.IsNullOrEmpty(UsedGuids.Find(match => match == guid)));
                UsedGuids.Add(guid);

                var newserializedObject = new SerializedObject(objectSync);
                newserializedObject.Update();
                SerializedProperty _guidProperty = newserializedObject.FindProperty("guid"); 
                _guidProperty.stringValue = guid;
                newserializedObject.ApplyModifiedProperties();
            }

            foreach (var player in videoPlayers)
            {
                Guid res;
                if (player.playerId == null || !Guid.TryParse(player.playerId, out res))
                {
                    string guid;
                    do
                    {
                        guid = Guid.NewGuid().ToString();
                    } while (!string.IsNullOrEmpty(UsedGuids.Find(match => match == guid)));
                    UsedGuids.Add(guid);

                    player.playerId = guid;
                }
            }
            
            foreach (var spawnable in spawnables)
            {
                string guid;
                do
                {
                    guid = Guid.NewGuid().ToString();
                } while (!string.IsNullOrEmpty(UsedGuids.Find(match => match == guid)));

                if (spawnable.preGeneratedInstanceId == "")
                {
                    UsedGuids.Add(guid);
                    spawnable.preGeneratedInstanceId = "ws~" + guid;
                }
                else
                {
                    UsedGuids.Add(spawnable.preGeneratedInstanceId);
                }

                spawnable.spawnableType = CVRSpawnable.SpawnableType.WorldSpawnable;
            }

            int i = 0;
            foreach (GameInstanceController gameInstance in gameInstances)
            {
                if (gameInstance.teams.Count == 0)
                {
                    var team = new Team();
                    team.name = "Team";
                    team.color = Color.red;
                    team.playerLimit = 16;
                    team.index = i;
                    gameInstance.teams.Add(team);
                    i++;
                }
                else
                {
                    foreach (var team in gameInstance.teams)
                    {
                        team.index = i;
                        i++;
                    }
                }
            }
            
            foreach (var gunController in gunControllers)
            {
                string guid;
                do
                {
                    guid = Guid.NewGuid().ToString();
                } while (!string.IsNullOrEmpty(UsedGuids.Find(match => match == guid)));
                UsedGuids.Add(guid);

                var newserializedObject = new SerializedObject(gunController);
                newserializedObject.Update();
                SerializedProperty _guidProperty = newserializedObject.FindProperty("referenceID"); 
                _guidProperty.stringValue = guid;
                newserializedObject.ApplyModifiedProperties();
            }
        }
    }
    
    [System.Serializable]
    public class PreAvatarBundleEvent : UnityEvent<GameObject>
    {
        
    }
    
    [System.Serializable]
    public class PrePropBundleEvent : UnityEvent<GameObject>
    {
        
    }
}
