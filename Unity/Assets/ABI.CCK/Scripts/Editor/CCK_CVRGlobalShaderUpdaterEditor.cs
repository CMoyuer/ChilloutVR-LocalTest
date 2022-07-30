using ABI.CCK.Components;
using UnityEditor;
using UnityEngine;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(ABI.CCK.Components.CVRGlobalShaderUpdater))]
    public class CCK_CVRGlobalShaderUpdaterEditor : UnityEditor.Editor
    {
        private CVRGlobalShaderUpdater _globalShaderUpdater;
        
        public override void OnInspectorGUI()
        {
            if (_globalShaderUpdater == null) _globalShaderUpdater = (CVRGlobalShaderUpdater) target;
            
            EditorGUI.BeginChangeCheck();

            bool updateValues = EditorGUILayout.Toggle("Update Values", _globalShaderUpdater.updateValues);

            Vector4 CVR_CCK_Global_1 = Vector4.zero;
            Vector4 CVR_CCK_Global_2 = Vector4.zero;
            Vector4 CVR_CCK_Global_3 = Vector4.zero;
            Vector4 CVR_CCK_Global_4 = Vector4.zero;
            
            if (updateValues)
            {
                CVR_CCK_Global_1 = EditorGUILayout.Vector4Field("CVR_CCK_Global_1", _globalShaderUpdater.CVR_CCK_Global_1);
                CVR_CCK_Global_2 = EditorGUILayout.Vector4Field("CVR_CCK_Global_2", _globalShaderUpdater.CVR_CCK_Global_2);
                CVR_CCK_Global_3 = EditorGUILayout.Vector4Field("CVR_CCK_Global_3", _globalShaderUpdater.CVR_CCK_Global_3);
                CVR_CCK_Global_4 = EditorGUILayout.Vector4Field("CVR_CCK_Global_4", _globalShaderUpdater.CVR_CCK_Global_4);
            }

            EditorGUILayout.Space();
            
            bool updateTexture = EditorGUILayout.Toggle("Update Texture", _globalShaderUpdater.updateTexture);

            RenderTexture renderTexture = null;
            string propertyName = "";

            if (updateTexture)
            {
                renderTexture = (RenderTexture) EditorGUILayout.ObjectField("Render Texture", _globalShaderUpdater.renderTexture, typeof(RenderTexture));
                propertyName = EditorGUILayout.TextField("Property Name", _globalShaderUpdater.propertyName);
            }
            
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed CVRGlobalShaderUpdater");

                _globalShaderUpdater.updateValues = updateValues;
                _globalShaderUpdater.CVR_CCK_Global_1 = CVR_CCK_Global_1;
                _globalShaderUpdater.CVR_CCK_Global_2 = CVR_CCK_Global_2;
                _globalShaderUpdater.CVR_CCK_Global_3 = CVR_CCK_Global_3;
                _globalShaderUpdater.CVR_CCK_Global_4 = CVR_CCK_Global_4;

                _globalShaderUpdater.updateTexture = updateTexture;
                _globalShaderUpdater.renderTexture = renderTexture;
                _globalShaderUpdater.propertyName = propertyName;
            }
        }
    }
}