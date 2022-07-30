using System.Collections.Generic;
using System.Linq;
using ABI.CCK.Components;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using AnimatorController = UnityEditor.Animations.AnimatorController;
using AnimatorControllerParameterType = UnityEngine.AnimatorControllerParameterType;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(CVRSpawnableTrigger))]
    public class CCK_CVRSpawnableTriggerEditor : UnityEditor.Editor
    {
        private CVRSpawnableTrigger trigger;
        private CVRSpawnableTriggerTask enterEntity;
        private CVRSpawnableTriggerTask exitEntity;
        private CVRSpawnableTriggerTaskStay stayEntity;
        private ReorderableList _onEnterList;
        private ReorderableList _onExitList;
        private ReorderableList _onStayList;
        private List<string> spawnableParameters;
        
        public override void OnInspectorGUI()
        {
            trigger = (CVRSpawnableTrigger) target;
            var spawnable = trigger.GetComponentInParent<CVRSpawnable>();

            if (spawnable == null)
            {
                EditorGUILayout.HelpBox("No CVRSpawnable was detected for this Trigger.", MessageType.Error);
                return;
            }

            spawnableParameters = new List<string>();
            spawnableParameters.Add("-none-");
            
            if (!spawnable.useAdditionalValues)
            {
                EditorGUILayout.HelpBox("The detected Spawnable does not use additional Values.", MessageType.Error);
                return;
            }
            else
            {
                foreach (var syncValue in spawnable.syncValues)
                {
                    spawnableParameters.Add(syncValue.name);
                }
            }

            var triggers = trigger.GetComponents<CVRSpawnableTrigger>();

            if (triggers.Length > 1)
            {
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_ADVAVTR_TRIGGER_MULTIPLE_TRIGGER_HELPBOX"), MessageType.Error);
            }

            trigger.areaSize = EditorGUILayout.Vector3Field("Area Size", trigger.areaSize);
            
            trigger.areaOffset = EditorGUILayout.Vector3Field("Area Offset", trigger.areaOffset);

            if (!trigger.useAdvancedTrigger)
            {
                trigger.settingIndex = EditorGUILayout.Popup("Parameter", trigger.settingIndex + 1, spawnableParameters.ToArray()) - 1;

                trigger.settingValue = EditorGUILayout.FloatField("Setting Value", trigger.settingValue);

                trigger.useAdvancedTrigger = EditorGUILayout.Toggle("Enabled Advanced Mode", trigger.useAdvancedTrigger);
            }
            else
            {
                trigger.useAdvancedTrigger = EditorGUILayout.Toggle("Enabled Advanced Mode", trigger.useAdvancedTrigger);

                var list = serializedObject.FindProperty("allowedTypes");
                EditorGUILayout.PropertyField(list, new GUIContent("Allowed Types"), true);
                serializedObject.ApplyModifiedProperties();
                
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_ADVAVTR_TRIGGER_ALLOWED_TYPES_HELPBOX"), MessageType.Info);

                trigger.allowParticleInteraction = EditorGUILayout.Toggle("Enabled Particle Interaction", trigger.allowParticleInteraction);
                
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_ADVAVTR_TRIGGER_PARTICLE_HELPBOX"), MessageType.Info);
                
                if (_onEnterList == null)
                {
                    _onEnterList = new ReorderableList(trigger.enterTasks, typeof(CVRSpawnableTriggerTask),
                        false, true, true, true);
                    _onEnterList.drawHeaderCallback = OnDrawHeaderEnter;
                    _onEnterList.drawElementCallback = OnDrawElementEnter;
                    _onEnterList.elementHeightCallback = OnHeightElementEnter;
                    _onEnterList.onAddCallback = OnAddEnter;
                    _onEnterList.onChangedCallback = OnChangedEnter; 
                }
                
                _onEnterList.DoLayoutList();
                
                if (_onExitList == null)
                {
                    _onExitList = new ReorderableList(trigger.exitTasks, typeof(CVRSpawnableTriggerTask),
                        false, true, true, true);
                    _onExitList.drawHeaderCallback = OnDrawHeaderExit;
                    _onExitList.drawElementCallback = OnDrawElementExit;
                    _onExitList.elementHeightCallback = OnHeightElementExit;
                    _onExitList.onAddCallback = OnAddExit;
                    _onExitList.onChangedCallback = OnChangedExit; 
                }
                
                _onExitList.DoLayoutList();

                if (_onStayList == null)
                {
                    _onStayList = new ReorderableList(trigger.stayTasks, typeof(CVRSpawnableTriggerTaskStay),
                        false, true, true, true);
                    _onStayList.drawHeaderCallback = OnDrawHeaderStay;
                    _onStayList.drawElementCallback = OnDrawElementStay;
                    _onStayList.elementHeightCallback = OnHeightElementStay;
                    _onStayList.onAddCallback = OnAddStay;
                    _onStayList.onChangedCallback = OnChangedStay; 
                }
                
                _onStayList.DoLayoutList();

                if (trigger.stayTasks.Count > 0)
                {
                    trigger.sampleDirection = (CVRSpawnableTrigger.SampleDirection) 
                        EditorGUILayout.EnumPopup("Sample Direction", trigger.sampleDirection);
                }
            }
        }

        private void OnDrawHeaderEnter(Rect rect)
        {
            Rect _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            GUI.Label(_rect, "On Enter Trigger");
        }

        private void OnDrawElementEnter(Rect rect, int index, bool isactive, bool isfocused)
        {
            if (index > trigger.enterTasks.Count) return;
            enterEntity = trigger.enterTasks[index];
            
            Rect _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_rect, "Parameter");
            _rect.x += 100;
            _rect.width = rect.width - 100;

            enterEntity.settingIndex = EditorGUI.Popup(_rect, enterEntity.settingIndex + 1, spawnableParameters.ToArray()) - 1;
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Setting Value");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            enterEntity.settingValue = EditorGUI.FloatField(_rect, enterEntity.settingValue);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Delay");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            enterEntity.delay = EditorGUI.FloatField(_rect, enterEntity.delay);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Hold Time");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            enterEntity.holdTime = EditorGUI.FloatField(_rect, enterEntity.holdTime);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Update Method");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            enterEntity.updateMethod = (CVRSpawnableTriggerTask.UpdateMethod) EditorGUI.EnumPopup(_rect, enterEntity.updateMethod);
        }

        private float OnHeightElementEnter(int index)
        {
            return EditorGUIUtility.singleLineHeight * 6.25f;
        }

        private void OnAddEnter(ReorderableList list)
        {
            trigger.enterTasks.Add(new CVRSpawnableTriggerTask());
            Repaint();
        }

        private void OnChangedEnter(ReorderableList list)
        {
            //EditorUtility.SetDirty(target);
        }
        
        private void OnDrawHeaderExit(Rect rect)
        {
            Rect _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            GUI.Label(_rect, "On Exit Trigger");
        }

        private void OnDrawElementExit(Rect rect, int index, bool isactive, bool isfocused)
        {
            if (index > trigger.exitTasks.Count) return;
            exitEntity = trigger.exitTasks[index];
            
            Rect _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_rect, "Parameter");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            
            exitEntity.settingIndex = EditorGUI.Popup(_rect, exitEntity.settingIndex + 1, spawnableParameters.ToArray()) - 1;
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Setting Value");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            exitEntity.settingValue = EditorGUI.FloatField(_rect, exitEntity.settingValue);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Delay");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            exitEntity.delay = EditorGUI.FloatField(_rect, exitEntity.delay);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Update Method");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            exitEntity.updateMethod = (CVRSpawnableTriggerTask.UpdateMethod) EditorGUI.EnumPopup(_rect, exitEntity.updateMethod);
        }

        private float OnHeightElementExit(int index)
        {
            return EditorGUIUtility.singleLineHeight * 5f;
        }

        private void OnAddExit(ReorderableList list)
        {
            trigger.exitTasks.Add(new CVRSpawnableTriggerTask());
            Repaint();
        }

        private void OnChangedExit(ReorderableList list)
        {
            //EditorUtility.SetDirty(target);
        }
        
        private void OnDrawHeaderStay(Rect rect)
        {
            Rect _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            GUI.Label(_rect, "On Stay Trigger");
        }

        private void OnDrawElementStay(Rect rect, int index, bool isactive, bool isfocused)
        {
            if (index > trigger.stayTasks.Count) return;
            stayEntity = trigger.stayTasks[index];
            
            Rect _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_rect, "Parameter");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            
            stayEntity.settingIndex = EditorGUI.Popup(_rect, stayEntity.settingIndex + 1, spawnableParameters.ToArray()) - 1;
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_rect, "Update Method");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            
            stayEntity.updateMethod = (CVRSpawnableTriggerTaskStay.UpdateMethod) EditorGUI.EnumPopup(_rect, stayEntity.updateMethod);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            if (stayEntity.updateMethod == CVRSpawnableTriggerTaskStay.UpdateMethod.SetFromPosition)
            {
                EditorGUI.LabelField(_rect, "Min Value");
                _rect.x += 100;
                _rect.width = rect.width - 100;
                stayEntity.minValue = EditorGUI.FloatField(_rect, stayEntity.minValue);

                rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

                EditorGUI.LabelField(_rect, "Max Value");
                _rect.x += 100;
                _rect.width = rect.width - 100;
                stayEntity.maxValue = EditorGUI.FloatField(_rect, stayEntity.maxValue);

                rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            }
            else
            {
                EditorGUI.LabelField(_rect, "Change per sec");
                _rect.x += 100;
                _rect.width = rect.width - 100;
                stayEntity.minValue = EditorGUI.FloatField(_rect, stayEntity.minValue);

                rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            }
        }

        private float OnHeightElementStay(int index)
        {
            if (index > trigger.stayTasks.Count) return EditorGUIUtility.singleLineHeight * 3.75f;
            stayEntity = trigger.stayTasks[index];
            
            if (stayEntity.updateMethod == CVRSpawnableTriggerTaskStay.UpdateMethod.SetFromPosition)
                return EditorGUIUtility.singleLineHeight * 5f;
            
            return EditorGUIUtility.singleLineHeight * 3.75f;
        }

        private void OnAddStay(ReorderableList list)
        {
            trigger.stayTasks.Add(new CVRSpawnableTriggerTaskStay());
            Repaint();
        }

        private void OnChangedStay(ReorderableList list)
        {
            //EditorUtility.SetDirty(target);
        }
    }
}