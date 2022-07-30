using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace ABI.CCK.Components
{
    [System.Serializable]
    public class CVRInteractable : MonoBehaviour
    {
        public string tooltip;
        public List<CVRInteractableAction> actions = new List<CVRInteractableAction>();

        public UnityEvent onEnterSeat;
        public UnityEvent onExitSeat;

        public void CustomTrigger()
        {
            
        }
        
        private void OnDrawGizmos()
        {
            foreach (var action in actions)
            {
                foreach (var operation in action.operations)
                {
                    if (operation.type == CVRInteractableActionOperation.ActionType.TeleportPlayer)
                    {
                        Gizmos.color = Color.green;
                        if(operation.gameObjectVal == null) continue;
                        Gizmos.DrawLine(transform.position, operation.gameObjectVal.transform.position);
                        DrawArrow(operation.gameObjectVal.transform.position, new Vector3(0, operation.gameObjectVal.transform.eulerAngles.y, 0), 1);
                    }
                    
                    if (operation.type == CVRInteractableActionOperation.ActionType.SitAtPosition)
                    {
                        Gizmos.color = Color.blue;
                        if(operation.targets.Count > 0 && operation.targets[0] == null) continue;
                        Gizmos.DrawLine(transform.position, operation.targets[0].transform.position);
                        DrawArrow(operation.targets[0].transform.position, new Vector3(0, operation.targets[0].transform.eulerAngles.y, 0), 0.5f);

                        if (operation.gameObjectVal == null) continue;
                        var position = operation.gameObjectVal.transform;
                        Gizmos.DrawLine(transform.position, position.position);
                        Matrix4x4 rotationMatrix = Matrix4x4.TRS(position.position, position.rotation, Vector3.one);
                        Gizmos.matrix = rotationMatrix;
                        Gizmos.DrawWireCube(new Vector3(+0.12f, -0.2f, 0.05f), new Vector3(0.1f, 0.4f, 0.1f));
                        Gizmos.DrawWireCube(new Vector3(-0.12f, -0.2f, 0.05f), new Vector3(0.1f, 0.4f, 0.1f));
                        Gizmos.DrawWireCube(new Vector3(+0.12f, 0.05f, -0.2f), new Vector3(0.1f, 0.1f, 0.6f));
                        Gizmos.DrawWireCube(new Vector3(-0.12f, 0.05f, -0.2f), new Vector3(0.1f, 0.1f, 0.6f));
                        Gizmos.DrawWireCube(new Vector3(0f, 0.4f, -0.4f), new Vector3(0.34f, 0.6f, 0.2f));
                        Gizmos.DrawWireCube(new Vector3(0f, 0.8f, -0.4f), new Vector3(0.2f, 0.2f, 0.2f));
                        Gizmos.DrawWireCube(new Vector3(+0.22f, 0.4f, -0.4f), new Vector3(0.1f, 0.4f, 0.1f));
                        Gizmos.DrawWireCube(new Vector3(-0.22f, 0.4f, -0.4f), new Vector3(0.1f, 0.4f, 0.1f));
                        Gizmos.DrawWireCube(new Vector3(+0.22f, 0.25f, -0.2f), new Vector3(0.1f, 0.1f, 0.3f));
                        Gizmos.DrawWireCube(new Vector3(-0.22f, 0.25f, -0.2f), new Vector3(0.1f, 0.1f, 0.3f));
                    }
                }
            }
        }

        private void DrawArrow(Vector3 position, Vector3 angle, float size)
        {
            var a1 = position + new Vector3(0, 0.1f * size, 0);
            var a2 = RotatePointAroundPivot(position + new Vector3(0.1f * size, 0, 0), position, angle);
            var a3 = position + new Vector3(0, -0.1f * size, 0);
            var a4 = RotatePointAroundPivot(position + new Vector3(-0.1f * size, 0, 0), position, angle);
            
            var b1 = RotatePointAroundPivot(position + new Vector3(0, 0.1f * size, 0.3f * size), position, angle);
            var b2 = RotatePointAroundPivot(position + new Vector3(0.1f * size, 0, 0.3f * size), position, angle);
            var b3 = RotatePointAroundPivot(position + new Vector3(0, -0.1f * size, 0.3f * size), position, angle);
            var b4 = RotatePointAroundPivot(position + new Vector3(-0.1f * size, 0, 0.3f * size), position, angle);
            
            var c1 = RotatePointAroundPivot(position + new Vector3(0, 0.2f * size, 0.3f * size), position, angle);
            var c2 = RotatePointAroundPivot(position + new Vector3(0.2f * size, 0, 0.3f * size), position, angle);
            var c3 = RotatePointAroundPivot(position + new Vector3(0, -0.2f * size, 0.3f * size), position, angle);
            var c4 = RotatePointAroundPivot(position + new Vector3(-0.2f * size, 0, 0.3f * size), position, angle);
            
            var d = RotatePointAroundPivot(position + new Vector3(0, 0, 0.5f * size), position, angle);
            
            Gizmos.DrawLine(position, a1);
            Gizmos.DrawLine(position, a2);
            Gizmos.DrawLine(position, a3);
            Gizmos.DrawLine(position, a4);
            
            Gizmos.DrawLine(a1, b1);
            Gizmos.DrawLine(a2, b2);
            Gizmos.DrawLine(a3, b3);
            Gizmos.DrawLine(a4, b4);
            
            Gizmos.DrawLine(b1, c1);
            Gizmos.DrawLine(b2, c2);
            Gizmos.DrawLine(b3, c3);
            Gizmos.DrawLine(b4, c4);
            
            Gizmos.DrawLine(c1, d);
            Gizmos.DrawLine(c2, d);
            Gizmos.DrawLine(c3, d);
            Gizmos.DrawLine(c4, d);
        }
        
        private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
        {
            var dir = point - pivot; // get point direction relative to pivot
            dir = Quaternion.Euler(angles) * dir; // rotate it
            point = dir + pivot; // calculate rotated point
            return point; // return it
        }
    }
}