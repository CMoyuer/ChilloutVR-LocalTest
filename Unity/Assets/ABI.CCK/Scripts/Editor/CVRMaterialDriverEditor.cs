using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ABI.CCK.Components;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Rendering;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(ABI.CCK.Components.CVRMaterialDriver))]
    public class CVRMaterialDriverEditor : UnityEditor.Editor
    {
        private CVRMaterialDriver _driver;
        
        private ReorderableList reorderableList;

        private CVRMaterialDriverTask entity;

        private void InitializeList()
        {
            reorderableList = new ReorderableList(_driver.tasks, typeof(CVRMaterialDriverTask),
                true, true, true, true);
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
            if (_driver.tasks.Count >= 16) return;
            _driver.tasks.Add(new CVRMaterialDriverTask());
            Repaint();
        }

        private float OnHeightElement(int index)
        {
            return EditorGUIUtility.singleLineHeight * 3 * 1.25f;
        }

        private void OnDrawElement(Rect rect, int index, bool isactive, bool isfocused)
        {
            if (index > _driver.tasks.Count) return;
            entity = _driver.tasks[index];
            
            rect.y += 2;
            rect.x += 12;
            rect.width -= 12;
            Rect _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(_rect, "Renderer");
            
            _rect.x += 100;
            _rect.width = rect.width - 100;

            entity.Renderer = (Renderer) EditorGUI.ObjectField(_rect, entity.Renderer, typeof(Renderer), true);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);

            var materialList = new Dictionary<int, string>();
            if (entity.Renderer != null)
            {
                for (var i = 0; i < entity.Renderer.sharedMaterials.Length; i++)
                {
                    if (entity.Renderer.sharedMaterials[i] != null)
                    {
                        materialList.Add(i, entity.Renderer.sharedMaterials[i].name + "[" + i + "]");
                    }
                    else
                    {
                        materialList.Add(i, "None[" + i + "]");
                    }
                }
            }
            
            var materialNames = new string[materialList.Count];
            materialList.Values.CopyTo(materialNames, 0);
            var materialIndeces = new int[materialList.Count];
            materialList.Keys.CopyTo(materialIndeces, 0);

            EditorGUI.LabelField(_rect, "Material");
            
            _rect.x += 100;
            _rect.width = rect.width - 100;

            entity.Index = EditorGUI.Popup(_rect, entity.Index, materialNames);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            
            var propertyList = new Dictionary<string, string>();
            var rendererFound = false;
            
            if (entity.Renderer != null)
            {
                int i = entity.Index;
                var material = entity.Renderer.sharedMaterials[i];
                if (material != null)
                {
                    var shader = material.shader;
                    for (var j = 0; j < shader.GetPropertyCount(); j++)
                    {
                        if ((shader.GetPropertyType(j) == ShaderPropertyType.Float ||
                             shader.GetPropertyType(j) == ShaderPropertyType.Range ||
                             shader.GetPropertyType(j) == ShaderPropertyType.Vector ||
                             shader.GetPropertyType(j) == ShaderPropertyType.Color))
                        {
                            var propertyKey = material.name + "[" + i + "]: " + shader.GetPropertyDescription(j) + "(" +
                                              shader.GetPropertyName(j) + ")";
                            if (!propertyList.ContainsKey(propertyKey))
                            {
                                propertyList.Add(
                                    propertyKey,
                                    "REN[" + i + "]:" + shader.GetPropertyName(j)
                                );
                            }
                        }
                    }

                    rendererFound = true;
                }
            }
            
            var propertyNames = new string[propertyList.Count];
            propertyList.Values.CopyTo(propertyNames, 0);
            var propertyDescriptions = new string[propertyList.Count];
            propertyList.Keys.CopyTo(propertyDescriptions, 0);
            
            EditorGUI.LabelField(_rect, "Property");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            var propertyIndex = EditorGUI.Popup(_rect, Array.IndexOf(propertyNames, entity.RendererType + "["+ entity.Index +"]:" + entity.PropertyName), propertyDescriptions);

            if (propertyIndex >= 0)
            {
                Regex rx = new Regex(@"^([A-Z]{3})\[(\d+)\]\:(.+)$");
                var match = rx.Match(propertyNames[propertyIndex]);

                entity.RendererType = match.Groups[1].Value;
                entity.Index = Convert.ToInt32(match.Groups[2].Value);
                entity.PropertyName = match.Groups[3].Value;

                var shader = entity.Renderer.sharedMaterials[entity.Index].shader;

                switch (shader.GetPropertyType(shader.FindPropertyIndex(entity.PropertyName)))
                {
                    case ShaderPropertyType.Float:
                    case ShaderPropertyType.Range:
                        entity.PropertyType = CVRMaterialDriverTask.Type.Float;
                        break;
                    case ShaderPropertyType.Vector:
                        entity.PropertyType = CVRMaterialDriverTask.Type.Vector4;
                        break;
                    case ShaderPropertyType.Color:
                        entity.PropertyType = CVRMaterialDriverTask.Type.Color;
                        break;
                }
            }
        }

        private void OnDrawHeader(Rect rect)
        {
            Rect _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            GUI.Label(_rect, "Materials");
        }


        public override void OnInspectorGUI()
        {
            if (_driver == null) _driver = (CVRMaterialDriver) target;

            base.OnInspectorGUI();

            EditorGUILayout.Space();
            
            if (reorderableList == null) InitializeList();
            reorderableList.DoLayoutList();
        }

    }
}