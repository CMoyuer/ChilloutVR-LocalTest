using ABI.CCK.Components;
using UnityEditor;
using UnityEngine;

namespace ABI.CCK.Scripts.Editor
{
    [CustomEditor(typeof(CVRGlobalMaterialPropertyUpdater))]
    public class CCK_CVRGlobalMaterialPropertyUpdaterEditor : UnityEditor.Editor
    {
        private CVRGlobalMaterialPropertyUpdater _target;

        public override void OnInspectorGUI()
        {
            if (_target == null) _target = (CVRGlobalMaterialPropertyUpdater) target;
            
            EditorGUI.BeginChangeCheck();

            Material material = (Material) EditorGUILayout.ObjectField("Material", _target.material, typeof(Material));

            CVRGlobalMaterialPropertyUpdater.PropertyType type = (CVRGlobalMaterialPropertyUpdater.PropertyType) EditorGUILayout.EnumPopup("Property Type", _target.propertyType);

            string name = EditorGUILayout.TextField("Property Name", _target.propertyName);

            int intval = 0;
            float floatval = 0f;
            Vector4 vectorval = Vector4.zero;

            switch (type)
            {
                case CVRGlobalMaterialPropertyUpdater.PropertyType.paramInt:
                    intval = EditorGUILayout.IntField("Value", _target.intValue);
                    break;
                case CVRGlobalMaterialPropertyUpdater.PropertyType.paramFloat:
                    floatval = EditorGUILayout.FloatField("Value", _target.floatValue);
                    break;
                case CVRGlobalMaterialPropertyUpdater.PropertyType.paramVector4:
                    vectorval = EditorGUILayout.Vector4Field("Value", _target.vector4Value);
                    break;
            }

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed CVRGlobalMaterialPropertyUpdater");

                _target.material = material;
                _target.propertyType = type;
                _target.propertyName = name;

                _target.intValue = intval;
                _target.floatValue = floatval;
                _target.vector4Value = vectorval;
            }
        }
    }
}