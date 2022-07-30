using UnityEditor;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(ABI.CCK.Components.CVRAction))]
    public class CCK_CVR_ActionEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("This component is not yet ready to use!", MessageType.Error);
            
            base.DrawDefaultInspector();
        }
    }
}