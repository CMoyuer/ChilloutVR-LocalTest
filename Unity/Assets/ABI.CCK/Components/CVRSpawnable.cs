using System;
using System.Collections;
using System.Collections.Generic;
using ABI.CCK.Components;
using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRSpawnable : MonoBehaviour
    {
        public float spawnHeight = 0f;
        
        public bool useAdditionalValues;
        
        public List<CVRSpawnableValue> syncValues = new List<CVRSpawnableValue>();

        public enum PropPrivacy
        {
            everyone = 1,
            owner = 2
        }

        public PropPrivacy propPrivacy = PropPrivacy.everyone;
        
        public List<CVRSpawnableSubSync> subSyncs = new List<CVRSpawnableSubSync>();

        public enum SpawnableType
        {
            StandaloneSpawnable = 0,
            WorldSpawnable = 1
        }

        [HideInInspector]
        public SpawnableType spawnableType = SpawnableType.StandaloneSpawnable;

        [HideInInspector]
        public string preGeneratedInstanceId = "";

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;

            Gizmos.DrawLine(transform.position, transform.position - new Vector3(0, spawnHeight, 0));
            Gizmos.matrix = Matrix4x4.TRS(transform.position - new Vector3(0, spawnHeight, 0), Quaternion.identity,
                new Vector3(1f, 0f, 1f));
            Gizmos.DrawWireSphere(Vector3.zero, 0.25f);
            Gizmos.DrawLine(new Vector3(0, 0, 0.35f), new Vector3(0.177f, 0, 0.177f));
            Gizmos.DrawLine(new Vector3(0, 0, 0.35f), new Vector3(0, 0, 0.25f));
            Gizmos.DrawLine(new Vector3(0, 0, 0.35f), new Vector3(-0.177f, 0, 0.177f));
            
            Gizmos.matrix = Matrix4x4.identity;
            
            //SubSyncGizmos
            foreach (var subSync in subSyncs)
            {
                if (subSync.precision == CVRSpawnableSubSync.SyncPrecision.Full) continue;
                if (subSync.transform == null) continue;
                
                Gizmos.matrix = Matrix4x4.TRS(subSync.transform.parent.position, Quaternion.identity, subSync.transform.parent.lossyScale);
                
                if (Mathf.Abs(subSync.transform.localPosition.x) > subSync.syncBoundary ||
                    Mathf.Abs(subSync.transform.localPosition.y) > subSync.syncBoundary ||
                    Mathf.Abs(subSync.transform.localPosition.z) > subSync.syncBoundary)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.blue;
                }
                
                Gizmos.DrawWireCube(Vector3.zero, Vector3.one * subSync.syncBoundary * 2f);
            }
        }

        private void DestroyProp()
        {
            
        }
        
        private void Reset()
        {
            if (GetComponent<CVRBuilderSpawnable>() != null)
            {
                Invoke("DestroyThis", 0);
            }
            else if (GetComponent<CVRAssetInfo>() == null)
            {
                CVRAssetInfo info = gameObject.AddComponent<CVRAssetInfo>();
                info.type = CVRAssetInfo.AssetType.Spawnable;
            }
        }
        void DestroyThis() {
            DestroyImmediate(this);
        }
    }

    [System.Serializable]
    public class CVRSpawnableValue
    {
        public string name;
        public float startValue;
        
        public enum UpdatedBy
        {
            None = 0,
            SystemTime = 1,
            WorldTime = 2,
            SpawnerPositionX = 3,
            SpawnerPositionY = 4,
            SpawnerPositionZ = 5,
            SpawnerDistance = 6,
            SpawnerLookDirectionX = 7,
            SpawnerLookDirectionY = 8,
            SpawnerLookDirectionZ = 9,
            SpawnerLeftHandDirectionX = 10,
            SpawnerLeftHandDirectionY = 11,
            SpawnerLeftHandDirectionZ = 12,
            SpawnerRightHandDirectionX = 13,
            SpawnerRightHandDirectionY = 14,
            SpawnerRightHandDirectionZ = 15,
            SpawnerLeftGrip = 16,
            SpawnerRightGrip = 17,
            SpawnerLeftTrigger = 18,
            SpawnerRightTrigger = 19,
            OwnerLeftGrip = 20,
            OwnerRightGrip = 21,
            OwnerLeftTrigger = 22,
            OwnerRightTrigger = 23,
            OwnerCurrentGrip = 24,
            OwnerCurrentTrigger = 25,
            OwnerOppositeGrip = 26,
            OwnerOppositeTrigger = 27
        }

        public UpdatedBy updatedBy = UpdatedBy.None;

        public enum UpdateMethod
        {
            Override = 1,
            AddToDefault = 2,
            AddToCurrent = 3,
            SubtractFromDefault = 4,
            SubtractFromCurrent = 5,
            MultiplyWithDefault = 6,
            DefaultDividedByCurrent = 7,
        }

        public UpdateMethod updateMethod = UpdateMethod.Override;

        public Animator animator;

        public string animatorParameterName;
    }
    
    [System.Serializable]
    public class CVRSpawnableSubSync
    {
        public Transform transform;

        [Flags]
        public enum SyncFlags
        {
            TransformX = 1 << 1,
            TransformY = 1 << 2,
            TransformZ = 1 << 3,
            RotationX = 1 << 4,
            RotationY = 1 << 5,
            RotationZ = 1 << 6
        }

        public SyncFlags syncedValues;

        public enum SyncPrecision
        {
            Quarter = 1,
            Half = 2,
            Full = 4
        }

        public SyncPrecision precision = SyncPrecision.Full;

        public float syncBoundary = 0.5f;
    }
}