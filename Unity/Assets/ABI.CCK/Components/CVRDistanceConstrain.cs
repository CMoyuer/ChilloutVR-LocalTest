using System.ComponentModel;
using UnityEngine;

namespace ABI.CCK.Components
{
    [ExecuteInEditMode]
    [AddComponentMenu("Scripts/CVR Distance Constraint")]
    public class CVRDistanceConstrain : MonoBehaviour
    {
        public Transform target;
        public float minDistance = 0;
        public float maxDistance = 0;

        [ReadOnly]
        [SerializeField]
        private float currentDistance = 0f;
        
        public void OnDrawGizmosSelected()
        {
            if (target == null) return;

            if (maxDistance < minDistance && maxDistance != 0f) return;
            
            Vector3 direction = (transform.position - target.position).normalized;

            if (minDistance == 0)
            {
                if (maxDistance == 0)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawRay(target.position, direction * 9999f);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(target.position, target.position + direction * maxDistance);
                }
            }
            else
            {
                if (maxDistance == 0)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(target.position, target.position + direction * minDistance);
                    Gizmos.color = Color.green;
                    Gizmos.DrawRay(target.position + direction * minDistance, direction * 9999f);
                }
                else
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(target.position, target.position + direction * minDistance);
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(target.position + direction * minDistance, target.position + direction * maxDistance);
                }
            }
        }

        public void Update()
        {
            if (target == null)
            {
                currentDistance = 0f;
            }
            else
            {
                currentDistance = Vector3.Distance(transform.position, target.position);
            }
        }
    }
}