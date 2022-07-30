using System;
using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRGlobalMaterialPropertyUpdater : MonoBehaviour
    {
        public Material material;

        public enum PropertyType
        {
            paramInt = 0,
            paramFloat = 1,
            paramVector4 = 2
        }

        public string propertyName;

        public PropertyType propertyType = PropertyType.paramFloat;

        public int intValue;

        public float floatValue;

        public Vector4 vector4Value;

        private void Start()
        {
            
        }
    }
}