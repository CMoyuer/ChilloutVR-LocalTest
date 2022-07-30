#if UNITY_EDITOR
using ABI.CCK.Components;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Threading;
using System.Net;
using System.Text;
using System.Net.Sockets;
using UnityEngine.SceneManagement;

public class CVRLocalWorldTest : EditorWindow
{
    private Vector2 mainScrollPos;
    [MenuItem("Moyuer/CVR_LocalTest/World", false, 1)]
    private static void StartTest()
    {
        var scenePath = SceneManager.GetActiveScene().path;
        var path = Application.dataPath.Replace("\\", "/");
        if (path.Contains("/")) path = path.Substring(0, path.LastIndexOf("/"));
        path += "/Build/";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        var report = BuildPipeline.BuildPlayer(new string[] { scenePath }, path + "temp", BuildTarget.StandaloneWindows64, BuildOptions.BuildAdditionalStreamedScenes);
        if (report == null)
        {
            EditorUtility.DisplayDialog("Error", "Built local world failed, please check the console log", "OK");
            return;
        }
        SendUDPPacket("{\"type\":\"change_local_world\",\"path\":\"" + (path + "temp") + "\"}");
        EditorUtility.DisplayDialog("Tip", "Built local world successfully , please go to the game to check the effect", "OK");
    }

    private static void SendUDPPacket(string msg)
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


    [MenuItem("Moyuer/CVR_LocalTest/Version: 0.2", false,99)]
    private static void CheckNew()
    {
        Application.OpenURL("https://github.com/CMoyuer/ChilloutVR-LocalTest/releases/latest");
    }

}
#endif