using System.Collections.Generic;
using System.Linq;
using ABI.CCK.Components;
using TMPro;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(ABI.CCK.Components.CVRTranslatable))]
    public class CCK_CVRTranslatableEditor : UnityEditor.Editor
    {
        private CVRTranslatable _translatable;

        private ReorderableList reorderableList;
        private CVRTranslatable.ObjectTranslatable_t entity;
        
        private void InitializeList()
        {
            if (_translatable == null) return;

            reorderableList = new ReorderableList(_translatable.Translatables, typeof(CVRTranslatable.ObjectTranslatable_t),
                true, true, true, true);
            reorderableList.drawHeaderCallback = OnDrawHeader;
            reorderableList.drawElementCallback = OnDrawElement;
            reorderableList.elementHeightCallback = OnHeightElement;
            reorderableList.onAddCallback = OnAdd;
            reorderableList.onChangedCallback = OnChanged; 
        }

        private void OnDrawElement(Rect rect, int index, bool isactive, bool isfocused)
        {
            if (index > _translatable.Translatables.Count) return;
            entity = _translatable.Translatables[index];
            rect.y += 2;
            rect.x += 12;
            rect.width -= 12;
            
            EditorGUI.BeginChangeCheck();

            Rect _rect = new Rect(rect.x, rect.y, 120, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_rect, "Type");
            _rect.x += 120;
            _rect.width = rect.width - 120;

            entity.Type = (CVRTranslatable.TranslatableType) EditorGUI.EnumPopup(_rect, entity.Type);

            if (entity.Type != CVRTranslatable.TranslatableType.GameObject)
            {
                rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
                _rect = new Rect(rect.x, rect.y, 120, EditorGUIUtility.singleLineHeight);

                EditorGUI.LabelField(_rect, "Component");
                _rect.x += 120;
                _rect.width = rect.width - 120;

                switch (entity.Type)
                {
                    case CVRTranslatable.TranslatableType.Text:
                        entity.Text = (Text) EditorGUI.ObjectField(_rect, entity.Text, typeof(Text), true);
                        break;
#if CCK_ADDIN_TRANSLATABLE_TMP
                    case CVRTranslatable.TranslatableType.TextMeshPro:
                        entity.TmpText =
                            (TMP_Text) EditorGUI.ObjectField(_rect, entity.TmpText, typeof(TMP_Text), true);
                        break;
#endif
                    case CVRTranslatable.TranslatableType.AudioClip:
                        entity.Source =
                            (AudioSource) EditorGUI.ObjectField(_rect, entity.Source, typeof(AudioSource), true);
                        break;
                }
            }

            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 120, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_rect, "Fallback Language");
            _rect.x += 120;
            _rect.width = rect.width - 120;

            var usedLanguages = new List<string>();
            var usedLanguagesNames = new List<string>();
            foreach (var translation in entity.Translations)
            {
                if (!usedLanguages.Contains(translation.Language))
                {
                    var selectedUsedIndex = CVRTranslatable.Languages.Keys.ToList().FindIndex(match => match == translation.Language);
                    usedLanguages.Add(translation.Language);
                    usedLanguagesNames.Add(CVRTranslatable.Languages.Values.ToList()[selectedUsedIndex]);
                }
            }

            var selectedIndex = usedLanguages.FindIndex(match => match == entity.FallbackLanguage);

            selectedIndex = EditorGUI.Popup(_rect, selectedIndex, usedLanguagesNames.ToArray());

            if (selectedIndex >= 0)
            {
                entity.FallbackLanguage = usedLanguages.ToArray()[selectedIndex];
            }
            else if (usedLanguages.Count > 0)
            {
                entity.FallbackLanguage = usedLanguages[0];
            }
            else
            {
                entity.FallbackLanguage = "en";
            }
            
            rect.y += EditorGUIUtility.singleLineHeight * 1.25f;
            _rect = new Rect(rect.x, rect.y, 120, EditorGUIUtility.singleLineHeight);
            
            entity.GetList().DoList(new Rect(rect.x, rect.y, rect.width, 20f));
            
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(_translatable);
                Undo.RegisterCreatedObjectUndo (_translatable, "Changed Translatable");
            }
        }

        private float OnHeightElement(int index)
        {
            if (index > _translatable.Translatables.Count) return EditorGUIUtility.singleLineHeight * 1f;
            entity = _translatable.Translatables[index];

            /*if (entity.Type == CVRTranslatable.TranslatableType.GameObject)
            {
                return (entity.Translations.Count == 0 ? 1 : entity.Translations.Count * 2f + 5.25f) * 1.25f * EditorGUIUtility.singleLineHeight;
            }*/
            
            return ((entity.Translations.Count == 0 ? 1 : entity.Translations.Count * 
                   (entity.Type == CVRTranslatable.TranslatableType.AudioClip || entity.Type == CVRTranslatable.TranslatableType.GameObject?2f:4f)) + 5.25f ) * 1.25f *
                   EditorGUIUtility.singleLineHeight;
        }

        private void OnChanged(ReorderableList list)
        {
            EditorUtility.SetDirty(_translatable);
        }

        private void OnAdd(ReorderableList list)
        {
            _translatable.Translatables.Add(new CVRTranslatable.ObjectTranslatable_t());
            Repaint();
        }

        private void OnDrawHeader(Rect rect)
        {
            Rect _rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

            GUI.Label(_rect, "Translatables");
        }

        public override void OnInspectorGUI()
        {
            if (_translatable == null) _translatable = (CVRTranslatable) target;

            if (reorderableList == null) InitializeList();
            
            reorderableList.DoLayoutList();
        }
    }
}