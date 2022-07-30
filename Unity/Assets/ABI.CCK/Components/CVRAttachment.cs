using UnityEngine;
using UnityEngine.Events;

namespace ABI.CCK.Components
{
    public class CVRAttachment : MonoBehaviour
    {
        [System.Flags]
        public enum AttachmentType
        {
            Bone = 1,
            Tracker = 2,
        }

        public AttachmentType attachmentType;

        [System.Flags]
        public enum BoneType
        {
            Hips = 1,
            Spine = 2,
            Chest = 4,
            UpperChest = 8,
            Neck = 16,
            Head = 32,
            LeftUpperLeg = 64,
            LeftLowerLeg = 128,
            LeftFoot = 256,
            RightUpperLeg = 512,
            RightLowerLeg = 1024,
            RightFoot = 2048,
            LeftShoulder = 4096,
            LeftArm = 8192,
            LeftForearm = 16384,
            LeftHand = 32768,
            RightShoulder = 65536,
            RightArm = 131072,
            RightForearm = 262144,
            RightHand = 524288,
            Root = 1048576
        }

        public BoneType boneType = 0;

        [System.Flags]
        public enum TrackerType
        {
            MainCamera = 1,
            RightHand = 2,
            LeftHand = 4,
            AdditionalTracker = 8
        }

        public TrackerType trackerType = 0;

        public bool useFixedPositionOffset = false;
        public bool useFixedRotationOffset = false;

        public Vector3 positionOffset;
        public Vector3 rotationOffset;

        public float maxAttachmentDistance = 0f;
        
        [SerializeField]
        public UnityEvent onAttach;
        
        [SerializeField]
        public UnityEvent onDeattach;

        public void Attach()
        {
            
        }

        public void DeAttach()
        {
            
        }
    }
}