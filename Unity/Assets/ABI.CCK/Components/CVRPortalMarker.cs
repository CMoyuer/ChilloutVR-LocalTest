using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRPortalMarker : MonoBehaviour
    {
        public string worldGUID;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireCube(new Vector3(0, 0.15f, 0), new Vector3(0.75f, 0.3f, 0.75f));
            Gizmos.DrawWireSphere(new Vector3(0, 1f, 0), 0.5f);
        }
    }
}