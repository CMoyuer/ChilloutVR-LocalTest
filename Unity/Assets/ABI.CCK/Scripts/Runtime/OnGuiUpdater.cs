using System;
using System.Collections;
using System.IO;
using ABI.CCK.Components;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ABI.CCK.Scripts.Runtime
{
    public class OnGuiUpdater : MonoBehaviour
    {
        [Space] [Header("Object details")] [Space]
        public Text uiTitle;
        public InputField assetName;
        public InputField assetDesc;
        public InputField assetChangelog;
        
        [Space] [Header("Object tags")] [Space]
        public Toggle LoudAudio;
        public Toggle LongRangeAudio;
        public Toggle SpawnAudio;
        public Toggle ContainsMusic;
        public Toggle ScreenEffects;
        public Toggle FlashingColors;
        public Toggle FlashingLights;
        public Toggle ExtremelyBright;
        public Toggle ParticleSystems;
        public Toggle Violence;
        public Toggle Gore;
        public Toggle Horror;
        public Toggle Jumpscare;
        public Toggle ExcessivelyHuge;
        public Toggle ExcessivelySmall;
        public Toggle Suggestive;
        public Toggle Nudity;

        public Toggle dontOverridePicture;
        public Toggle SetAsActive;
        
        //Regulatory
        public Toggle contentOwnership;
        public Toggle tagsCorrect;
        
        [Space] [Header("Object reference")] [Space]
        public CVRAssetInfo asset;

        [Space] [Header("UIHelper objects")] [Space]
        public GameObject camObj;
        public RenderTexture tex;
        public RawImage texView;
        public RawImage texViewBig;
        public GameObject tagsObject;
        public GameObject detailsObject;
        public GameObject legalObject;
        public GameObject uploadObject;
        public Image stepOne;
        public Image stepTwo;
        public Image stepThree;
        public Image tagsImage;
        public Image detailsImage;
        public Image legalImage;
        public Image uploadImage;
        public Text tagsText;
        public Text detailsText;
        public Text legalText;
        public Text uploadText;
        public Image uploadProgress;
        public Text uploadProgressText;
        public Image processingProgress;
        public Text processingProgressText;
        public Text assetFileSizeText;
        public Text assetImageFileSizeText;
        public Text assetFileManifestSizeText;
        public Text assetFilePano1SizeText;
        public Text assetFilePano4SizeText;

        [HideInInspector] public string UploadLocation;

        public CCK_RuntimeUploaderMaster uploader;
        
        public void ToggleObject(GameObject obj)
        {
            obj.SetActive(!obj.activeInHierarchy);
        }
        
        public static string ToFileSizeString(long fileSize)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (fileSize >= 1024 && order < sizes.Length - 1) {
                order++;
                fileSize = fileSize/1024;
            }
            
            return $"{fileSize:0.##} {sizes[order]}";
        }
        
        void Start()
        {
            SwitchPage(0);
            CVRAssetInfo.AssetType type = asset.GetComponent<CVRAssetInfo>().type;
            
            if (type == CVRAssetInfo.AssetType.World)
            {
                Scripts.Editor.CCK_WorldPreviewCapture.CreatePanoImages();
            }

            tex = new RenderTexture(512,512,1, RenderTextureFormat.ARGB32);
            tex.Create();
            
            camObj = new GameObject();
            camObj.name = "ShotCam for CVR CCK";
            camObj.transform.rotation = new Quaternion(0,180,0,0);
            CVRAvatar avatar = asset.GetComponent<CVRAvatar>();
            if (asset.type == CVRAssetInfo.AssetType.Avatar) camObj.transform.position = new Vector3(avatar.viewPosition.x, avatar.viewPosition.y, avatar.viewPosition.z *= 5f);
            var cam = camObj.AddComponent<Camera>();
            cam.aspect = 1f;
            cam.nearClipPlane = 0.01f;
            cam.targetTexture = tex;
            texView.texture = tex;
            texViewBig.texture = tex;
            
            #if UNITY_EDITOR

#endif
            
            if (type == CVRAssetInfo.AssetType.Avatar)
            {
                assetFileSizeText.text = ToFileSizeString(new FileInfo(Application.persistentDataPath + "/bundle.cvravatar").Length);
                assetImageFileSizeText.text = "N/A";
                assetFileManifestSizeText.text = ToFileSizeString(new FileInfo(Application.persistentDataPath + "/bundle.cvravatar.manifest").Length);
                assetFilePano1SizeText.text = "N/A";
                assetFilePano4SizeText.text = "N/A";
            }

            if (type == CVRAssetInfo.AssetType.Spawnable)
            {
                assetFileSizeText.text = ToFileSizeString(new FileInfo(Application.persistentDataPath + "/bundle.cvrprop").Length);
                assetImageFileSizeText.text = "";
                assetFileManifestSizeText.text = ToFileSizeString(new FileInfo(Application.persistentDataPath + "/bundle.cvrprop.manifest").Length);
                assetFilePano1SizeText.text = "N/A";
                assetFilePano4SizeText.text = "N/A";
            }
            
            if (type == CVRAssetInfo.AssetType.World)
            {
                assetFileSizeText.text = ToFileSizeString(new FileInfo(Application.persistentDataPath + "/bundle.cvrworld").Length);
                assetImageFileSizeText.text = "N/A";
                assetFileManifestSizeText.text = ToFileSizeString(new FileInfo(Application.persistentDataPath + "/bundle.cvrworld.manifest").Length);
                assetFilePano1SizeText.text = ToFileSizeString(new FileInfo(Application.persistentDataPath + "/bundle_pano_1024.png").Length);
                assetFilePano4SizeText.text = ToFileSizeString(new FileInfo(Application.persistentDataPath + "/bundle_pano_4096.png").Length);
            }
            
        }

        public void SwitchPage(int index)
        {
            switch (index)
            {
                case 0:
                    tagsObject.SetActive(true);
                    detailsObject.SetActive(false);
                    legalObject.SetActive(false);
                    uploadObject.SetActive(false);
                    stepOne.color = Color.white;
                    stepTwo.color = Color.white;
                    stepThree.color = Color.white;
                    tagsImage.color = Color.yellow;
                    detailsImage.color = Color.white;
                    legalImage.color = Color.white;
                    uploadImage.color = Color.white;
                    tagsText.color = Color.yellow;
                    detailsText.color = Color.white;
                    legalText.color = Color.white;
                    uploadText.color = Color.white;
                    break;
                case 1:
                    tagsObject.SetActive(false);
                    detailsObject.SetActive(true);
                    legalObject.SetActive(false);
                    uploadObject.SetActive(false);
                    stepOne.color = Color.green;
                    stepTwo.color = Color.white;
                    stepThree.color = Color.white;
                    tagsImage.color = Color.green;
                    detailsImage.color = Color.yellow;
                    legalImage.color = Color.white;
                    uploadImage.color = Color.white;
                    tagsText.color = Color.green;
                    detailsText.color = Color.yellow;
                    legalText.color = Color.white;
                    uploadText.color = Color.white;
                    break;
                case 2:
                    tagsObject.SetActive(false);
                    detailsObject.SetActive(false);
                    legalObject.SetActive(true);
                    uploadObject.SetActive(false);
                    stepOne.color = Color.green;
                    stepTwo.color = Color.green;
                    stepThree.color = Color.white;
                    tagsImage.color = Color.green;
                    detailsImage.color = Color.green;
                    legalImage.color = Color.yellow;
                    uploadImage.color = Color.white;
                    tagsText.color = Color.green;
                    detailsText.color = Color.green;
                    legalText.color = Color.yellow;
                    uploadText.color = Color.white;
                    break;
                case 3:
                    tagsObject.SetActive(false);
                    detailsObject.SetActive(false);
                    legalObject.SetActive(false);
                    uploadObject.SetActive(true);
                    stepOne.color = Color.green;
                    stepTwo.color = Color.green;
                    stepThree.color = Color.green;
                    tagsImage.color = Color.green;
                    detailsImage.color = Color.green;
                    legalImage.color = Color.green;
                    uploadImage.color = Color.yellow;
                    tagsText.color = Color.green;
                    detailsText.color = Color.green;
                    legalText.color = Color.green;
                    uploadText.color = Color.yellow;
                    if (string.IsNullOrEmpty(assetName.text))
                    {
#if UNITY_EDITOR
                        EditorUtility.DisplayDialog("Alpha Blend Interactive CCK", CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDSTEP_UPLOAD_DETAILS_MISSING"), "Okay");
#endif
                        SwitchPage(1);
                        return;
                    }

                    if (!contentOwnership.isOn || !tagsCorrect.isOn)
                    {
#if UNITY_EDITOR
                        EditorUtility.DisplayDialog("Alpha Blend Interactive CCK", CCKLocalizationProvider.GetLocalizedText("ABI_UI_BUILDSTEP_UPLOAD_LEGAL_MISSING"), "Okay");
#endif
                        SwitchPage(2);
                        return;
                    }
                    uploader.StartUpload();
                    break;
            }
        }

        public void MakePhotoAndDisableCamera()
        {
            
            Camera c = camObj.GetComponent<Camera>();
            if (!c.enabled)
            {
                c.targetTexture = tex;
                c.enabled = true;
            }
            gameObject.GetComponent<CCK_TexImageCreation>().SaveTexture(c, tex);
            assetImageFileSizeText.text = ToFileSizeString(new FileInfo(Application.persistentDataPath + "/bundle.png").Length);
            c.enabled = false;
            //
            //StartCoroutine(MakePhotoAndDisableCamera__Internal());
        }
    }
}
