using System;
using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRBuilderSpawnable : MonoBehaviour
    {
        private void Reset()
        {
            if (GetComponent<CVRSpawnable>() != null)
            {
                Invoke("DestroyThis", 0);
            }
        }
        void DestroyThis() {
            DestroyImmediate(this);
        }
    }
}