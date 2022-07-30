using System;
using System.Collections.Generic;

using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRAdvancedAvatarSettingsTrigger : MonoBehaviour
    {
        public Vector3 areaSize = new Vector3(0.05f, 0.05f, 0.05f);
        public Vector3 areaOffset = Vector3.zero;
        public string settingName;
        public float settingValue = 0;

        public bool useAdvancedTrigger = false;
        public bool isNetworkInteractable = true;
        [SerializeField]
        public List<CVRPointer> allowedPointer = new List<CVRPointer>();

        public string[] allowedTypes = new string[0];
        public bool allowParticleInteraction = false;

        public List<CVRAdvancedAvatarSettingsTriggerTask> enterTasks = new List<CVRAdvancedAvatarSettingsTriggerTask>();
        public List<CVRAdvancedAvatarSettingsTriggerTask> exitTasks = new List<CVRAdvancedAvatarSettingsTriggerTask>();
        
        public List<CVRAdvancedAvatarSettingsTriggerTaskStay> stayTasks = new List<CVRAdvancedAvatarSettingsTriggerTaskStay>();

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
    public class CVRAdvancedAvatarSettingsTriggerTask
    {
        public string settingName;
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

        public CVRAdvancedAvatarSettingsTriggerTask.UpdateMethod updateMethod = UpdateMethod.Override;
    }
    
    [System.Serializable]
    public class CVRAdvancedAvatarSettingsTriggerTaskStay
    {
        public string settingName;
        public float minValue = 0f;
        public float maxValue = 1f;
        
        public enum UpdateMethod
        {
            SetFromPosition = 1,
            Add = 2,
            Subtract = 3,
        }
        
        public CVRAdvancedAvatarSettingsTriggerTaskStay.UpdateMethod updateMethod = UpdateMethod.SetFromPosition;
    }
}