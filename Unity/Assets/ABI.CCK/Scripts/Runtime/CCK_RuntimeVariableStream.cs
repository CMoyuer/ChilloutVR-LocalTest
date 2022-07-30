using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;
using Abi.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

namespace ABI.CCK.Scripts.Runtime
{
    public class CCK_RuntimeVariableStream : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(StreamVars());
        }
        
        private IEnumerator StreamVars()
        {
            OnGuiUpdater updater = gameObject.GetComponent<OnGuiUpdater>();
            string type = updater.asset.type.ToString();
            var values = new Dictionary<string, string>
            {
                #if UNITY_EDITOR
                { "Username", EditorPrefs.GetString("m_ABI_Username")},
                { "AccessKey", EditorPrefs.GetString("m_ABI_Key")},
                { "PreferredUploadRegion", EditorPrefs.GetInt("ABI_PREF_UPLOAD_REGION").ToString() },
                #endif
                { "ContentType", type},
                { "ContentId", updater.asset.objectId},
            };
            using (UnityWebRequest www = UnityWebRequest.Post("https://gateway.abi.network/v1/IContentCreation/ParameterStream", values))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log("[CCK:RuntimeVariableStream] Unable to connect to the Gateway. The Gateway might be unavailable. Check https://status.abinteractive.net for more info.");
                    yield break;
                }

                VariableStreamResponse response = JsonConvert.DeserializeObject<VariableStreamResponse>(www.downloadHandler.text);

                if (response == null)
                {
#if UNITY_EDITOR
                    EditorUtility.ClearProgressBar();
                    if (UnityEditor.EditorUtility.DisplayDialog("Alpha Blend Interactive CCK","Request failed. Unable to connect to the Gateway. The Gateway might be unavailable. Check https://status.abinteractive.net for more info.","Okay"))
                    {
                        EditorApplication.isPlaying = false;
                    }
#endif
                    yield break;
                }

                if (!response.HasPermission)
                {
#if UNITY_EDITOR
                    EditorUtility.ClearProgressBar();
                    if (UnityEditor.EditorUtility.DisplayDialog("Alpha Blend Interactive CCK","Request failed. The provided content ID does not belong to your account.","Okay"))
                    {
                        EditorApplication.isPlaying = false;
                    }
#endif
                    yield break;
                }
                
                if (response.IsAtUploadLimit)
                {
#if UNITY_EDITOR
                    EditorUtility.ClearProgressBar();
                    if (UnityEditor.EditorUtility.DisplayDialog("Alpha Blend Interactive CCK","Request failed. Your account has reached the upload limit. Please consider buying the Unlocked account.","Okay"))
                    {
                        EditorApplication.isPlaying = false;
                    }
#endif
                }
                
                if (response.IsBannedFromUploading)
                {
#if UNITY_EDITOR
                    EditorUtility.ClearProgressBar();
                    if (UnityEditor.EditorUtility.DisplayDialog("Alpha Blend Interactive CCK","Request failed. Your upload permissions are suspended. For more information, consult your moderation profile in the ABI community hub.","Okay"))
                    {
                        EditorApplication.isPlaying = false;
                    }
#endif
                }

                updater.UploadLocation = response.UploadLocation;
                
                updater.assetName.text = response.ObjectName;
                updater.assetDesc.text = response.ObjectDescription;

                updater.LoudAudio.isOn = response.LoudAudio;
                updater.LongRangeAudio.isOn = response.LongRangeAudio;
                updater.SpawnAudio.isOn = response.SpawnAudio;
                updater.ContainsMusic.isOn = response.ContainsMusic;

                updater.ScreenEffects.isOn = response.ScreenFx;
                updater.FlashingColors.isOn = response.FlashingColors;
                updater.FlashingLights.isOn = response.FlashingLights;
                updater.ExtremelyBright.isOn = response.ExtremelyBright;
                updater.ParticleSystems.isOn = response.ParticleSystems;
                
                updater.Violence.isOn = response.Violence;
                updater.Gore.isOn = response.Gore;
                updater.Horror.isOn = response.Horror;
                updater.Jumpscare.isOn = response.Jumpscare;

                updater.ExcessivelySmall.isOn = response.ExtremelySmall;
                updater.ExcessivelyHuge.isOn = response.ExtremelyHuge;

                updater.Suggestive.isOn = response.Suggestive;
                updater.Nudity.isOn = response.Nudity;
            }
        }
    }

    [Serializable]
    public class VariableStreamResponse
    {
        public bool HasPermission { get; set; }
        public bool IsAtUploadLimit { get; set; }
        public bool IsBannedFromUploading { get; set; }

        public string UploadLocation { get; set; }
        
        public string ObjectName { get; set; }
        public string ObjectDescription { get; set; }
        
        public bool LoudAudio { get; set; }
        public bool LongRangeAudio { get; set; }
        public bool SpawnAudio { get; set; }
        public bool ContainsMusic { get; set; }

        public bool ScreenFx { get; set; }
        public bool FlashingColors { get; set; }
        public bool FlashingLights { get; set; }
        public bool ExtremelyBright { get; set; }
        public bool ParticleSystems { get; set; }

        public bool Violence { get; set; }
        public bool Gore { get; set; }
        public bool Horror { get; set; }
        public bool Jumpscare { get; set; }

        public bool ExtremelySmall { get; set; }
        public bool ExtremelyHuge { get; set; }

        public bool Suggestive { get; set; }
        public bool Nudity { get; set; }
    }
}