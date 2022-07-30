using System.Collections.Generic;
using ABI.CCK.Components;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using AnimatorController = UnityEditor.Animations.AnimatorController;
using AnimatorControllerParameterType = UnityEngine.AnimatorControllerParameterType;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(ABI.CCK.Components.CVRAnimatorDriver))]
    public class CCK_CVRAnimatorDriverEditor : UnityEditor.Editor
    {
        private CVRAnimatorDriver _driver;
        
        private ReorderableList reorderableList;

        private List<string> animatorParamNameList = new List<string>();
        private List<AnimatorControllerParameterType> animatorParamTypeList = new List<AnimatorControllerParameterType>();

        private Dictionary<AnimatorControllerParameterType, int> typeList = new Dictionary<AnimatorControllerParameterType, int>()
        {
            {AnimatorControllerParameterType.Float, 0},
            {AnimatorControllerParameterType.Int, 1},
            {AnimatorControllerParameterType.Bool, 2},
            {AnimatorControllerParameterType.Trigger, 3}
        }; 
        
        private void InitializeList()
        {
            reorderableList = new ReorderableList(_driver.animators, typeof(Animator),
                false, true, true, true);
            reorderableList.drawHeaderCallback = OnDrawHeader;
            reorderableList.drawElementCallback = OnDrawElement;
            reorderableList.elementHeightCallback = OnHeightElement;
            reorderableList.onAddCallback = OnAdd;
            reorderableList.onChangedCallback = OnChanged;
            reorderableList.onRemoveCallback = OnRemove;
        }

        private void OnRemove(ReorderableList list)
        {
            _driver.animators.RemoveAt(list.index);
            _driver.animatorParameters.RemoveAt(list.index);
            _driver.animatorParameterType.RemoveAt(list.index);
            Repaint();
        }

        private void OnChanged(ReorderableList list)
        {
            //EditorUtility.SetDirty(target);
        }

        private void OnAdd(ReorderableList list)
        {
            if (_driver.animators.Count >= 16) return;
            _driver.animators.Add(null);
            _driver.animatorParameters.Add(null);
            _driver.animatorParameterType.Add(0);
            Repaint();
        }

        private float OnHeightElement(int index)
        {
            return EditorGUIUtility.singleLineHeight * 3 * 1.25f;
        }

        private void OnDrawElement(Rect rect, int index, bool isactive, bool isfocused)
        {
            if (index > _driver.animators.Count) return;

            rect.y += 2;
            rect.x += 12;
            rect.width -= 12;
            Rect _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Animator");
            
            _rect.x += 100;
            _rect.width = rect.width - 100;

            EditorGUI.BeginChangeCheck();
            
            var animator = (Animator) EditorGUI.ObjectField(_rect, _driver.animators[index], typeof(Animator), true);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Animator Driver Animator changed");
                EditorUtility.SetDirty(_driver);
                _driver.animators[index] = animator;
            }

            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Parameter");
            
            _rect.x += 100;
            _rect.width = rect.width - 100;

            animatorParamNameList.Clear();
            animatorParamTypeList.Clear();

            animatorParamNameList.Add("-none-");
            animatorParamTypeList.Add(AnimatorControllerParameterType.Bool);

            var oldIndex = 0;
            var i = 1;
            
            if (_driver.animators[index] != null && _driver.animators[index].runtimeAnimatorController != null)
            {
                var controller = (AnimatorController) _driver.animators[index].runtimeAnimatorController;
                foreach (var parameter in controller.parameters)
                {
                    animatorParamNameList.Add(parameter.name);
                    animatorParamTypeList.Add(parameter.type);

                    if (_driver.animatorParameters[index] == parameter.name)
                    {
                        oldIndex = i;
                    }

                    i++;
                }
            }

            EditorGUI.BeginChangeCheck();
            
            var parameterIndex = EditorGUI.Popup(_rect, oldIndex, animatorParamNameList.ToArray());

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Animator Driver Parameter changed");
                EditorUtility.SetDirty(_driver);
                _driver.animatorParameters[index] = animatorParamNameList[parameterIndex];
                _driver.animatorParameterType[index] = typeList[animatorParamTypeList[parameterIndex]];
            }

            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Value");
            
            _rect.x += 100;
            _rect.width = rect.width - 100;
            
            EditorGUI.BeginChangeCheck();

            var value = EditorGUI.FloatField(_rect, GetAnimatorParameterValue(index));
            
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Animator Driver Value changed");
                EditorUtility.SetDirty(_driver);
                SetAnimatorParameterValue(index, value);
            }
        }

        private void OnDrawHeader(Rect rect)
        {
            Rect _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            GUI.Label(_rect, "Animator Parameters");
        }


        public override void OnInspectorGUI()
        {
            if (_driver == null) _driver = (CVRAnimatorDriver) target;

            EditorGUILayout.Space();
            
            if (reorderableList == null) InitializeList();
            reorderableList.DoLayoutList();
        }
        
        public float GetAnimatorParameterValue(int index)
        {
            switch (index)
            {
                case 0:
                    return _driver.animatorParameter01;
                    break;
                case 1:
                    return _driver.animatorParameter02;
                    break;
                case 2:
                    return _driver.animatorParameter03;
                    break;
                case 3:
                    return _driver.animatorParameter04;
                    break;
                case 4:
                    return _driver.animatorParameter05;
                    break;
                case 5:
                    return _driver.animatorParameter06;
                    break;
                case 6:
                    return _driver.animatorParameter07;
                    break;
                case 7:
                    return _driver.animatorParameter08;
                    break;
                case 8:
                    return _driver.animatorParameter09;
                    break;
                case 9:
                    return _driver.animatorParameter10;
                    break;
                case 10:
                    return _driver.animatorParameter11;
                    break;
                case 11:
                    return _driver.animatorParameter12;
                    break;
                case 12:
                    return _driver.animatorParameter13;
                    break;
                case 13:
                    return _driver.animatorParameter14;
                    break;
                case 14:
                    return _driver.animatorParameter15;
                    break;
                default:
                    return _driver.animatorParameter16;
                    break;
            }
        }
        
        public void SetAnimatorParameterValue(int index, float value)
        {
            switch (index)
            {
                case 0:
                    _driver.animatorParameter01 = value;
                    break;
                case 1:
                    _driver.animatorParameter02 = value;
                    break;
                case 2:
                    _driver.animatorParameter03 = value;
                    break;
                case 3:
                    _driver.animatorParameter04 = value;
                    break;
                case 4:
                    _driver.animatorParameter05 = value;
                    break;
                case 5:
                    _driver.animatorParameter06 = value;
                    break;
                case 6:
                    _driver.animatorParameter07 = value;
                    break;
                case 7:
                    _driver.animatorParameter08 = value;
                    break;
                case 8:
                    _driver.animatorParameter09 = value;
                    break;
                case 9:
                    _driver.animatorParameter10 = value;
                    break;
                case 10:
                    _driver.animatorParameter11 = value;
                    break;
                case 11:
                    _driver.animatorParameter12 = value;
                    break;
                case 12:
                    _driver.animatorParameter13 = value;
                    break;
                case 13:
                    _driver.animatorParameter14 = value;
                    break;
                case 14:
                    _driver.animatorParameter15 = value;
                    break;
                default:
                    _driver.animatorParameter16 = value;
                    break;
            }
        }
    }
}