#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using static MoyuerLocalTest.LocalTestUtils;

namespace MoyuerLocalTest
{
    public class CVRSkipLogin : ScriptableObject
    {
        [MenuItem("Moyuer/CVR_LocalTest/Skip Login", false, 1)]
        private static void StartTest()
        {
            SendUDPPacket("{\"type\":\"skip_login\"}");
        }
    }
}
#endif