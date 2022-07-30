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
    [CustomEditor(typeof(CVRAdvancedAvatarSettingsTrigger))]
    public class CCK_CVRAdvancedAvatarSettingsTriggerEditor : UnityEditor.Editor
    {
        private CVRAdvancedAvatarSettingsTrigger trigger;
        private AnimatorController animator;
        private CVRAdvancedAvatarSettingsTriggerTask enterEntity;
        private CVRAdvancedAvatarSettingsTriggerTask exitEntity;
        private CVRAdvancedAvatarSettingsTriggerTaskStay stayEntity;
        private ReorderableList _onEnterList;
        private ReorderableList _onExitList;
        private ReorderableList _onStayList;
        private List<string> animatorParameters;
        
        public override void OnInspectorGUI()
        {
            trigger = (CVRAdvancedAvatarSettingsTrigger) target;
            var avatar = trigger.GetComponentInParent<CVRAvatar>();
            animator = null;
            animatorParameters = new List<string>();
            animatorParameters.Add("-none-");

            if (avatar != null && avatar.overrides != null && avatar.overrides.runtimeAnimatorController != null)
            {
                animator = (AnimatorController) avatar.overrides.runtimeAnimatorController;
                foreach (var parameter in animator.parameters)
                {
                    if ((parameter.type == AnimatorControllerParameterType.Float ||
                         parameter.type == AnimatorControllerParameterType.Bool ||
                         parameter.type == AnimatorControllerParameterType.Int) 
                        && parameter.name.Length > 0 &&
                        !CCK_CVRAvatarEditor.coreParameters.Contains(parameter.name) && parameter.name.Substring(0, 1) != "#")
                    {
                        animatorParameters.Add(parameter.name);
                    }
                }
            }

            var triggers = trigger.GetComponents<CVRAdvancedAvatarSettingsTrigger>();

            if (triggers.Length > 1)
            {
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_ADVAVTR_TRIGGER_MULTIPLE_TRIGGER_HELPBOX"), MessageType.Error);
            }

            trigger.areaSize = EditorGUILayout.Vector3Field("Area Size", trigger.areaSize);
            
            trigger.areaOffset = EditorGUILayout.Vector3Field("Area Offset", trigger.areaOffset);

            if (!trigger.useAdvancedTrigger)
            {
                if (animator == null)
                {
                    trigger.settingName = EditorGUILayout.TextField("Setting Name", trigger.settingName);
                }
                else
                {
                    var animatorParams = animatorParameters.ToArray();
                    var index = animatorParameters.FindIndex(match => match == trigger.settingName);
                    index = Mathf.Max(EditorGUILayout.Popup("Setting Name", index, animatorParams), 0);
                    trigger.settingName = animatorParams[index];
                }
                

                trigger.settingValue = EditorGUILayout.FloatField("Setting Value", trigger.settingValue);

                trigger.useAdvancedTrigger = EditorGUILayout.Toggle("Enabled Advanced Mode", trigger.useAdvancedTrigger);
            }
            else
            {
                trigger.useAdvancedTrigger = EditorGUILayout.Toggle("Enabled Advanced Mode", trigger.useAdvancedTrigger);
                
                var list = serializedObject.FindProperty("allowedPointer");
                EditorGUILayout.PropertyField(list, new GUIContent("Allowed Pointers"), true);
                serializedObject.ApplyModifiedProperties();
                
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_ADVAVTR_TRIGGER_ALLOWED_POINTERS_HELPBOX"), MessageType.Info);

                trigger.isNetworkInteractable = EditorGUILayout.Toggle("Network Interactable", trigger.isNetworkInteractable);
                
                list = serializedObject.FindProperty("allowedTypes");
                EditorGUILayout.PropertyField(list, new GUIContent("Allowed Types"), true);
                serializedObject.ApplyModifiedProperties();
                
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_ADVAVTR_TRIGGER_ALLOWED_TYPES_HELPBOX"), MessageType.Info);

                trigger.allowParticleInteraction = EditorGUILayout.Toggle("Enabled Particle Interaction", trigger.allowParticleInteraction);
                
                EditorGUILayout.HelpBox(CCKLocalizationProvider.GetLocalizedText("ABI_UI_ADVAVTR_TRIGGER_PARTICLE_HELPBOX"), MessageType.Info);
                
                if (_onEnterList == null)
                {
                    _onEnterList = new ReorderableList(trigger.enterTasks, typeof(CVRAdvancedAvatarSettingsTriggerTask),
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
                    _onExitList = new ReorderableList(trigger.exitTasks, typeof(CVRAdvancedAvatarSettingsTriggerTask),
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
                    _onStayList = new ReorderableList(trigger.stayTasks, typeof(CVRAdvancedAvatarSettingsTriggerTaskStay),
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
                    trigger.sampleDirection = (CVRAdvancedAvatarSettingsTrigger.SampleDirection) 
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

            EditorGUI.LabelField(_rect, "Setting Name");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            
            if (animator == null)
            {
                enterEntity.settingName = EditorGUI.TextField(_rect, enterEntity.settingName);
            }
            else
            {
                _rect.y += 1;
                var animatorParams = animatorParameters.ToArray();
                var selected = animatorParameters.FindIndex(match => match == enterEntity.settingName);
                selected = Mathf.Max(EditorGUI.Popup(_rect, selected, animatorParams), 0);
                enterEntity.settingName = animatorParams[selected];
            }
            
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
            enterEntity.updateMethod = (CVRAdvancedAvatarSettingsTriggerTask.UpdateMethod) EditorGUI.EnumPopup(_rect, enterEntity.updateMethod);
        }

        private float OnHeightElementEnter(int index)
        {
            return EditorGUIUtility.singleLineHeight * 6.25f;
        }

        private void OnAddEnter(ReorderableList list)
        {
            trigger.enterTasks.Add(new CVRAdvancedAvatarSettingsTriggerTask());
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

            EditorGUI.LabelField(_rect, "Setting Name");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            
            if (animator == null)
            {
                exitEntity.settingName = EditorGUI.TextField(_rect, exitEntity.settingName);
            }
            else
            {
                _rect.y += 1;
                var animatorParams = animatorParameters.ToArray();
                var selected = animatorParameters.FindIndex(match => match == exitEntity.settingName);
                selected = Mathf.Max(EditorGUI.Popup(_rect, selected, animatorParams), 0);
                exitEntity.settingName = animatorParams[selected];
            }
            
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
            exitEntity.updateMethod = (CVRAdvancedAvatarSettingsTriggerTask.UpdateMethod) EditorGUI.EnumPopup(_rect, exitEntity.updateMethod);
        }

        private float OnHeightElementExit(int index)
        {
            return EditorGUIUtility.singleLineHeight * 5f;
        }

        private void OnAddExit(ReorderableList list)
        {
            trigger.exitTasks.Add(new CVRAdvancedAvatarSettingsTriggerTask());
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

            EditorGUI.LabelField(_rect, "Setting Name");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            
            if (animator == null)
            {
                stayEntity.settingName = EditorGUI.TextField(_rect, stayEntity.settingName);
            }
            else
            {
                _rect.y += 1;
                var animatorParams = animatorParameters.ToArray();
                var selected = animatorParameters.FindIndex(match => match == stayEntity.settingName);
                selected = Mathf.Max(EditorGUI.Popup(_rect, selected, animatorParams), 0);
                stayEntity.settingName = animatorParams[selected];
            }
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_rect, "Update Method");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            
            stayEntity.updateMethod = (CVRAdvancedAvatarSettingsTriggerTaskStay.UpdateMethod) EditorGUI.EnumPopup(_rect, stayEntity.updateMethod);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            if (stayEntity.updateMethod == CVRAdvancedAvatarSettingsTriggerTaskStay.UpdateMethod.SetFromPosition)
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
            
            if (stayEntity.updateMethod == CVRAdvancedAvatarSettingsTriggerTaskStay.UpdateMethod.SetFromPosition)
                return EditorGUIUtility.singleLineHeight * 5f;
            
            return EditorGUIUtility.singleLineHeight * 3.75f;
        }

        private void OnAddStay(ReorderableList list)
        {
            trigger.stayTasks.Add(new CVRAdvancedAvatarSettingsTriggerTaskStay());
            Repaint();
        }

        private void OnChangedStay(ReorderableList list)
        {
            //EditorUtility.SetDirty(target);
        }
    }
}