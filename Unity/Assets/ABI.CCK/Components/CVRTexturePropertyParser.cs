using System;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

namespace ABI.CCK.Components
{
    public class CVRTexturePropertyParser : MonoBehaviour
    {
        public enum TextureType
        {
            LocalTexture = 0,
            GlobalTexture = 1,
        }

        public TextureType textureType = TextureType.LocalTexture;
        
        public RenderTexture texture;
        public string globalTextureName = "";
        
        public List<CVRTexturePropertyParserTask> tasks = new List<CVRTexturePropertyParserTask>();

        private void Update()
        {

        }
    }

    [System.Serializable]
    public class CVRTexturePropertyParserTask
    {
        public int x = 0;
        public int y = 0;

        public enum Channel
        {
            r = 0,
            g = 1,
            b = 2,
            a = 3,
        }
        public Channel channel = Channel.r;

        private Vector4[] conversionTable = new Vector4[]
        {
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 0, 0, 1)
        };

        public float minValue = 0f;
        public float maxValue = 1f;

        public GameObject target;
        public Component component;
        
        public string propertyName = "";
        public int typeIndex = 0;
        public int targetIndex = 0;
    }
}