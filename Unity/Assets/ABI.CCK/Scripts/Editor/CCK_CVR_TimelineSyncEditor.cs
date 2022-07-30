using UnityEditor;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(ABI.CCK.Components.CVRTimelineSync))]
    public class CCK_CVR_TimelineSyncEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("This component is not yet ready to use!", MessageType.Error);
            
            base.DrawDefaultInspector();
        }
    }
}