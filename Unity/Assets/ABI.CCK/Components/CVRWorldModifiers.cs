using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRWorldModifiers : MonoBehaviour
    {
        [Space] [Header("World modification")] [Space]
        [Range(1,5)] public float voiceCommsMinDistance = 1.5f;
        [Range(3,50)] public float voiceCommsMaxDistance = 10f;
        public float baseMovementSpeed = 2f;
        public float jumpHeight = 1f;
        public bool disableJumping = false;
        public bool disableFlight = false;
        public bool disableTeleportRequests = false;
        public bool disableProps = false;

        [Space] [Header("Newton Runtime")] [Space]
        public bool newtonEnabled;
        public bool autoRegisterFlavors;
        public bool useGameObjectRegistry;
    }
}
