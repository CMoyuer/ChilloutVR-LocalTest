using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRHapticZone : MonoBehaviour
    {
        public enum TriggerForm
        {
            Box = 1,
            Sphere = 2
        }

        public TriggerForm triggerForm = TriggerForm.Box;

        public Vector3 center = Vector3.zero;
        public Vector3 bounds = Vector3.one;

        public enum TriggerTiming
        {
            once = 1,
            continuous = 2,
            random = 3
        }
        
        public bool enableOnEnter = false;
        public float onEnterIntensity = 0.2f;

        public bool enableOnStay = false;
        public float onStayIntensity = 0.2f;
        public TriggerTiming onStayTiming = TriggerTiming.continuous;
        public float onStayChance = 0.1f;

        public bool enableOnExit = false;
        public float onExitIntensity = 0.2f;

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.matrix = rotationMatrix;
            if (triggerForm == TriggerForm.Box)
            {
                Gizmos.DrawWireCube(center, bounds);
            }
            else
            {
                Gizmos.DrawWireSphere(center, bounds.x);
            }
        }
    }
}