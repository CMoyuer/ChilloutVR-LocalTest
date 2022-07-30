using ABI.CCK.Components;
using UnityEditor;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(CVRToggleStateTrigger))]
    public class CCK_CVRToggleStateTriggerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var triggers = ((CVRToggleStateTrigger) target).GetComponents<CVRToggleStateTrigger>();

            if (triggers.Length > 1)
            {
                EditorGUILayout.HelpBox("Having multiple Triggers on the same GameObject will lead to unpredictable behavior!", MessageType.Error);
            }
            
            base.OnInspectorGUI();
        }
    }
}