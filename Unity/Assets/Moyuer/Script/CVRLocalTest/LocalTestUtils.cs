#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace MoyuerLocalTest
{
    // Author:  Moyuer
    // Github:  https://github.com/CMoyuer/ChilloutVR-LocalTest

    public abstract class LocalTestUtils
    {
        [MenuItem("Moyuer/CVR_LocalTest/Version: 0.3", false, 99)]
        private static void CheckNew()
        {
            Application.OpenURL("https://github.com/CMoyuer/ChilloutVR-LocalTest/releases/latest");
        }

        public static void SendUDPPacket(string msg)
        {
            new Thread(() =>
            {
                IPEndPoint remotePoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 25588);
                byte[] sendData = Encoding.Default.GetBytes(msg);
                UdpClient client = new UdpClient();
                client.Send(sendData, sendData.Length, remotePoint);
                client.Close();
            }).Start();
        }
    }
}
#endif
