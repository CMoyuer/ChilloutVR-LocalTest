using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ABI.CCK.Components;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(ABI.CCK.Components.CVRDistanceLod), true)]
    public class CVRDistanceLodEditor : UnityEditor.Editor
    {
        private CVRDistanceLod _lod;
        
        private ReorderableList reorderableList;

        private CVRDistanceLodGroup entity;

        private void InitializeList()
        {
            reorderableList = new ReorderableList(_lod.Groups, typeof(CVRDistanceLodGroup),
                false, true, true, true);
            reorderableList.drawHeaderCallback = OnDrawHeader;
            reorderableList.drawElementCallback = OnDrawElement;
            reorderableList.elementHeightCallback = OnHeightElement;
            reorderableList.onAddCallback = OnAdd;
            reorderableList.onChangedCallback = OnChanged; 
        }

        private void OnChanged(ReorderableList list)
        {
            //EditorUtility.SetDirty(target);
        }

        private void OnAdd(ReorderableList list)
        {
            _lod.Groups.Add(new CVRDistanceLodGroup());
            Repaint();
        }

        private float OnHeightElement(int index)
        {
            return EditorGUIUtility.singleLineHeight * 3 * 1.25f;
        }

        private void OnDrawElement(Rect rect, int index, bool isactive, bool isfocused)
        {
            if (index > _lod.Groups.Count) return;
            entity = _lod.Groups[index];
            
            rect.y += 2;
            rect.x += 12;
            rect.width -= 12;
            Rect _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Game Object");
            
            _rect.x += 100;
            _rect.width = rect.width - 100;

            entity.GameObject = (GameObject) EditorGUI.ObjectField(_rect, entity.GameObject, typeof(GameObject), true);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_rect, "Min Distance");
            
            _rect.x += 100;
            _rect.width = rect.width - 100;
            
            entity.MinDistance = EditorGUI.FloatField(_rect, entity.MinDistance);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_rect, "Max Distance");
            
            _rect.x += 100;
            _rect.width = rect.width - 100;
            
            entity.MaxDistance = EditorGUI.FloatField(_rect, entity.MaxDistance);
        }

        private void OnDrawHeader(Rect rect)
        {
            Rect _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            GUI.Label(_rect, "Groups");
        }


        public override void OnInspectorGUI()
        {
            if (_lod == null) _lod = (CVRDistanceLod) target;

            _lod.distance3D = EditorGUILayout.Toggle("3D Distance", _lod.distance3D);

            if (reorderableList == null) InitializeList();
            reorderableList.DoLayoutList();
        }
    }
}