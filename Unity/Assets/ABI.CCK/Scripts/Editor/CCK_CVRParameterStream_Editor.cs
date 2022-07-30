using System;
using System.Collections.Generic;
using System.Linq;
using ABI.CCK.Components;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using AnimatorControllerParameterType = UnityEngine.AnimatorControllerParameterType;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(CVRParameterStream))]
    public class CCK_CVRParameterStream_Editor : UnityEditor.Editor
    {
        public static string[] coreParameters = {"MovementX", "MovementY", "Grounded", "Emote", "GestureLeft", 
            "GestureRight", "Toggle", "Sitting", "Crouching", "CancelEmote", "Prone", "Flying"};
        
        private CVRParameterStream stream;

        private ReorderableList list;

        private CVRAvatar avatar;
        private CVRSpawnable spawnable;
        
        public override void OnInspectorGUI()
        {
            if (stream == null) stream = (CVRParameterStream) target;

            stream.referenceType = CVRParameterStream.ReferenceType.World;
            
            avatar = stream.transform.GetComponentInParent<CVRAvatar>();
            if (avatar != null) stream.referenceType = CVRParameterStream.ReferenceType.Avatar;
            
            spawnable = stream.transform.GetComponentInParent<CVRSpawnable>();
            if (spawnable != null) stream.referenceType = CVRParameterStream.ReferenceType.Spawnable;

            if (stream.referenceType != CVRParameterStream.ReferenceType.Avatar)
            {
                EditorGUILayout.HelpBox("This Component is currently only supported for usage on Avatars.", MessageType.Warning);
                return;
            }
            
            if (list == null)
            {
                list = new ReorderableList(stream.entries, typeof(CVRParameterStreamEntry), 
                    true, true, true, true);
                list.drawHeaderCallback = DrawHeaderCallback;
                list.drawElementCallback = DrawElementCallback;
                list.elementHeightCallback = ElementHeightCallback;
            }
            
            list.DoLayoutList();
        }

        private float ElementHeightCallback(int index)
        {
            return EditorGUIUtility.singleLineHeight * 1.25f * (((int) stream.entries[index].applicationType % 5 == 1?5f:4f) + 
                   (stream.entries[index].targetType == CVRParameterStreamEntry.TargetType.Animator ? 1f : 0f));
        }

        private void DrawElementCallback(Rect rect, int index, bool isactive, bool isfocused)
        {
            if (index >= stream.entries.Count) return;
        
            rect.y += 2f;

            switch (stream.referenceType)
            {
                case CVRParameterStream.ReferenceType.World:
                    stream.entries[index].type = (CVRParameterStreamEntry.Type) EditorGUI.EnumPopup(
                        new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), 
                        "Type",
                        stream.entries[index].type
                    );

                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                
                    stream.entries[index].targetType = (CVRParameterStreamEntry.TargetType) EditorGUI.Popup(
                        new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), 
                        "Output Type",
                        (int) stream.entries[index].targetType,
                        new []{"Animator", "VariableBuffer"}
                    );

                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                
                    stream.entries[index].target = (GameObject) EditorGUI.ObjectField(
                        new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), 
                        "Target",
                        stream.entries[index].target,
                        typeof(GameObject),
                        true
                    );

                    if (stream.entries[index].target)
                    {
                        switch (stream.entries[index].targetType)
                        {
                            case CVRParameterStreamEntry.TargetType.Animator:
                                var animator = stream.entries[index].target.GetComponent<Animator>();
                                if (animator == null) stream.entries[index].target = null;
                                break;
                            case CVRParameterStreamEntry.TargetType.VariableBuffer:
                                var varBuffer = stream.entries[index].target.GetComponent<CVRVariableBuffer>();
                                if (varBuffer == null) stream.entries[index].target = null;
                                break;
                        }
                    }

                    if (stream.entries[index].target != null && stream.entries[index].targetType == CVRParameterStreamEntry.TargetType.Animator)
                    {
                        rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                        
                        var _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
                        
                        var animator = stream.entries[index].target.GetComponent<Animator>();
                        
                        var parameters = new List<string>();
                        parameters.Add("-none-");
                        var parameterIndex = 0;
                        if (animator != null)
                        {
                            if (animator.runtimeAnimatorController != null)
                            {
                                foreach (var parameter in ((UnityEditor.Animations.AnimatorController) animator.runtimeAnimatorController).parameters)
                                {
                                    if (parameter.type == AnimatorControllerParameterType.Float || 
                                        parameter.type == AnimatorControllerParameterType.Int || 
                                        parameter.type == AnimatorControllerParameterType.Bool)
                                    {
                                        parameters.Add(parameter.name);
                                    }
                                }

                                parameterIndex = parameters.FindIndex(match => match == stream.entries[index].parameterName);
                            }
                        }

                        if (parameterIndex < 0) parameterIndex = 0;
                    
                        parameterIndex = EditorGUI.Popup(_rect, "Parameter", parameterIndex, parameters.ToArray());
                        stream.entries[index].parameterName = parameters[parameterIndex];
                    }
                    break;
                
                case CVRParameterStream.ReferenceType.Avatar:
                    stream.entries[index].type = (CVRParameterStreamEntry.Type) EditorGUI.EnumPopup(
                        new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), 
                        "Type",
                        stream.entries[index].type
                    );

                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                    
                    var _rectA = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
                    
                    EditorGUI.LabelField(_rectA, "Output Type", "AdvancedAvatarAnimator");
                    
                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;

                    stream.entries[index].targetType = CVRParameterStreamEntry.TargetType.AvatarAnimator;
                    
                    _rectA = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

                    var parametersA = new List<string>();
                    parametersA.Add("-none-");
                    var parameterIndexA = 0;
                    if (avatar != null)
                    {
                        if (avatar.overrides != null)
                        {
                            foreach (var parameter in ((UnityEditor.Animations.AnimatorController) avatar.overrides.runtimeAnimatorController).parameters)
                            {
                                if ((parameter.type == AnimatorControllerParameterType.Float || 
                                     parameter.type == AnimatorControllerParameterType.Int || 
                                     parameter.type == AnimatorControllerParameterType.Bool) &&
                                    !coreParameters.Contains(parameter.name))
                                {
                                    parametersA.Add(parameter.name);
                                }
                            }

                            parameterIndexA = parametersA.FindIndex(match => match == stream.entries[index].parameterName);
                        }
                    }

                    if (parameterIndexA < 0) parameterIndexA = 0;
                    
                    parameterIndexA = EditorGUI.Popup(_rectA, "Parameter", parameterIndexA, parametersA.ToArray());
                    stream.entries[index].parameterName = parametersA[parameterIndexA];
                    
                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;

                    stream.entries[index].applicationType = (CVRParameterStreamEntry.ApplicationType) EditorGUI.EnumPopup(
                        new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), 
                        "Value Application",
                        stream.entries[index].applicationType
                    );

                    if ((int) stream.entries[index].applicationType % 5 == 1)
                    {
                        rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                        stream.entries[index].staticValue = EditorGUI.FloatField(
                            new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                            "Static Value",
                            stream.entries[index].staticValue
                        );
                    }
                    
                    break;
                
                case CVRParameterStream.ReferenceType.Spawnable:
                    stream.entries[index].type = (CVRParameterStreamEntry.Type) EditorGUI.EnumPopup(
                        new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), 
                        "Type",
                        stream.entries[index].type
                    );

                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                    
                    var _rectB = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
                    
                    EditorGUI.LabelField(_rectB, "Output Type", "SpawnableCustomFloat");
                    
                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;

                    stream.entries[index].targetType = CVRParameterStreamEntry.TargetType.CustomFloat;
                    
                    _rectB = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

                    var parametersB = new List<string>();
                    parametersB.Add("-none-");
                    var parameterIndexB = 0;
                    if (spawnable != null)
                    {
                        foreach (var parameter in spawnable.syncValues)
                        {
                            if (!String.IsNullOrWhiteSpace(parameter.name))
                            {
                                parametersB.Add(parameter.name);
                            }
                        }

                        parameterIndexB = parametersB.FindIndex(match => match == stream.entries[index].parameterName);
                    }

                    if (parameterIndexB < 0) parameterIndexB = 0;
                    
                    parameterIndexB = EditorGUI.Popup(_rectB, "Parameter", parameterIndexB, parametersB.ToArray());
                    stream.entries[index].parameterName = parametersB[parameterIndexB];
                    break;
            }
        }

        private void DrawHeaderCallback(Rect rect)
        {
            Rect _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            GUI.Label(_rect, "Entries");
        }
    }
}