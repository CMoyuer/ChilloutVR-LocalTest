using System;
using System.Collections.Generic;
using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRDistanceLod : MonoBehaviour
    {
        public bool distance3D = false;
        public List<CVRDistanceLodGroup> Groups = new List<CVRDistanceLodGroup>();
        
        private static Color[] _gizmoColors = new Color[] { Color.green, Color.yellow, Color.red, Color.white };

        private void OnDrawGizmosSelected()
        {
            if (!distance3D)
            {
                Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, new Vector3(1f, 0f, 1f));
            }
            else
            {
                Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, Vector3.one);
            }
            
            var i = 0;
            foreach (var group in Groups)
            {
                Gizmos.color = _gizmoColors[Math.Min(i, 3)];
                Gizmos.DrawWireSphere(Vector3.zero, group.MaxDistance);
                i++;
            }
        }
    }

    [System.Serializable]
    public class CVRDistanceLodGroup
    {
        public GameObject GameObject;
        public float MinDistance;
        public float MaxDistance;
    }
}