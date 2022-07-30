using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CVRWarpPoint : MonoBehaviour
{
    [Header("CVR Warp Point (Will teleport you to the position of this object on ui interaction.)")]
    public string warpPointName;
    public string warpPointDescription;
}
