using UnityEngine;
using UnityEngine.AI;

namespace ABI.CCK.Components
{
    public class CVRNavController : MonoBehaviour
    {
        public NavMeshAgent navMeshAgent;
        public Transform[] navTargetList;
        public int navTargetIndex = 0;
        public Transform[] patrolPoints;
        public int patrolPointIndex = 0;
        public float patrolPointCheckDistance = 0.5f;
        public bool patrolEnabled = false;
    }
}