using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using ABI.CCK.Components;
using ABI.CCK.Scripts;
using Abi.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;

namespace ABI.CCK.Scripts.Runtime
{
    public class CCK_RuntimeUploaderMaster : MonoBehaviour
    {
        public OnGuiUpdater updater;
        private UnityWebRequest req;

        public bool isUploading;
        private bool _stateTransfer = false;
        public float progress = 0f;

        public string encryption;
        
        public void StartUpload()
        {
            //Build string
            var type = updater.asset.type;
            var sfwLevel = string.Empty;
            var overwritePic = string.Empty;
            
            if (!File.Exists($"{Application.persistentDataPath}/bundle.png")) gameObject.GetComponent<CCK_TexImageCreation>().SaveTexture(updater.camObj.GetComponent<Camera>(), updater.tex);
            StartCoroutine(UploadAssetAndSendInformation(updater.asset.GetComponent<CVRAssetInfo>().objectId, type.ToString(), sfwLevel, updater.assetName.text, updater.assetDesc.text, updater.dontOverridePicture.isOn));
        }

        private IEnumerator UploadAssetAndSendInformation(string contentId, string type, string sfwLevel, string assetName, string assetDesc, bool overwritePic)
        {
            string[] path = null;
            if (type == "Avatar")
            {
                path = new string[3];
                path[0] = $"file://{Application.persistentDataPath}/bundle.cvravatar";
                path[1] = $"file://{Application.persistentDataPath}/bundle.cvravatar.manifest";
                path[2] = $"file://{Application.persistentDataPath}/bundle.png";
            }
            if (type == "World")
            {
                path = new string[5];
                path[0] = $"file://{Application.persistentDataPath}/bundle.cvrworld";
                path[1] = $"file://{Application.persistentDataPath}/bundle.cvrworld.manifest";
                path[2] = $"file://{Application.persistentDataPath}/bundle.png";
                path[3] = $"file://{Application.persistentDataPath}/bundle_pano_1024.png";
                path[4] = $"file://{Application.persistentDataPath}/bundle_pano_4096.png";
            }
            if (type == "Spawnable")
            {
                path = new string[3];
                path[0] = $"file://{Application.persistentDataPath}/bundle.cvrprop";
                path[1] = $"file://{Application.persistentDataPath}/bundle.cvrprop.manifest";
                path[2] = $"file://{Application.persistentDataPath}/bundle.png";
            }

            UnityWebRequest[] files = new UnityWebRequest[path.Length];
            WWWForm form = new WWWForm();
            
#if UNITY_EDITOR
            form.AddField("Username", EditorPrefs.GetString("m_ABI_Username"));
            form.AddField("AccessKey", EditorPrefs.GetString("m_ABI_Key"));
#endif
            form.AddField("ContentId", contentId);
            form.AddField("ContentType", type);
            
            form.AddField("ContentName", assetName);
            form.AddField("ContentDescription", assetDesc);
            form.AddField("ContentChangelog", updater.assetChangelog.text);
            
            if (updater.LoudAudio.isOn) form.AddField("Tag_LoudAudio", 1);
            if (updater.LongRangeAudio.isOn) form.AddField("Tag_LongRangeAudio", 1);
            if (updater.ContainsMusic.isOn) form.AddField("Tag_ContainsMusic", 1);
            if (updater.SpawnAudio.isOn) form.AddField("Tag_SpawnAudio", 1);
            
            if (updater.FlashingColors.isOn) form.AddField("Tag_FlashingColors", 1);
            if (updater.FlashingLights.isOn) form.AddField("Tag_FlashingLights", 1);
            if (updater.ExtremelyBright.isOn) form.AddField("Tag_ExtremelyBright", 1);
            if (updater.ScreenEffects.isOn) form.AddField("Tag_ScreenEffects", 1);
            if (updater.ParticleSystems.isOn) form.AddField("Tag_ParticleSystems", 1);
            
            if (updater.Violence.isOn) form.AddField("Tag_Violence", 1);
            if (updater.Gore.isOn) form.AddField("Tag_Gore", 1);
            if (updater.Horror.isOn) form.AddField("Tag_Horror", 1);
            if (updater.Jumpscare.isOn) form.AddField("Tag_Jumpscare", 1);
            
            if (updater.ExcessivelyHuge.isOn) form.AddField("Tag_ExcessivelyHuge", 1);
            if (updater.ExcessivelySmall.isOn) form.AddField("Tag_ExcessivelySmall", 1);
            
            if (updater.Suggestive.isOn) form.AddField("Tag_Suggestive", 1);
            if (updater.Nudity.isOn) form.AddField("Tag_Nudity", 1);
            
            if (updater.SetAsActive.isOn) form.AddField("Flag_SetFileAsActive", 1);
            if (overwritePic) form.AddField("Flag_OverwritePicture", 1);

            for (int i = 0; i < files.Length; i++)
            {
                string fieldName = string.Empty;
                switch (i)
                {
                    case 0: fieldName = "AssetFile"; break;
                    case 1: fieldName = "AssetManifestFile"; break;
                    case 2: fieldName = "AssetThumbnail"; break;
                    case 3: fieldName = "AssetPano1K"; break;
                    case 4: fieldName = "AssetPano4K"; break;
                }
                files[i] = UnityWebRequest.Get(path[i]);
                yield return files[i].SendWebRequest();
                form.AddBinaryData(fieldName, files[i].downloadHandler.data, Path.GetFileName(path[i]));
            }
            #if UNITY_EDITOR
            req = UnityWebRequest.Post($"https://{updater.UploadLocation}/v1/upload-file", form);
            #endif
            isUploading = true;
            yield return req.SendWebRequest();
            
            if (req.isHttpError || req.isNetworkError)
                Debug.LogError(req.error);
        }

        private IEnumerator WriteState()
        {
            _stateTransfer = true;
            yield return new WaitForSeconds(0.15f);
            float percent = 0f;
            while (99f > percent)
            {
                var values = new Dictionary<string, string>
                {
                    {"ContentType", updater.asset.type.ToString()},
                    {"ContentId", updater.asset.objectId}
                };

                using (UnityWebRequest uwr =
                    UnityWebRequest.Post($"https://{updater.UploadLocation}/v1/progress-for-file", values))
                {
                    yield return uwr.SendWebRequest();

                    if (uwr.isNetworkError || uwr.isHttpError)
                    {
                        Debug.Log(
                            "[CCK:RuntimeUploader] Unable to connect to the edge we are uploading to. There might be a network issue.");
                        yield break;
                    }

                    UploadProgressStatus s =
                        JsonConvert.DeserializeObject<UploadProgressStatus>(uwr.downloadHandler.text);
                    if (s != null)
                    {
                        updater.processingProgress.fillAmount = s.Percent / 100;
                        if (!string.IsNullOrEmpty(s.CurrentStep)) updater.processingProgressText.text = CCKLocalizationProvider.GetLocalizedText(s.CurrentStep);
                        if (s.Percent > 99f) ShowResponseDialog(s.CurrentStep);
                        percent = s.Percent;
                    }
                }

                yield return new WaitForSeconds(0.25f);
            }
        }

        void Update()
        {
            if (isUploading)
            {
#if UNITY_EDITOR
                updater.uploadProgress.fillAmount = req.uploadProgress;
                updater.uploadProgressText.text = (req.uploadProgress * 100f).ToString("F2") + "%";
                if (req.uploadProgress * 100f > 99.9f)
                {
                    updater.uploadProgressText.text = "100%";
                    if (!_stateTransfer) StartCoroutine(WriteState());
                }
#endif
            }
        }
        
        private void ShowResponseDialog(string text)
        {
            isUploading = false;
            #if UNITY_EDITOR
            EditorUtility.ClearProgressBar();
            if (UnityEditor.EditorUtility.DisplayDialog("Alpha Blend Interactive CCK",$"Message from server: {CCKLocalizationProvider.GetLocalizedText(text)}","Okay"))
            {
                EditorApplication.isPlaying = false;
            }
            #endif
        }
    }

    [Serializable]
    public class UploadProgressStatus
    {
        public string CurrentStep { get; set; }
        public float Percent { get; set; }
    }
}
