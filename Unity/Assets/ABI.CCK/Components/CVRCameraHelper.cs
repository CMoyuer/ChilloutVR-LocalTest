using System.Collections.Generic;
using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRCameraHelper : MonoBehaviour
    {
        public Camera cam;
        public bool setAsMirroringCamera;

        public List<Shader> replacementShaders = new List<Shader>();
        public int selectedShader = -1;
        
        public void TakeScreenshot()
        {
            
        }
    }
}