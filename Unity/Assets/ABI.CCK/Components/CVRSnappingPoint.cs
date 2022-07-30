using System;
using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRSnappingPoint : MonoBehaviour
    {
        public string type;

        public float distance = 0.05f;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Vector3 pos = transform.position;
            float length = distance * 1.25f;
            Gizmos.DrawLine(pos - length * Vector3.up, pos + length * Vector3.up);
            Gizmos.DrawLine(pos - length * Vector3.left, pos + length * Vector3.left);
            Gizmos.DrawLine(pos - length * Vector3.forward, pos + length * Vector3.forward);
            Gizmos.DrawWireSphere(pos, distance);
            Gizmos.color = new Color(0.2f, 0.2f, 0.2f);
            Gizmos.DrawWireSphere(pos, length);
        }
    }
}