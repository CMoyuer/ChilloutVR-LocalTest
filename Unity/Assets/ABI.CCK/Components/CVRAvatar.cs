using System.Collections.Generic;
using ABI.CCK.Scripts;
using UnityEngine;

namespace ABI.CCK.Components
{
    [RequireComponent(typeof(CVRAssetInfo))]
    [RequireComponent(typeof(Animator))]
    [ExecuteInEditMode]
    public class CVRAvatar : MonoBehaviour
    {
        public enum CVRAvatarVoiceParent
        {
            Head = 0,
            LeftHand = 2,
            RightHand = 3,
            Hips = 4
        }
        
        [Space] [Header("General avatar settings")] [Space]
        public Vector3 viewPosition = new Vector3(0, 0.1f, 0);
        public Vector3 voicePosition = new Vector3(0, 0.1f, 0);
        public CVRAvatarVoiceParent voiceParent = CVRAvatarVoiceParent.Head;
        public bool useEyeMovement = true;
        public bool useBlinkBlendshapes;
        public bool useVisemeLipsync;
        public SkinnedMeshRenderer bodyMesh;

        public string[] blinkBlendshape = new string[4];

        public enum CVRAvatarVisemeMode
        {
            Visemes = 0,
            SingleBlendshape = 1,
            JawBone = 2
        }

        public CVRAvatarVisemeMode visemeMode = CVRAvatarVisemeMode.Visemes;
        
        public string[] visemeBlendshapes = new string[15];

        [Space] [Header("Avatar customization")] [Space]
        public AnimatorOverrideController overrides;

        public bool enableAdvancedTagging = false;
        public List<CVRAvatarAdvancedTaggingEntry> advancedTaggingList = new List<CVRAvatarAdvancedTaggingEntry>();

        public bool avatarUsesAdvancedSettings = false;
        public CVRAdvancedAvatarSettings avatarSettings = null;
        
        void OnDrawGizmosSelected()
        {
            var scale = transform.localScale;
            scale.x = 1 / scale.x;
            scale.y = 1 / scale.y;
            scale.z = 1 / scale.z;

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.TransformPoint(Vector3.Scale(viewPosition, scale)), 0.01f);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.TransformPoint(Vector3.Scale(voicePosition, scale)), 0.01f);
        }
        
        private void OnEnable()
        {
            CVRAssetInfo info = gameObject.GetComponent<CVRAssetInfo>();
            info.type = CVRAssetInfo.AssetType.Avatar;
        }

    }

    [System.Serializable]
    public class CVRAvatarAdvancedTaggingEntry
    {
        public enum Tags
        {
            LoudAudio = 1,
            LongRangeAudio = 2,
            ScreenFx = 4,
            FlashingColors = 8,
            FlashingLights = 16,
            Violence = 32,
            Gore = 64,
            //Suggestive = 128,
            //Nudity = 256,
            Horror = 512
        }

        public Tags tags = 0;

        public GameObject gameObject;
        public GameObject fallbackGameObject;
    }
}
