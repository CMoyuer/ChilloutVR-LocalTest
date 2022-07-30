using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace ABI.CCK.Components
{
    public class CVRCustomRenderTextureUpdater : MonoBehaviour
    {
        public CustomRenderTexture customRenderTexture;

        private void Update()
        {
            customRenderTexture.Update();
        }
    }
}