using System.Collections.Generic;
using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRSpawnableTrigger : MonoBehaviour
    {
        public Vector3 areaSize = new Vector3(0.05f, 0.05f, 0.05f);
        public Vector3 areaOffset = Vector3.zero;
        public int settingIndex = -1;
        public float settingValue = 0;

        public bool useAdvancedTrigger = false;

        public string[] allowedTypes = new string[0];
        public bool allowParticleInteraction = false;

        public List<CVRSpawnableTriggerTask> enterTasks = new List<CVRSpawnableTriggerTask>();
        public List<CVRSpawnableTriggerTask> exitTasks = new List<CVRSpawnableTriggerTask>();
        
        public List<CVRSpawnableTriggerTaskStay> stayTasks = new List<CVRSpawnableTriggerTaskStay>();

        public enum SampleDirection
        {
            XPositive,
            XNegative,
            YPositive,
            YNegative,
            ZPositive,
            ZNegative
        }

        public SampleDirection sampleDirection = SampleDirection.XPositive;
        
        public void Trigger()
        {
            
        }

        public void EnterTrigger()
        {
            
        }

        public void ExitTrigger()
        {
            
        }

        public void StayTrigger(float percent = 0f)
        {
            
        }

        private void OnDrawGizmosSelected()
        {
            if (isActiveAndEnabled)
            {
                Gizmos.color = Color.cyan;
                Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
                Gizmos.matrix = rotationMatrix;
                Gizmos.DrawCube(areaOffset, areaSize);

                Vector3 bounds = new Vector3(areaSize.x * 0.5f, areaSize.y * 0.5f, areaSize.z * 0.5f);
                
                if (stayTasks.Count > 0)
                {
                    Gizmos.DrawWireCube(areaOffset, areaSize);

                    switch (sampleDirection)
                    {
                        case SampleDirection.XPositive:
                            Gizmos.DrawLine(
                                new Vector3(-bounds.x, bounds.y, bounds.z) + areaOffset,
                                new Vector3(bounds.x, 0f, bounds.z) + areaOffset
                            );
                            Gizmos.DrawLine(
                                new Vector3(-bounds.x, -bounds.y, bounds.z) + areaOffset,
                                new Vector3(bounds.x, 0f, bounds.z) + areaOffset
                            );
                            Gizmos.DrawLine(
                                new Vector3(-bounds.x, bounds.y, bounds.z) + areaOffset,
                                new Vector3(bounds.x, bounds.y, 0f) + areaOffset
                            );
                            Gizmos.DrawLine(
                                new Vector3(-bounds.x, bounds.y, -bounds.z) + areaOffset,
                                new Vector3(bounds.x, bounds.y, 0f) + areaOffset
                            );
                            break;
                        case SampleDirection.XNegative:
                            Gizmos.DrawLine(
                                new Vector3(bounds.x, bounds.y, bounds.z) + areaOffset,
                                new Vector3(-bounds.x, 0f, bounds.z) + areaOffset
                            );
                            Gizmos.DrawLine(
                                new Vector3(bounds.x, -bounds.y, bounds.z) + areaOffset,
                                new Vector3(-bounds.x, 0f, bounds.z) + areaOffset
                            );
                            Gizmos.DrawLine(
                                new Vector3(bounds.x, bounds.y, bounds.z) + areaOffset,
                                new Vector3(-bounds.x, bounds.y, 0f) + areaOffset
                            );
                            Gizmos.DrawLine(
                                new Vector3(bounds.x, bounds.y, -bounds.z) + areaOffset,
                                new Vector3(-bounds.x, bounds.y, 0f) + areaOffset
                            );
                            break;
                        case SampleDirection.YPositive:
                            Gizmos.DrawLine(
                                new Vector3(-bounds.x, -bounds.y, bounds.z) + areaOffset,
                                new Vector3(0f, bounds.y, bounds.z) + areaOffset
                            );
                            Gizmos.DrawLine(
                                new Vector3(bounds.x, -bounds.y, bounds.z) + areaOffset,
                                new Vector3(0f, bounds.y, bounds.z) + areaOffset
                            );
                            Gizmos.DrawLine(
                                new Vector3(-bounds.x, -bounds.y, -bounds.z) + areaOffset,
                                new Vector3(-bounds.x, bounds.y, 0f) + areaOffset
                            );
                            Gizmos.DrawLine(
                                new Vector3(-bounds.x, -bounds.y, bounds.z) + areaOffset,
                                new Vector3(-bounds.x, bounds.y, 0f) + areaOffset
                            );
                            break;
                        case SampleDirection.YNegative:
                            Gizmos.DrawLine(
                                new Vector3(-bounds.x, bounds.y, bounds.z) + areaOffset,
                                new Vector3(0f, -bounds.y, bounds.z) + areaOffset
                            );
                            Gizmos.DrawLine(
                                new Vector3(bounds.x, bounds.y, bounds.z) + areaOffset,
                                new Vector3(0f, -bounds.y, bounds.z) + areaOffset
                            );
                            Gizmos.DrawLine(
                                new Vector3(-bounds.x, bounds.y, -bounds.z) + areaOffset,
                                new Vector3(-bounds.x, -bounds.y, 0f) + areaOffset
                            );
                            Gizmos.DrawLine(
                                new Vector3(-bounds.x, bounds.y, bounds.z) + areaOffset,
                                new Vector3(-bounds.x, -bounds.y, 0f) + areaOffset
                            );
                            break;
                        case SampleDirection.ZPositive:
                            Gizmos.DrawLine(
                                new Vector3(-bounds.x, bounds.y, -bounds.z) + areaOffset,
                                new Vector3(0f, bounds.y, bounds.z) + areaOffset
                            );
                            Gizmos.DrawLine(
                                new Vector3(bounds.x, bounds.y, -bounds.z) + areaOffset,
                                new Vector3(0f, bounds.y, bounds.z) + areaOffset
                            );
                            Gizmos.DrawLine(
                                new Vector3(-bounds.x, bounds.y, -bounds.z) + areaOffset,
                                new Vector3(-bounds.x, 0f, bounds.z) + areaOffset
                            );
                            Gizmos.DrawLine(
                                new Vector3(-bounds.x, -bounds.y, -bounds.z) + areaOffset,
                                new Vector3(-bounds.x, 0f, bounds.z) + areaOffset
                            );
                            break;
                        case SampleDirection.ZNegative:
                            Gizmos.DrawLine(
                                new Vector3(-bounds.x, bounds.y, bounds.z) + areaOffset,
                                new Vector3(0f, bounds.y, -bounds.z) + areaOffset
                            );
                            Gizmos.DrawLine(
                                new Vector3(bounds.x, bounds.y, bounds.z) + areaOffset,
                                new Vector3(0f, bounds.y, -bounds.z) + areaOffset
                            );
                            Gizmos.DrawLine(
                                new Vector3(-bounds.x, bounds.y, bounds.z) + areaOffset,
                                new Vector3(-bounds.x, 0f, -bounds.z) + areaOffset
                            );
                            Gizmos.DrawLine(
                                new Vector3(-bounds.x, -bounds.y, bounds.z) + areaOffset,
                                new Vector3(-bounds.x, 0f, -bounds.z) + areaOffset
                            );
                            break;
                    }
                }
            }
        }
    }

    [System.Serializable]
    public class CVRSpawnableTriggerTask
    {
        public int settingIndex = -1;
        public float settingValue = 0f;
        public float delay = 0f;
        public float holdTime = 0f;
        
        public enum UpdateMethod
        {
            Override = 1,
            Add = 2,
            Subtract = 3,
            Toggle = 4
        }

        public CVRSpawnableTriggerTask.UpdateMethod updateMethod = UpdateMethod.Override;
    }
    
    [System.Serializable]
    public class CVRSpawnableTriggerTaskStay
    {
        public int settingIndex = -1;
        public float minValue = 0f;
        public float maxValue = 1f;
        
        public enum UpdateMethod
        {
            SetFromPosition = 1,
            Add = 2,
            Subtract = 3,
        }
        
        public CVRSpawnableTriggerTaskStay.UpdateMethod updateMethod = UpdateMethod.SetFromPosition;
    }
}