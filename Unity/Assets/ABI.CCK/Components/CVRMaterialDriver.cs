using System.Collections.Generic;
using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRMaterialDriver : MonoBehaviour
    {
        public float material01X;
        public float material01Y;
        public float material01Z;
        public float material01W;

        public float material02X;
        public float material02Y;
        public float material02Z;
        public float material02W;

        public float material03X;
        public float material03Y;
        public float material03Z;
        public float material03W;

        public float material04X;
        public float material04Y;
        public float material04Z;
        public float material04W;

        public float material05X;
        public float material05Y;
        public float material05Z;
        public float material05W;

        public float material06X;
        public float material06Y;
        public float material06Z;
        public float material06W;

        public float material07X;
        public float material07Y;
        public float material07Z;
        public float material07W;


        public float material08X;
        public float material08Y;
        public float material08Z;
        public float material08W;

        public float material09X;
        public float material09Y;
        public float material09Z;
        public float material09W;

        public float material10X;
        public float material10Y;
        public float material10Z;
        public float material10W;

        public float material11X;
        public float material11Y;
        public float material11Z;
        public float material11W;

        public float material12X;
        public float material12Y;
        public float material12Z;
        public float material12W;

        public float material13X;
        public float material13Y;
        public float material13Z;
        public float material13W;

        public float material14X;
        public float material14Y;
        public float material14Z;
        public float material14W;

        public float material15X;
        public float material15Y;
        public float material15Z;
        public float material15W;

        public float material16X;
        public float material16Y;
        public float material16Z;
        public float material16W;

        [HideInInspector]
        public List<CVRMaterialDriverTask> tasks = new List<CVRMaterialDriverTask>();
    }

    [System.Serializable]
    public class CVRMaterialDriverTask
    {
        public Renderer Renderer;
        public int Index = 0;
        public string RendererType = "";
        public string PropertyName = "";
        public CVRMaterialDriverTask.Type PropertyType;

        public enum Type
        {
            Float = 0,
            Vector4 = 1,
            Color = 2
        }

        public Vector4 values;
    }
}