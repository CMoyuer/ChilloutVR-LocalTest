using System;
using System.Collections.Generic;
using System.Reflection;
using ABI.CCK.Components;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(ABI.CCK.Components.CVRInteractable))]
    [CanEditMultipleObjects]
    public class CCK_CVRInteractableEditor : UnityEditor.Editor
    {
        private CVRInteractable _interactable;

        private static string[] gameObjectStates = new string[] {"Enable", "Disable", "Toggle" };
        private static string[] variableComparisions = new string[] {"Buffer -> static", "Buffer -> Buffer" };
        private static string[] variableComparitors = new string[] {"==", ">=", ">", "<", "<=", "!=" };
        private static string[] arithmeticConstellations = new string[] {"Buffer -> static", "Buffer -> Buffer", "Buffer -> Random" };
        private static string[] arithmeticOperators = new string[] {"+", "-", "*", "÷", "mod", "pow", "log"};
        private static string[] timerModes = new string[] {"once on enable", "repeat", "deactivate self"};
        private static string[] spawnableUpdateTypes = new string[] {"Override", "Add", "Subtract", "Toggle"};
        private static string[] apfSetTypes = new string[] {"Static", "Variable Buffer"};
        private static string[] apfSetTypesString = new string[] {"Static", "Property"};
        
        public override void OnInspectorGUI()
        {
            if (_interactable == null) _interactable = (CVRInteractable)target;

            _interactable.tooltip = EditorGUILayout.TextField("Tooltip:", _interactable.tooltip);
            
            EditorGUILayout.LabelField("Triggers:", EditorStyles.boldLabel);

            var actionIndex = 0;
            
            foreach (CVRInteractableAction trigger in _interactable.actions)
            {
                GUILayout.BeginVertical("HelpBox");
                GUILayout.BeginHorizontal ();

                trigger.actionType = (CVRInteractableAction.ActionRegister) EditorGUILayout.EnumPopup("Trigger:", trigger.actionType);
                
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal ();
                GUILayout.BeginVertical ("GroupBox");
                
                trigger.execType = (CVRInteractableAction.ExecutionType) EditorGUILayout.EnumPopup("Broadcast Type:", trigger.execType);

                if (trigger.actionType == CVRInteractableAction.ActionRegister.OnEnterCollider ||
                    trigger.actionType == CVRInteractableAction.ActionRegister.OnExitCollider ||
                    trigger.actionType == CVRInteractableAction.ActionRegister.OnEnterTrigger ||
                    trigger.actionType == CVRInteractableAction.ActionRegister.OnExitTrigger)
                {
                    LayerMask tempMask = EditorGUILayout.MaskField("Layer:", InternalEditorUtility.LayerMaskToConcatenatedLayersMask(trigger.layerMask), InternalEditorUtility.layers);
                    trigger.layerMask = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(tempMask);
                }

                if (trigger.actionType == CVRInteractableAction.ActionRegister.OnTimer)
                {
                    trigger.floatVal = EditorGUILayout.FloatField("Seconds:", trigger.floatVal);
                    trigger.floatVal2 = EditorGUILayout.Popup("Mode:", (int)trigger.floatVal2, timerModes);
                }
                
                if (trigger.actionType == CVRInteractableAction.ActionRegister.OnInteractDown ||
                    trigger.actionType == CVRInteractableAction.ActionRegister.OnInteractUp)
                {
                    trigger.floatVal = EditorGUILayout.FloatField("Distance:", trigger.floatVal);
                }
                
                if (trigger.actionType == CVRInteractableAction.ActionRegister.OnVariableBufferUpdate)
                {
                    trigger.varBufferVal = (CVRVariableBuffer) EditorGUILayout.ObjectField("Value:", trigger.varBufferVal, typeof(CVRVariableBuffer), true);
                }
                
                if (trigger.actionType == CVRInteractableAction.ActionRegister.OnVariableBufferComparision)
                {
                    trigger.floatVal = EditorGUILayout.Popup("Type:", (int)trigger.floatVal, variableComparisions);
                    
                    trigger.varBufferVal = (CVRVariableBuffer) EditorGUILayout.ObjectField("Value 1:", trigger.varBufferVal, typeof(CVRVariableBuffer), true);

                    if (trigger.varBufferVal != null)
                    {
                        trigger.varBufferVal.AddInteracteable(_interactable);
                    }
                    
                    trigger.floatVal2 = EditorGUILayout.Popup("Comparitor:", (int)trigger.floatVal2, variableComparitors);
                    
                    if (trigger.floatVal == 0)
                    {
                        trigger.floatVal3 = EditorGUILayout.FloatField("Value 2:", trigger.floatVal3);
                    }
                    else
                    {
                        trigger.varBufferVal2 = (CVRVariableBuffer) EditorGUILayout.ObjectField("Value 2:", trigger.varBufferVal2, typeof(CVRVariableBuffer), true);
                        
                        if (trigger.varBufferVal2 != null)
                        {
                            trigger.varBufferVal2.AddInteracteable(_interactable);
                        }
                    }
                }

                if (trigger.actionType == CVRInteractableAction.ActionRegister.OnCron)
                {
                    trigger.stringVal = EditorGUILayout.TextField("Cron String", trigger.stringVal);
                }
                
                if (trigger.actionType == CVRInteractableAction.ActionRegister.OnWorldTrigger)
                {
                    trigger.floatVal = EditorGUILayout.IntField("Index", (int) trigger.floatVal);
                }

                if (trigger.actionType == CVRInteractableAction.ActionRegister.OnParticleHit)
                {
                    GUIStyle foldoutStyle = new GUIStyle (EditorStyles.foldout);
                    foldoutStyle.margin.left = 12;
                    GUIStyle areaStyle = new GUIStyle ();
                    areaStyle.margin.left = 20;

                    trigger.specificParticleSystemsCollapse = EditorGUILayout.Foldout(trigger.specificParticleSystemsCollapse, "Specific Particle Systems", foldoutStyle);

                    EditorGUILayout.BeginVertical(areaStyle);
                    
                    if (trigger.specificParticleSystemsCollapse)
                    {
                        int pointerCount = Mathf.Max(0, EditorGUILayout.IntField("Size", trigger.specificParticleSystems.Count));
                        while (pointerCount < trigger.specificParticleSystems.Count)
                            trigger.specificParticleSystems.RemoveAt(trigger.specificParticleSystems.Count - 1);
                        while (pointerCount > trigger.specificParticleSystems.Count)
                            trigger.specificParticleSystems.Add(null);

                        for (int i = 0; i < trigger.specificParticleSystems.Count; i++)
                        {
                            trigger.specificParticleSystems[i] = (ParticleSystem) EditorGUILayout.ObjectField("Element " + i,
                                trigger.specificParticleSystems[i], typeof(ParticleSystem));
                        }
                    }

                    EditorGUILayout.EndVertical();
                }

                if (trigger.actionType == CVRInteractableAction.ActionRegister.OnPointerEnter)
                {
                    GUIStyle foldoutStyle = new GUIStyle (EditorStyles.foldout);
                    foldoutStyle.margin.left = 12;
                    GUIStyle areaStyle = new GUIStyle ();
                    areaStyle.margin.left = 20;

                    trigger.allowedPointerCollapse = EditorGUILayout.Foldout(trigger.allowedPointerCollapse, "Allowed Pointers", foldoutStyle);

                    EditorGUILayout.BeginVertical(areaStyle);
                    
                    if (trigger.allowedPointerCollapse)
                    {
                        int pointerCount = Mathf.Max(0, EditorGUILayout.IntField("Size", trigger.allowedPointer.Count));
                        while (pointerCount < trigger.allowedPointer.Count)
                            trigger.allowedPointer.RemoveAt(trigger.allowedPointer.Count - 1);
                        while (pointerCount > trigger.allowedPointer.Count)
                            trigger.allowedPointer.Add(null);

                        for (int i = 0; i < trigger.allowedPointer.Count; i++)
                        {
                            trigger.allowedPointer[i] = (CVRPointer) EditorGUILayout.ObjectField("Element " + i,
                                trigger.allowedPointer[i], typeof(CVRPointer));
                        }
                    }

                    EditorGUILayout.EndVertical();
                    
                    trigger.allowedTypesCollapse = EditorGUILayout.Foldout(trigger.allowedTypesCollapse, "Allowed Types", foldoutStyle);
                    
                    EditorGUILayout.BeginVertical(areaStyle);
                    
                    if (trigger.allowedTypesCollapse)
                    {
                        int pointerCount = Mathf.Max(0, EditorGUILayout.IntField("Size", trigger.allowedTypes.Count));
                        while (pointerCount < trigger.allowedTypes.Count)
                            trigger.allowedTypes.RemoveAt(trigger.allowedTypes.Count - 1);
                        while (pointerCount > trigger.allowedTypes.Count)
                            trigger.allowedTypes.Add(null);

                        for (int i = 0; i < trigger.allowedTypes.Count; i++)
                        {
                            trigger.allowedTypes[i] = EditorGUILayout.TextField("Element " + i,
                                trigger.allowedTypes[i]);
                        }
                    }

                    EditorGUILayout.EndVertical();
                }

                if (trigger.actionType == CVRInteractableAction.ActionRegister.OnInputDown ||
                    trigger.actionType == CVRInteractableAction.ActionRegister.OnInputUp)
                {
                    trigger.interactionFilter = (CVRInteractableAction.InteractionFilter) EditorGUILayout.EnumPopup("Interaction Filter", trigger.interactionFilter);
                    
                    trigger.interactionInput = (CVRInteractableAction.InteractionInput) EditorGUILayout.EnumPopup("Interaction Input", trigger.interactionInput);
                    
                    trigger.interactionInputModifier = (CVRInteractableAction.InteractionInputModifier) EditorGUILayout.EnumFlagsField("Interaction Input Modifier", trigger.interactionInputModifier);
                }

                if (trigger.actionType == CVRInteractableAction.ActionRegister.OnAPFTrigger ||
                    trigger.actionType == CVRInteractableAction.ActionRegister.OnAPFBoolChange ||
                    trigger.actionType == CVRInteractableAction.ActionRegister.OnAPFIntChange ||
                    trigger.actionType == CVRInteractableAction.ActionRegister.OnAPFFloatChange ||
                    trigger.actionType == CVRInteractableAction.ActionRegister.OnAPFStringChange)
                {
                    trigger.execType = CVRInteractableAction.ExecutionType.LocalNotNetworked;

                    trigger.stringVal = EditorGUILayout.TextField("Key", trigger.stringVal);
                }
                else
                {
                    trigger.delay = EditorGUILayout.FloatField("Delay (Seconds):", trigger.delay);
                }

                if(trigger.varBufferVal != null) trigger.varBufferVal.AddInteracteable(_interactable);
                if(trigger.varBufferVal2 != null) trigger.varBufferVal2.AddInteracteable(_interactable);
                
                EditorGUILayout.LabelField("Actions:", EditorStyles.boldLabel);

                for (int j = 0; j < trigger.operations.Count; j++)
                {
                    GUILayout.BeginVertical ("GroupBox");
                    
                    trigger.operations[j].type = (CVRInteractableActionOperation.ActionType) EditorGUILayout.EnumPopup("Action Type:", trigger.operations[j].type);

                    switch (trigger.operations[j].type)
                    {
                        case CVRInteractableActionOperation.ActionType.SetGameObjectActive:

                            if (trigger.operations[j].targets.Count == 0)
                            {
                                trigger.operations[j].targets.Add(null);
                            }

                            if (trigger.operations[j].floatVal > 2)
                            {
                                trigger.operations[j].floatVal = 0;
                            }

                            RenderTargets(trigger.operations[j].targets);

                            trigger.operations[j].floatVal = EditorGUILayout.Popup("State:", (int)trigger.operations[j].floatVal, gameObjectStates);

                            break;
                        
                        case CVRInteractableActionOperation.ActionType.TeleportPlayer:

                            trigger.operations[j].gameObjectVal = (GameObject)EditorGUILayout.ObjectField(
                                "Target Location:", 
                                trigger.operations[j].gameObjectVal, 
                                typeof(GameObject), 
                                true
                            );
                            
                            break;
                        
                        case CVRInteractableActionOperation.ActionType.SetAnimatorBoolValue:

                            RenderTargets(trigger.operations[j].targets);

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("Parameter Name:", trigger.operations[j].stringVal);
                            
                            trigger.operations[j].boolVal = EditorGUILayout.Toggle("Value", trigger.operations[j].boolVal);
                            
                            break;
                        
                        case CVRInteractableActionOperation.ActionType.SetAnimatorFloatValue:

                            RenderTargets(trigger.operations[j].targets);

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("Parameter Name:", trigger.operations[j].stringVal);
                            
                            trigger.operations[j].floatVal = EditorGUILayout.FloatField("Value", trigger.operations[j].floatVal);
                            
                            break;
                        
                        case CVRInteractableActionOperation.ActionType.SetAnimatorIntValue:

                            RenderTargets(trigger.operations[j].targets);

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("Parameter Name:", trigger.operations[j].stringVal);
                            
                            trigger.operations[j].floatVal = EditorGUILayout.FloatField("Value", trigger.operations[j].floatVal);
                            
                            break;
                        
                        case CVRInteractableActionOperation.ActionType.TriggerAnimatorTrigger:

                            RenderTargets(trigger.operations[j].targets);

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("Trigger Name:", trigger.operations[j].stringVal);

                            break;
                        
                        case CVRInteractableActionOperation.ActionType.SpawnObject:

                            RenderTargets(trigger.operations[j].targets, "Object");

                            trigger.operations[j].gameObjectVal = (GameObject)EditorGUILayout.ObjectField(
                                "Target Location:", 
                                trigger.operations[j].gameObjectVal, 
                                typeof(GameObject), 
                                true
                            );

                            trigger.operations[j].boolVal = EditorGUILayout.Toggle("Auto Pickup", trigger.operations[j].boolVal);
                            trigger.operations[j].boolVal2 = EditorGUILayout.Toggle("Auto Attach", trigger.operations[j].boolVal2);
                            
                            break;
                        
                        case CVRInteractableActionOperation.ActionType.TeleportObject:

                            RenderTargets(trigger.operations[j].targets, "Object");

                            trigger.operations[j].gameObjectVal = (GameObject)EditorGUILayout.ObjectField(
                                "Target Location:", 
                                trigger.operations[j].gameObjectVal, 
                                typeof(GameObject), 
                                true
                            );
                            
                            break;
                        
                        case CVRInteractableActionOperation.ActionType.ToggleAnimatorBoolValue:

                            RenderTargets(trigger.operations[j].targets);

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("Parameter Name:", trigger.operations[j].stringVal);

                            break;
                        
                        case CVRInteractableActionOperation.ActionType.SetAnimatorFloatRandom:

                            RenderTargets(trigger.operations[j].targets);

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("Parameter Name:", trigger.operations[j].stringVal);
                            
                            trigger.operations[j].floatVal = EditorGUILayout.FloatField("Min:", trigger.operations[j].floatVal);
                            
                            trigger.operations[j].floatVal2 = EditorGUILayout.FloatField("Max:", trigger.operations[j].floatVal2);

                            break;

                        case CVRInteractableActionOperation.ActionType.SetAnimatorIntRandom:

                            RenderTargets(trigger.operations[j].targets);

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("Parameter Name:", trigger.operations[j].stringVal);
                            
                            trigger.operations[j].floatVal = EditorGUILayout.FloatField("Min:", trigger.operations[j].floatVal);
                            
                            trigger.operations[j].floatVal2 = EditorGUILayout.FloatField("Max:", trigger.operations[j].floatVal2);

                            break;
                        
                        case CVRInteractableActionOperation.ActionType.SetAnimatorBoolRandom:

                            RenderTargets(trigger.operations[j].targets);

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("Parameter Name:", trigger.operations[j].stringVal);
                            
                            trigger.operations[j].floatVal = EditorGUILayout.FloatField("Chance (0-1):", trigger.operations[j].floatVal);

                            break;
                        
                        case CVRInteractableActionOperation.ActionType.VariableBufferArithmetic:

                            trigger.operations[j].floatVal = EditorGUILayout.Popup("Type:", (int)trigger.operations[j].floatVal, arithmeticConstellations);
                            
                            trigger.operations[j].varBufferVal = (CVRVariableBuffer) EditorGUILayout.ObjectField(
                                "Value 1:", 
                                trigger.operations[j].varBufferVal, 
                                typeof(CVRVariableBuffer), 
                                true
                            );
                            
                            trigger.operations[j].floatVal2 = EditorGUILayout.Popup("Operator:", (int)trigger.operations[j].floatVal2, arithmeticOperators);

                            switch ((int) trigger.operations[j].floatVal)
                            {
                                case 0:
                                    trigger.operations[j].floatVal3 = EditorGUILayout.FloatField("Value 2:", trigger.operations[j].floatVal3);
                                    break;
                                case 1:
                                    trigger.operations[j].varBufferVal2 = (CVRVariableBuffer) EditorGUILayout.ObjectField(
                                        "Value 2:", 
                                        trigger.operations[j].varBufferVal2, 
                                        typeof(CVRVariableBuffer), 
                                        true
                                    );
                                    break;
                                case 2:
                                    trigger.operations[j].floatVal3 = EditorGUILayout.FloatField("Min:", trigger.operations[j].floatVal3);
                                    trigger.operations[j].floatVal4 = EditorGUILayout.FloatField("Max:", trigger.operations[j].floatVal4);
                                    break;
                            }
                            
                            trigger.operations[j].varBufferVal3 = (CVRVariableBuffer) EditorGUILayout.ObjectField(
                                "Result:", 
                                trigger.operations[j].varBufferVal3, 
                                typeof(CVRVariableBuffer), 
                                true
                            );

                            break;
                        
                        case CVRInteractableActionOperation.ActionType.SetAnimatorFloatByVar:

                            RenderTargets(trigger.operations[j].targets);

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("Parameter Name:", trigger.operations[j].stringVal);
                            
                            trigger.operations[j].varBufferVal = (CVRVariableBuffer) EditorGUILayout.ObjectField(
                                "Value:", 
                                trigger.operations[j].varBufferVal, 
                                typeof(CVRVariableBuffer), 
                                true
                            );
                            
                            break;
                        
                        case CVRInteractableActionOperation.ActionType.SetAnimatorIntByVar:

                            RenderTargets(trigger.operations[j].targets);

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("Parameter Name:", trigger.operations[j].stringVal);
                            
                            trigger.operations[j].varBufferVal = (CVRVariableBuffer) EditorGUILayout.ObjectField(
                                "Value:", 
                                trigger.operations[j].varBufferVal, 
                                typeof(CVRVariableBuffer), 
                                true
                            );
                            
                            break;
                        
                        case CVRInteractableActionOperation.ActionType.DisplayWorldDetailPage:

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("World GUID:", trigger.operations[j].stringVal);
                            
                            break;
                        
                        case CVRInteractableActionOperation.ActionType.DisplayInstanceDetailPage:

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("Instance GUID:", trigger.operations[j].stringVal);
                            
                            break;
                        
                        case CVRInteractableActionOperation.ActionType.DisplayAvatarDetailPage:

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("Avatar GUID:", trigger.operations[j].stringVal);
                            
                            break;
                        
                        case CVRInteractableActionOperation.ActionType.SitAtPosition:

                            trigger.operations[j].gameObjectVal = (GameObject)EditorGUILayout.ObjectField(
                                "Sitting Location:", 
                                trigger.operations[j].gameObjectVal, 
                                typeof(GameObject), 
                                true
                            );
                            
                            trigger.operations[j].targets[0] = (GameObject)EditorGUILayout.ObjectField(
                                "Exit Location:", 
                                trigger.operations[j].targets[0], 
                                typeof(GameObject), 
                                true
                            );
                            
                            trigger.operations[j].animationVal = (AnimationClip)EditorGUILayout.ObjectField(
                                "Overwrite Animation:", 
                                trigger.operations[j].animationVal, 
                                typeof(AnimationClip), 
                                true
                            );
                            
                            serializedObject.Update();
                            EditorGUILayout.PropertyField(serializedObject.FindProperty("onEnterSeat"), true);
                            serializedObject.ApplyModifiedProperties();
                            
                            serializedObject.Update();
                            EditorGUILayout.PropertyField(serializedObject.FindProperty("onExitSeat"), true);
                            serializedObject.ApplyModifiedProperties();

                            trigger.operations[j].boolVal = EditorGUILayout.Toggle("Lock Controls", trigger.operations[j].boolVal);
                            
                            break;
                        
                        case CVRInteractableActionOperation.ActionType.MethodCall:
                            var currentOperation = serializedObject.FindProperty("actions")
                                .GetArrayElementAtIndex(actionIndex).FindPropertyRelative("operations")
                                .GetArrayElementAtIndex(j).FindPropertyRelative("customEvent");
                            serializedObject.Update();
                            EditorGUILayout.PropertyField(currentOperation, true);
                            serializedObject.ApplyModifiedProperties();
                            break;
                        case CVRInteractableActionOperation.ActionType.SetSpawnableValue:

                            var spawnable = _interactable.GetComponentInParent<CVRSpawnable>();
                            var spawnableParameters = new List<string>();
                            spawnableParameters.Add("-none-");
            
                            if (spawnable == null || !spawnable.useAdditionalValues)
                            {
                                EditorGUILayout.HelpBox("No Spawnable detected or it does not use additional Values.", MessageType.Error);
                            }
                            else
                            {
                                foreach (var syncValue in spawnable.syncValues)
                                {
                                    spawnableParameters.Add(syncValue.name);
                                }
                                
                                trigger.operations[j].floatVal2 = EditorGUILayout.Popup("Parameter", (int) trigger.operations[j].floatVal2 + 1, spawnableParameters.ToArray()) - 1;
                            
                                trigger.operations[j].floatVal = EditorGUILayout.FloatField("Value", trigger.operations[j].floatVal);

                                trigger.operations[j].floatVal3 = EditorGUILayout.Popup("Update Method", (int) trigger.operations[j].floatVal3, spawnableUpdateTypes);
                            }

                            break;
                        case CVRInteractableActionOperation.ActionType.PlayAudio:
                        case CVRInteractableActionOperation.ActionType.StopAudio:

                            var go = (GameObject) EditorGUILayout.ObjectField("Audio Source", trigger.operations[j].gameObjectVal, typeof(GameObject), true);
                            if (go != null && go.GetComponent<AudioSource>() != null)
                            {
                                trigger.operations[j].gameObjectVal = go;
                            }
                            
                            break;
                        case CVRInteractableActionOperation.ActionType.SetAnimatorBoolByAPF:
                        case CVRInteractableActionOperation.ActionType.SetAnimatorIntByAPF:
                        case CVRInteractableActionOperation.ActionType.SetAnimatorFloatByAPF:
                            RenderTargets(trigger.operations[j].targets);

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("Parameter Name:", trigger.operations[j].stringVal);
                            break;
                        case CVRInteractableActionOperation.ActionType.SetVariableBufferByAPF:
                            trigger.operations[j].varBufferVal = (CVRVariableBuffer) EditorGUILayout.ObjectField(
                                "Buffer:", 
                                trigger.operations[j].varBufferVal, 
                                typeof(CVRVariableBuffer), 
                                true
                            );
                            break;
                        case CVRInteractableActionOperation.ActionType.UpdateAPFTrigger:

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("Key", trigger.operations[j].stringVal);

                            break;
                        case CVRInteractableActionOperation.ActionType.UpdateAPFBool:

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("Key", trigger.operations[j].stringVal);

                            trigger.operations[j].floatVal = EditorGUILayout.Popup("Set via", (int) trigger.operations[j].floatVal, apfSetTypes);

                            if ((int) trigger.operations[j].floatVal == 0)
                            {
                                trigger.operations[j].boolVal = EditorGUILayout.Toggle("Value", trigger.operations[j].boolVal);
                            }
                            else
                            {
                                trigger.operations[j].varBufferVal = (CVRVariableBuffer) EditorGUILayout.ObjectField(
                                    "Value:", 
                                    trigger.operations[j].varBufferVal, 
                                    typeof(CVRVariableBuffer), 
                                    true
                                );
                            }
                            
                            break;
                        case CVRInteractableActionOperation.ActionType.UpdateAPFInt:

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("Key", trigger.operations[j].stringVal);

                            trigger.operations[j].floatVal = EditorGUILayout.Popup("Set via", (int) trigger.operations[j].floatVal, apfSetTypes);

                            if ((int) trigger.operations[j].floatVal == 0)
                            {
                                trigger.operations[j].floatVal2 = EditorGUILayout.IntField("Value", (int) trigger.operations[j].floatVal2);
                            }
                            else
                            {
                                trigger.operations[j].varBufferVal = (CVRVariableBuffer) EditorGUILayout.ObjectField(
                                    "Value:", 
                                    trigger.operations[j].varBufferVal, 
                                    typeof(CVRVariableBuffer), 
                                    true
                                );
                            }

                            break;
                        case CVRInteractableActionOperation.ActionType.UpdateAPFFloat:

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("Key", trigger.operations[j].stringVal);

                            trigger.operations[j].floatVal = EditorGUILayout.Popup("Set via", (int) trigger.operations[j].floatVal, apfSetTypes);

                            if ((int) trigger.operations[j].floatVal == 0)
                            {
                                trigger.operations[j].floatVal2 = EditorGUILayout.FloatField("Value", trigger.operations[j].floatVal2);
                            }
                            else
                            {
                                trigger.operations[j].varBufferVal = (CVRVariableBuffer) EditorGUILayout.ObjectField(
                                    "Value:", 
                                    trigger.operations[j].varBufferVal, 
                                    typeof(CVRVariableBuffer), 
                                    true
                                );
                            }
                            
                            break;
                        case CVRInteractableActionOperation.ActionType.UpdateAPFString:

                            trigger.operations[j].stringVal = EditorGUILayout.TextField("Key", trigger.operations[j].stringVal);

                            trigger.operations[j].floatVal = EditorGUILayout.Popup("Set via", (int) trigger.operations[j].floatVal, apfSetTypesString);

                            if ((int) trigger.operations[j].floatVal == 0)
                            {
                                trigger.operations[j].stringVal2 = EditorGUILayout.TextField("Value", trigger.operations[j].stringVal2);
                            }
                            else
                            {
                                trigger.operations[j].gameObjectVal = (GameObject) EditorGUILayout.ObjectField(
                                    "Target:", 
                                    trigger.operations[j].gameObjectVal, 
                                    typeof(GameObject), 
                                    true
                                );
                                
                                //Get component Type
                                if (trigger.operations[j].gameObjectVal == null)
                                {
                                    EditorGUILayout.HelpBox("Select a Target to proceed.", MessageType.Info);
                                    break;
                                }

                                var rawComponents = trigger.operations[j].gameObjectVal.GetComponents<Component>();
                                List<Type> componentListTypes = new List<Type>();
                                List<string> componentListNames = new List<string>();
                                List<string> componentListAssemblyNames = new List<string>();
                                int selectedIndex = 0;

                                foreach (var rawComponent in rawComponents)
                                {
                                    if (rawComponent != null && !componentListTypes.Contains(rawComponent.GetType()) && rawComponent.GetType() != typeof(Transform) && rawComponent.GetType() != typeof(CVRInteractable))
                                    {
                                        componentListTypes.Add(rawComponent.GetType());
                                        componentListNames.Add(rawComponent.GetType().Name);
                                        componentListAssemblyNames.Add(rawComponent.GetType().AssemblyQualifiedName);
                                    }
                                }

                                selectedIndex = componentListAssemblyNames.FindIndex(match => match == trigger.operations[j].stringVal3);
        
                                selectedIndex = EditorGUILayout.Popup("Component", selectedIndex, componentListNames.ToArray());
                                if (selectedIndex >= 0 && selectedIndex < componentListTypes.Count)
                                {
                                    trigger.operations[j].stringVal3 = componentListTypes[selectedIndex].AssemblyQualifiedName;
                                }
                                else
                                {
                                    trigger.operations[j].stringVal3 = "";
                                }
                                
                                //Get Component Property
                                if (trigger.operations[j].stringVal3 == "")
                                {
                                    EditorGUILayout.HelpBox("Select a Component to proceed.", MessageType.Info);
                                    break;
                                }
                                
                                BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default;
                                FieldInfo[] finfos = componentListTypes[selectedIndex].GetFields(flags);
                                PropertyInfo[] pinfos = componentListTypes[selectedIndex].GetProperties(flags);
                                List<string> fieldListNames = new List<string>();
                                List<bool> isProperty = new List<bool>();
                                selectedIndex = 0;
                                
                                foreach (var finfo in finfos)
                                {
                                    if (finfo.IsPublic)
                                    {
                                        if (finfo.FieldType == typeof(string))
                                        {
                                            fieldListNames.Add(finfo.Name);
                                            isProperty.Add(false);
                                        }
                                    }
                                }
                                
                                foreach (var pinfo in pinfos)
                                {
                                    if (pinfo.CanWrite)
                                    {
                                        if (pinfo.PropertyType == typeof(string))
                                        {
                                            fieldListNames.Add(pinfo.Name);
                                            isProperty.Add(true);
                                        }
                                    }
                                }
                                
                                selectedIndex = fieldListNames.FindIndex(match => match == trigger.operations[j].stringVal4);

                                selectedIndex = EditorGUILayout.Popup("Property", selectedIndex, fieldListNames.ToArray());
                                if (selectedIndex >= 0 && selectedIndex < fieldListNames.Count)
                                {
                                    trigger.operations[j].stringVal4 = fieldListNames[selectedIndex];
                                    trigger.operations[j].boolVal = isProperty[selectedIndex];
                                }
                                else
                                {
                                    trigger.operations[j].stringVal4 = "";
                                }
                            }
                            
                            break;
                        case CVRInteractableActionOperation.ActionType.SetPropertyByApf:

                            trigger.operations[j].gameObjectVal = (GameObject) EditorGUILayout.ObjectField(
                                "Target:", 
                                trigger.operations[j].gameObjectVal, 
                                typeof(GameObject), 
                                true
                            );
                            
                            //Get component Type
                            if (trigger.operations[j].gameObjectVal == null)
                            {
                                EditorGUILayout.HelpBox("Select a Target to proceed.", MessageType.Info);
                                break;
                            }

                            var rawComponents2 = trigger.operations[j].gameObjectVal.GetComponents<Component>();
                            List<Type> componentListTypes2 = new List<Type>();
                            List<string> componentListNames2 = new List<string>();
                            List<string> componentListAssemblyNames2 = new List<string>();
                            int selectedIndex2 = 0;

                            foreach (var rawComponent in rawComponents2)
                            {
                                if (rawComponent != null && !componentListTypes2.Contains(rawComponent.GetType()) && rawComponent.GetType() != typeof(Transform) && rawComponent.GetType() != typeof(CVRInteractable))
                                {
                                    componentListTypes2.Add(rawComponent.GetType());
                                    componentListNames2.Add(rawComponent.GetType().Name);
                                    componentListAssemblyNames2.Add(rawComponent.GetType().AssemblyQualifiedName);
                                }
                            }

                            selectedIndex2 = componentListAssemblyNames2.FindIndex(match => match == trigger.operations[j].stringVal3);
    
                            selectedIndex2 = EditorGUILayout.Popup("Component", selectedIndex2, componentListNames2.ToArray());
                            if (selectedIndex2 >= 0 && selectedIndex2 < componentListTypes2.Count)
                            {
                                trigger.operations[j].stringVal3 = componentListTypes2[selectedIndex2].AssemblyQualifiedName;
                            }
                            else
                            {
                                trigger.operations[j].stringVal3 = "";
                            }
                            
                            //Get Component Property
                            if (trigger.operations[j].stringVal3 == "")
                            {
                                EditorGUILayout.HelpBox("Select a Component to proceed.", MessageType.Info);
                                break;
                            }
                            
                            BindingFlags flags2 = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default;
                            FieldInfo[] finfos2 = componentListTypes2[selectedIndex2].GetFields(flags2);
                            PropertyInfo[] pinfos2 = componentListTypes2[selectedIndex2].GetProperties(flags2);
                            List<string> fieldListNames2 = new List<string>();
                            List<bool> isProperty2 = new List<bool>();
                            selectedIndex2 = 0;
                            
                            foreach (var finfo in finfos2)
                            {
                                if (finfo.IsPublic)
                                {
                                    switch (trigger.actionType)
                                    {
                                        case CVRInteractableAction.ActionRegister.OnAPFBoolChange:
                                            if (finfo.FieldType == typeof(bool))
                                            {
                                                fieldListNames2.Add(finfo.Name);
                                                isProperty2.Add(false);
                                            }
                                            break;
                                        case CVRInteractableAction.ActionRegister.OnAPFFloatChange:
                                            if (finfo.FieldType == typeof(float))
                                            {
                                                fieldListNames2.Add(finfo.Name);
                                                isProperty2.Add(false);
                                            }
                                            break;
                                        case CVRInteractableAction.ActionRegister.OnAPFIntChange:
                                            if (finfo.FieldType == typeof(int))
                                            {
                                                fieldListNames2.Add(finfo.Name);
                                                isProperty2.Add(false);
                                            }
                                            break;
                                        default:
                                            if (finfo.FieldType == typeof(string))
                                            {
                                                fieldListNames2.Add(finfo.Name);
                                                isProperty2.Add(false);
                                            }
                                            break;
                                    }
                                }
                            }
                            
                            foreach (var pinfo in pinfos2)
                            {
                                if (pinfo.CanWrite)
                                {
                                    switch (trigger.actionType)
                                    {
                                        case CVRInteractableAction.ActionRegister.OnAPFBoolChange:
                                            if (pinfo.PropertyType == typeof(bool))
                                            {
                                                fieldListNames2.Add(pinfo.Name);
                                                isProperty2.Add(true);
                                            }
                                            break;
                                        case CVRInteractableAction.ActionRegister.OnAPFFloatChange:
                                            if (pinfo.PropertyType == typeof(float))
                                            {
                                                fieldListNames2.Add(pinfo.Name);
                                                isProperty2.Add(true);
                                            }
                                            break;
                                        case CVRInteractableAction.ActionRegister.OnAPFIntChange:
                                            if (pinfo.PropertyType == typeof(int))
                                            {
                                                fieldListNames2.Add(pinfo.Name);
                                                isProperty2.Add(true);
                                            }
                                            break;
                                        default:
                                            if (pinfo.PropertyType == typeof(string))
                                            {
                                                fieldListNames2.Add(pinfo.Name);
                                                isProperty2.Add(true);
                                            }
                                            break;
                                    }
                                }
                            }
                            
                            selectedIndex2 = fieldListNames2.FindIndex(match => match == trigger.operations[j].stringVal4);

                            selectedIndex2 = EditorGUILayout.Popup("Property", selectedIndex2, fieldListNames2.ToArray());
                            if (selectedIndex2 >= 0 && selectedIndex2 < fieldListNames2.Count)
                            {
                                trigger.operations[j].stringVal4 = fieldListNames2[selectedIndex2];
                                trigger.operations[j].boolVal = isProperty2[selectedIndex2];
                            }
                            else
                            {
                                trigger.operations[j].stringVal4 = "";
                            }

                            break;
                        case CVRInteractableActionOperation.ActionType.SetPropertyByValue:

                            trigger.operations[j].varBufferVal = (CVRVariableBuffer) EditorGUILayout.ObjectField("Variable Buffer", trigger.operations[j].varBufferVal, typeof(CVRVariableBuffer));
                            
                            trigger.operations[j].gameObjectVal = (GameObject) EditorGUILayout.ObjectField(
                                "Target:", 
                                trigger.operations[j].gameObjectVal, 
                                typeof(GameObject), 
                                true
                            );
                            
                            //Get component Type
                            if (trigger.operations[j].gameObjectVal == null)
                            {
                                EditorGUILayout.HelpBox("Select a Target to proceed.", MessageType.Info);
                                break;
                            }

                            var rawComponents3 = trigger.operations[j].gameObjectVal.GetComponents<Component>();
                            List<Type> componentListTypes3 = new List<Type>();
                            List<string> componentListNames3 = new List<string>();
                            List<string> componentListAssemblyNames3 = new List<string>();
                            int selectedIndex3 = 0;

                            foreach (var rawComponent in rawComponents3)
                            {
                                if (rawComponent != null && !componentListTypes3.Contains(rawComponent.GetType()) && rawComponent.GetType() != typeof(Transform) && rawComponent.GetType() != typeof(CVRInteractable))
                                {
                                    componentListTypes3.Add(rawComponent.GetType());
                                    componentListNames3.Add(rawComponent.GetType().Name);
                                    componentListAssemblyNames3.Add(rawComponent.GetType().AssemblyQualifiedName);
                                }
                            }

                            selectedIndex3 = componentListAssemblyNames3.FindIndex(match => match == trigger.operations[j].stringVal3);
    
                            selectedIndex3 = EditorGUILayout.Popup("Component", selectedIndex3, componentListNames3.ToArray());
                            if (selectedIndex3 >= 0 && selectedIndex3 < componentListTypes3.Count)
                            {
                                trigger.operations[j].stringVal3 = componentListTypes3[selectedIndex3].AssemblyQualifiedName;
                            }
                            else
                            {
                                trigger.operations[j].stringVal3 = "";
                            }
                            
                            //Get Component Property
                            if (trigger.operations[j].stringVal3 == "")
                            {
                                EditorGUILayout.HelpBox("Select a Component to proceed.", MessageType.Info);
                                break;
                            }
                            
                            BindingFlags flags3 = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default;
                            FieldInfo[] finfos3 = componentListTypes3[selectedIndex3].GetFields(flags3);
                            PropertyInfo[] pinfos3 = componentListTypes3[selectedIndex3].GetProperties(flags3);
                            List<string> fieldListNames3 = new List<string>();
                            List<bool> isProperty3 = new List<bool>();
                            selectedIndex3 = 0;
                            
                            foreach (var finfo in finfos3)
                            {
                                if (finfo.IsPublic)
                                {
                                    if (finfo.FieldType == typeof(float))
                                    {
                                        fieldListNames3.Add(finfo.Name);
                                        isProperty3.Add(false);
                                    }
                                }
                            }
                            
                            foreach (var pinfo in pinfos3)
                            {
                                if (pinfo.CanWrite)
                                {
                                    if (pinfo.PropertyType == typeof(float))
                                    {
                                        fieldListNames3.Add(pinfo.Name);
                                        isProperty3.Add(true);
                                    }
                                }
                            }
                            
                            selectedIndex3 = fieldListNames3.FindIndex(match => match == trigger.operations[j].stringVal4);

                            selectedIndex3 = EditorGUILayout.Popup("Property", selectedIndex3, fieldListNames3.ToArray());
                            if (selectedIndex3 >= 0 && selectedIndex3 < fieldListNames3.Count)
                            {
                                trigger.operations[j].stringVal4 = fieldListNames3[selectedIndex3];
                                trigger.operations[j].boolVal = isProperty3[selectedIndex3];
                            }
                            else
                            {
                                trigger.operations[j].stringVal4 = "";
                            }

                            break;
                        case CVRInteractableActionOperation.ActionType.DeleteGameObject:
                            trigger.operations[j].gameObjectVal = (GameObject) EditorGUILayout.ObjectField("Target",
                                trigger.operations[j].gameObjectVal, typeof(GameObject));
                            break;
                    }
                    
                    if(trigger.operations[j].varBufferVal != null) trigger.operations[j].varBufferVal.AddInteracteable(_interactable);
                    if(trigger.operations[j].varBufferVal2 != null) trigger.operations[j].varBufferVal2.AddInteracteable(_interactable);
                    if(trigger.operations[j].varBufferVal3 != null) trigger.operations[j].varBufferVal3.AddInteracteable(_interactable);

                    GUILayout.EndVertical ();
                }
                
                if (GUILayout.Button("Remove Action"))
                {
                    trigger.operations.RemoveAt(trigger.operations.Count - 1);
                }
                if (GUILayout.Button("Add Action"))
                {
                    trigger.operations.Add(new CVRInteractableActionOperation());
                }
                
                GUILayout.EndVertical ();
                GUILayout.EndHorizontal ();
                GUILayout.EndVertical ();

                actionIndex++;
            }
            
            if (GUILayout.Button("Remove Trigger"))
            {
                _interactable.actions.RemoveAt(_interactable.actions.Count - 1);
            }
            
            if (GUILayout.Button("Add Trigger"))
            {
                _interactable.actions.Add(new CVRInteractableAction());
            }
            
            if (GUI.changed)
            {
                EditorUtility.SetDirty (target);
            }
        }

        private void RenderTargets(List<GameObject> targets)
        {
            RenderTargets(targets, "Target");
        }

        private void RenderTargets(List<GameObject> targets, String caption)
        {
            if (targets != null)
            {
                for (int k = 0; k < targets.Count; k++)
                {
                    targets[k] = (GameObject) EditorGUILayout.ObjectField(
                        caption + ":",
                        targets[k],
                        typeof(GameObject),
                        true
                    );
                }
            }

            if (GUILayout.Button("Remove " + caption))
            {
                targets.RemoveAt(targets.Count - 1);
            }
            if (GUILayout.Button("Add " + caption))
            {
                targets.Add(null);
            }
        }
    }
}