using ABI_RC.Core.Player;
using MelonLoader;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChilloutVR_LocalTest
{
    public class Main : MelonMod
    {
        public static Main Interface;

        public override void OnApplicationStart()
        {
            Interface = this;
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            // MelonLogger.Msg($"OnSceneWasInitialized: {sceneName}");
            _ = Loom.Current;
            UDPSocketHelper.Interface.OnMessageReceive = OnChangeLocalAvatar;
        }

        private void OnChangeLocalAvatar(string ip, string msg)
        {
            if (PlayerSetup.Instance == null) return;
            Loom.QueueOnMainThread(() =>
            {
                AssetBundle assetBundle = null;
                try
                {
                    var info = JObject.Parse(msg);
                    if (info.ContainsKey("type"))
                    {
                        var type = (string)info["type"];
                        if (type.Equals("change_local_avatar"))
                        {
                            var path = (string)info["path"];
                            if (!File.Exists(path)) throw new Exception($"Avatar asset not found: {path}");
                            MelonLogger.Msg($"Loading local avatar: {path}");
                            assetBundle = AssetBundle.LoadFromFile(path);
                            var prefab = assetBundle.LoadAsset<GameObject>(assetBundle.GetAllAssetNames()[0]);
                            var obj = UnityEngine.Object.Instantiate(prefab);
                            obj.name = "_CVRAvatar(Clone)";
                            obj.transform.parent = PlayerSetup.Instance.transform.Find("[PlayerAvatar]") ?? PlayerSetup.Instance.transform;
                            obj.transform.localPosition = Vector3.zero;
                            obj.transform.localEulerAngles = Vector3.zero;
                            PlayerSetup.Instance.ClearAvatar();
                            PlayerSetup.Instance.SetupAvatar(obj);
                            MelonLogger.Msg($"Load local avatar Success!");
                        }
                        else if (type.Equals("change_local_world"))
                        {
                            var path = (string)info["path"];
                            if (!File.Exists(path)) throw new Exception($"World asset not found: {path}");
                            MelonLogger.Msg($"Loading local world: {path}");
                            assetBundle = AssetBundle.LoadFromFile(path);
                            SceneManager.LoadScene(assetBundle.GetAllScenePaths()[0]);
                            MelonLogger.Msg($"Load local world Success!");
                        }
                    }

                }
                catch (Exception e)
                {
                    MelonLogger.Error(e);
                }
                finally
                {
                    if (assetBundle != null)
                        assetBundle.Unload(false);
                }
            });
        }

    }
}
