using System;
using System.Collections.Generic;
using ABI.CCK.Components;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(ABI.CCK.Components.CVRObjectSync))]
public class CCK_ObjectSyncEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("This component will have this objects position and rotation synced over network.", MessageType.Info);
        
        //EditorGUILayout.Space();
    }
    
}
