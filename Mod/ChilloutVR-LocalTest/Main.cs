using ABI_RC.Core;
using ABI_RC.Core.Player;
using ABI_RC.Core.Savior;
using ABI_RC.Core.UI;
using MelonLoader;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;
using System.Xml;
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
            _ = Loom.Current;
            UDPSocketHelper.Interface.OnMessageReceive = OnChangeLocalAvatar;
        }

        private void OnChangeLocalAvatar(string ip, string msg)
        {
            Loom.QueueOnMainThread(() =>
            {
                AssetBundle assetBundle = null;
                try
                {
                    var info = JObject.Parse(msg);
                    if (info.ContainsKey("type"))
                    {
                        var type = (string)info["type"];
                        if (type.Equals("skip_login"))
                        {
                            SkipLogin();
                        }
                        else if (type.Equals("change_local_avatar"))
                        {
                            LoadLocalAvatar((string)info["path"]);
                        }
                        else if (type.Equals("change_local_world"))
                        {
                            LoadLocalWorld((string)info["path"]);
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
        // ------------------------------------------------------------------------------------------------
        private void LoadLocalAvatar(string path)
        {
            if (!PlayerSetup.Instance)
            {
                MelonLogger.Warning("You are not logged in yet! You can use the SkipLogin function.");
                return;
            }
            AssetBundle assetBundle = null;
            try
            {
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
            catch (Exception e)
            {
                MelonLogger.Error(e);
            }
            finally
            {
                if (assetBundle != null)
                    assetBundle.Unload(false);
            }
        }
        // ------------------------------------------------------------------------------------------------
        private string tempWorldPath = null;
        private void LoadLocalWorld(string path)
        {
            if (!PlayerSetup.Instance)
            {
                MelonLogger.Warning("You are not logged in yet! skipping login...");
                tempWorldPath = path;
                SkipLogin();
                return;
            }
            AssetBundle assetBundle = null;
            try
            {
                if (!File.Exists(path)) throw new Exception($"World asset not found: {path}");
                MelonLogger.Msg($"Loading local world: {path}");
                assetBundle = AssetBundle.LoadFromFile(path);
                SceneManager.LoadScene(assetBundle.GetAllScenePaths()[0]);
                MelonLogger.Msg($"Load local world Success!");
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
        }
        // ------------------------------------------------------------------------------------------------
        private void SkipLogin()
        {
            if (PlayerSetup.Instance)
            {
                MelonLogger.Msg("You are logged in!");
                return;
            }
            var manager = UnityEngine.Object.FindObjectOfType<AuthUIManager>();
            if (!manager)
            {
                MelonLogger.Warning("Currently not in the login interface!");
                return;
            }
            var username = "player";
            var accessKey = "12345678910";
            if (!File.Exists(Application.dataPath + "/autologin.profile"))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(string.Concat(new string[]
                {
                                "<LoginProfile>    <Username>",
                                username,
                                "</Username>    <AccessKey>",
                                accessKey,
                                "</AccessKey></LoginProfile>"
                }));
                xmlDocument.Save(Application.dataPath + "/autologin.profile");
            }
            MetaPort.Instance.username = username;
            MetaPort.Instance.accessKey = accessKey;
            MetaPort.Instance.ownerId = "00000000-0000-0000-0000-000000000000";
            MetaPort.Instance.homeWorldGuid = "501e2584-ce9a-4570-8c28-ef496e033f5f";
            MetaPort.Instance.currentAvatarGuid = "17c267db-18c4-4900-bb73-ad323f082640";
            MetaPort.Instance.VideoResolverHashUrl = "https://github.com/yt-dlp/yt-dlp/releases/latest/download/SHA2-256SUMS";
            MetaPort.Instance.VideoResolverExecutableUrl = "https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp.exe";

            CommonTools.Log(CommonTools.LogLevelType_t.Info, string.Concat(new string[]
            {
                    "[CVRGame => ",
                    base.GetType().Name,
                    "] ABI Realtime Network Services answered with successful login: Authenticated as ",
                    username,
                    "."
            }), "A0100");
            manager.loginWithProfileUi.SetActive(true);
            manager.loginFormUi.SetActive(false);
            manager.errorBox.SetActive(true);
            manager.errorHeader.text = "Successfully logged in";
            manager.errorContent.text = "Successfully authenticated as " + username + ".";
            MelonCoroutines.Start(LoginOk());
        }

        private IEnumerator LoginOk()
        {
            yield return new WaitForEndOfFrame();
            MetaPort.Instance.UpdateVideoResolver();
            yield return SceneManager.LoadSceneAsync("Init");
            if(tempWorldPath != null)
            {
                yield return new WaitForSeconds(2.0f);
                LoadLocalWorld(tempWorldPath);
            }
        }
        // ------------------------------------------------------------------------------------------------
    }
}
