using ABI.CCK.Components;
using UnityEditor;
using UnityEngine;
#if CCK_ADDIN_MAGICACLOTHSUPPORT
using MagicaCloth;
#endif

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(ABI.CCK.Components.CVRPointer), true)]
    public class CCK_CVRPointerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
#if CCK_ADDIN_MAGICACLOTHSUPPORT
            var components = ((CVRPointer) target).GetComponentsInParent<BaseCloth>();
            if (components.Length > 0)
            {
                EditorGUILayout.HelpBox(
                    "A MagicaCloth component was detected on this Object or its parent. This can lead to pointers to not work as intended.", 
                    MessageType.Error
                );
            }
#endif
        }
    }
}