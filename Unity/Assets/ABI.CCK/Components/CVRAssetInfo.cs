using System;
using UnityEngine;
using UnityEngine.Serialization;


namespace ABI.CCK.Components
{
    public class CVRAssetInfo : MonoBehaviour
    {
        public enum AssetType
        {
            Avatar = 1,
            World = 2,
            Spawnable = 3
        }

        
        public AssetType type;
        public string objectId;
    }
}
