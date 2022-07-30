using System;
using ABI.CCK.Components;
using UnityEditor;
using UnityEngine;

#pragma warning disable

[CustomEditor(typeof(CVRHapticAreaChest))]
public class CVRHapticAreaChestEditor : UnityEditor.Editor
{
    private CVRHapticAreaChest _hapticsChest;
    
    private void OnEnable()
    {
        _hapticsChest = (CVRHapticAreaChest) target;
    }

    private void OnSceneGUI()
    {
        if (_hapticsChest.gameObject.activeInHierarchy)
        {
            Event e = Event.current;
            int controlId = GUIUtility.GetControlID(GetHashCode(), FocusType.Passive);
            
            var wasMouseDown = false;
            var preventControls = false;
            
            if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
            {
                preventControls = true;
            }

            if(Event.current.type == EventType.MouseDown && Event.current.button == 0) {
                preventControls = true;
            }

            if (Event.current.button == 0)
            {
                preventControls = true;
            }
            
            var hapticsTransform = _hapticsChest.transform;
            var scale = hapticsTransform.localScale;
            var inverseScale = new Vector3(1 / scale.x, 1 / scale.y, 1 / scale.z);

            var i = 0;
            var selected = false;
            foreach (var point in _hapticsChest.HapticPoints40)
            {
                var localPoint = point;
                localPoint.Scale(_hapticsChest.chestAreaSize * 0.5f);

                if (i == _hapticsChest.selectedPoint)
                {
                    Handles.color = Color.cyan;
                    
                    Vector3 pos = hapticsTransform.TransformPoint(Vector3.Scale(_hapticsChest.HapticPoints40[i], inverseScale));
                    
                    EditorGUI.BeginChangeCheck();
                    Vector3 pointPosition = Handles.PositionHandle(pos, _hapticsChest.transform.rotation);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(_hapticsChest, "CVR Haptic Chest Point Change");
                        var newLocalPoint = _hapticsChest.transform.InverseTransformPoint(pointPosition);
                        newLocalPoint.Scale(_hapticsChest.transform.lossyScale);
                        Debug.Log(newLocalPoint);
                        //_hapticsChest.HapticPoints40[i].z = newLocalPoint.z;
                    }
                }
                else
                {
                    Handles.color = Color.yellow;
                }

                /*if (Handles.Button(_hapticsChest.transform.TransformPoint(localPoint), _hapticsChest.transform.rotation, 0.01f, 0.01f,
                    Handles.CubeHandleCap))
                {
                    if (_hapticsChest.selectedPoint == i)
                    {
                        _hapticsChest.selectedPoint = -1;
                    }
                    else
                    {
                        _hapticsChest.selectedPoint = i;
                    }
                    selected = true;
                }*/

                i++;
            }

            if (e.type == EventType.Layout && preventControls && selected)
            {
                HandleUtility.AddDefaultControl(controlId);
            }
        }
    }
}