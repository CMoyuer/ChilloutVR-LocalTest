using System;
using System.Collections.Generic;
using System.Reflection;
using ABI.CCK.Components;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(ABI.CCK.Components.CVRTexturePropertyParser), true)]
    public class CCK_CVRTexturePropertyParserEditor : UnityEditor.Editor
    {
        private CVRTexturePropertyParser _parser;
        private ReorderableList _list;

        private CVRTexturePropertyParserTask _element;
        private static Dictionary<int, string[]> TypeAttributeList = new Dictionary<int, string[]>()
        {
            {3, new string[]{"X", "Y"}},
            {4, new string[]{"X", "Y", "Z"}},
            {5, new string[]{"X", "Y", "Z", "W"}},
            {6, new string[]{"R", "G", "B", "A"}}
        };
        
        public override void OnInspectorGUI()
        {
            if (_parser == null) _parser = (CVRTexturePropertyParser) target;

            EditorGUI.BeginChangeCheck();

            CVRTexturePropertyParser.TextureType textureType = (CVRTexturePropertyParser.TextureType) EditorGUILayout.EnumPopup("Texture Type", _parser.textureType);

            RenderTexture texture = null;
            string globalTextureName = "";
            if (textureType == CVRTexturePropertyParser.TextureType.LocalTexture)
            {
                texture = (RenderTexture) EditorGUILayout.ObjectField("Texture", _parser.texture, typeof(RenderTexture));
            }
            else
            {
                globalTextureName = EditorGUILayout.TextField("Texture Name", _parser.globalTextureName);
            }

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed CVRTexturePropertyParser");

                _parser.textureType = textureType;
                if (textureType == CVRTexturePropertyParser.TextureType.LocalTexture)
                {
                    _parser.texture = texture;
                }
                else
                {
                    _parser.globalTextureName = globalTextureName;
                }
            }

            if (_list == null) InitializeList();
            _list.DoLayoutList();
        }

        private void InitializeList()
        {
            _list = new ReorderableList(_parser.tasks, typeof(CVRTexturePropertyParserTask), false, true, true, true);
            _list.drawElementCallback = DrawElement;
            _list.drawHeaderCallback = DrawHeader;
            _list.elementHeightCallback = ElementHeight;
        }

        private void DrawElement(Rect rect, int index, bool isactive, bool isfocused)
        {
            if (index >= _parser.tasks.Count) return;
            _element = _parser.tasks[index];
            
            EditorGUI.BeginChangeCheck();
            
            Rect _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(_rect, "X Position");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            int x = EditorGUI.IntField(_rect, _element.x);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(_rect, "Y Position");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            int y = EditorGUI.IntField(_rect, _element.y);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(_rect, "Color Channel");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            CVRTexturePropertyParserTask.Channel channel = (CVRTexturePropertyParserTask.Channel) EditorGUI.EnumPopup(_rect, _element.channel);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(_rect, "Min Value");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            float minValue = EditorGUI.FloatField(_rect, _element.minValue);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(_rect, "Max Value");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            float maxValue = EditorGUI.FloatField(_rect, _element.maxValue);
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(_rect, "Target");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            GameObject target = (GameObject) EditorGUI.ObjectField(_rect, _element.target, typeof(GameObject));

            if (target != _element.target) _element.component = null;
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(_rect, "Component");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            
            //Get Component
            List<Component> componentList = new List<Component>();
            List<Type> componentListTypes = new List<Type>();
            List<string> componentListNames = new List<string>();
            int selectedIndex = 0;
            Component component = null;
            
            if (_element.target == null)
            {
                EditorGUI.HelpBox(_rect, "Select a Target.", MessageType.Info);
            }
            else
            {
                Component[] rawComponents = target.GetComponents<Component>();
                
                foreach (var rawComponent in rawComponents)
                {
                    if (rawComponent != null && !componentListTypes.Contains(rawComponent.GetType()) &&
                        rawComponent.GetType() != typeof(CVRTexturePropertyParser))
                    {
                        componentListTypes.Add(rawComponent.GetType());
                        componentListNames.Add(rawComponent.GetType().Name);
                        componentList.Add(rawComponent);
                    }
                }

                selectedIndex = componentList.FindIndex(match => match == _element.component);

                selectedIndex = EditorGUI.Popup(_rect, selectedIndex, componentListNames.ToArray());
                if (selectedIndex >= 0 && selectedIndex < componentListTypes.Count)
                {
                    component = componentList[selectedIndex];
                }
                else
                {
                    component = null;
                }
            }

            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            
            _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(_rect, "Property");
            _rect.x += 100;
            _rect.width = rect.width - 100;
            
            //Get Component Property
            string propertyName = "";
            int propertyType = 0;
            int targetIndex = 0;

            if (_element.component == null)
            {
                EditorGUI.HelpBox(_rect, "Select a Component to proceed.", MessageType.Info);
            }
            else
            {
                BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default;
                FieldInfo[] finfos = componentListTypes[selectedIndex].GetFields(flags);
                PropertyInfo[] pinfos = componentListTypes[selectedIndex].GetProperties(flags);
                List<string> fieldListNames = new List<string>();
                List<string> fieldListDisplayNames = new List<string>();
                List<int> fieldTypes = new List<int>();
                selectedIndex = 0;

                foreach (var finfo in finfos)
                {
                    if (finfo.IsPublic)
                    {
                        if (finfo.FieldType == typeof(float))
                        {
                            fieldListNames.Add(finfo.Name);
                            fieldListDisplayNames.Add(finfo.Name + " (float)");
                            fieldTypes.Add(0);
                        }
                        else if (finfo.FieldType == typeof(int))
                        {
                            fieldListNames.Add(finfo.Name);
                            fieldListDisplayNames.Add(finfo.Name + " (int)");
                            fieldTypes.Add(1);
                        }
                        else if (finfo.FieldType == typeof(bool))
                        {
                            fieldListNames.Add(finfo.Name);
                            fieldListDisplayNames.Add(finfo.Name + " (bool)");
                            fieldTypes.Add(2);
                        }
                        else if (finfo.FieldType == typeof(Vector2))
                        {
                            fieldListNames.Add(finfo.Name);
                            fieldListDisplayNames.Add(finfo.Name + " (Vector2)");
                            fieldTypes.Add(3);
                        }
                        else if (finfo.FieldType == typeof(Vector3))
                        {
                            fieldListNames.Add(finfo.Name);
                            fieldListDisplayNames.Add(finfo.Name + " (Vector3)");
                            fieldTypes.Add(4);
                        }
                        else if (finfo.FieldType == typeof(Vector4))
                        {
                            fieldListNames.Add(finfo.Name);
                            fieldListDisplayNames.Add(finfo.Name + " (Vector4)");
                            fieldTypes.Add(5);
                        }
                        else if (finfo.FieldType == typeof(Color))
                        {
                            fieldListNames.Add(finfo.Name);
                            fieldListDisplayNames.Add(finfo.Name + " (Color)");
                            fieldTypes.Add(6);
                        }
                    }
                }

                foreach (var pinfo in pinfos)
                {
                    if (pinfo.CanWrite)
                    {
                        if (pinfo.PropertyType == typeof(float))
                        {
                            fieldListNames.Add(pinfo.Name);
                            fieldListDisplayNames.Add(pinfo.Name + " (float)");
                            fieldTypes.Add(0);
                        }
                        else if (pinfo.PropertyType == typeof(int))
                        {
                            fieldListNames.Add(pinfo.Name);
                            fieldListDisplayNames.Add(pinfo.Name + " (int)");
                            fieldTypes.Add(1);
                        }
                        else if (pinfo.PropertyType == typeof(bool))
                        {
                            fieldListNames.Add(pinfo.Name);
                            fieldListDisplayNames.Add(pinfo.Name + " (bool)");
                            fieldTypes.Add(2);
                        }
                        else if (pinfo.PropertyType == typeof(Vector2))
                        {
                            fieldListNames.Add(pinfo.Name);
                            fieldListDisplayNames.Add(pinfo.Name + " (Vector2)");
                            fieldTypes.Add(3);
                        }
                        else if (pinfo.PropertyType == typeof(Vector3))
                        {
                            fieldListNames.Add(pinfo.Name);
                            fieldListDisplayNames.Add(pinfo.Name + " (Vector3)");
                            fieldTypes.Add(4);
                        }
                        else if (pinfo.PropertyType == typeof(Vector4))
                        {
                            fieldListNames.Add(pinfo.Name);
                            fieldListDisplayNames.Add(pinfo.Name + " (Vector4)");
                            fieldTypes.Add(5);
                        }
                        else if (pinfo.PropertyType == typeof(Color))
                        {
                            fieldListNames.Add(pinfo.Name);
                            fieldListDisplayNames.Add(pinfo.Name + " (Color)");
                            fieldTypes.Add(6);
                        }
                    }
                }

                selectedIndex = fieldListNames.FindIndex(match => match == _element.propertyName);

                selectedIndex = EditorGUI.Popup(_rect, selectedIndex, fieldListDisplayNames.ToArray());
                if (selectedIndex >= 0 && selectedIndex < fieldListNames.Count)
                {
                    propertyName = fieldListNames[selectedIndex];
                    propertyType = fieldTypes[selectedIndex];
                }
                else
                {
                    propertyName = "";
                    propertyType = 0;
                }

                if (propertyType >= 3)
                {
                    rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            
                    _rect = new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight);
                    EditorGUI.LabelField(_rect, "Target Attribute");
                    _rect.x += 100;
                    _rect.width = rect.width - 100;

                    targetIndex = EditorGUI.Popup(_rect, _element.targetIndex, TypeAttributeList[propertyType]);
                }
            }

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_parser, "Changed CVRTexturePropertyParser Task");

                _element.x = x;
                _element.y = y;
                _element.channel = channel;
                _element.minValue = minValue;
                _element.maxValue = maxValue;
                _element.target = target;
                _element.component = component;
                _element.propertyName = propertyName;
                _element.typeIndex = propertyType;
                _element.targetIndex = targetIndex;
            }
        }

        private float ElementHeight(int index)
        {
            return EditorGUIUtility.singleLineHeight * (_parser.tasks[index].typeIndex >= 3 ? 11.75f : 10.5f);
        }

        private void DrawHeader(Rect rect)
        {
            Rect _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            GUI.Label(_rect, "Tasks");
        }
    }
}