using ABI.CCK.Components;
using UnityEditor;
using UnityEngine;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(ABI.CCK.Components.CVRDescription))]
    public class CCK_CVRDescriptionEditor : UnityEditor.Editor
    {
        private CVRDescription _description;
        public override void OnInspectorGUI()
        {
            if (_description == null) _description = (CVRDescription) target;

            if (_description.locked)
            {
                switch (_description.type)
                {
                    case 0:
                        EditorGUILayout.HelpBox(_description.description, MessageType.None);
                        break;
                    case 1:
                        EditorGUILayout.HelpBox(_description.description, MessageType.Info);
                        break;
                    case 2:
                        EditorGUILayout.HelpBox(_description.description, MessageType.Warning);
                        break;
                    case 3:
                        EditorGUILayout.HelpBox(_description.description, MessageType.Error);
                        break;
                }

                if (!string.IsNullOrEmpty(_description.url))
                {
                    if (GUILayout.Button("Read more about this topic"))
                    {
                        Application.OpenURL(_description.url);
                    }
                }
            }
            else
            {
                EditorGUILayout.LabelField("Description");
                _description.description = EditorGUILayout.TextArea(_description.description);
                
                EditorGUILayout.LabelField("Documentation Url");
                _description.url = EditorGUILayout.TextField(_description.url);

                EditorGUILayout.LabelField("Icon Type");
                _description.type = EditorGUILayout.Popup(_description.type, new string[] { "None", "Info", "Warning", "Error" });
                
                if (GUILayout.Button("Lock info"))
                {
                    _description.locked = true;
                }
            }
        }
    }
}