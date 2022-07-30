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

public class CVRLocalAvatarTest : EditorWindow
{
    private Vector2 mainScrollPos;
    private CVRAvatar avatar;
    [MenuItem("Moyuer/CVR_LocalTest/Avatar")]
    private static void ShowWindow()
    {
        GetWindow<CVRLocalAvatarTest>();
    }
    private void OnGUI()
    {
        mainScrollPos = GUILayout.BeginScrollView(mainScrollPos);
        GUILayout.Space(10);
        GUI.skin.label.fontSize = 24;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("CVR Avatar Test");
        GUI.skin.label.fontSize = 12;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("v0.1");
        GUI.skin.label.fontSize = 12;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("by: 如梦(Moyuer)");
        GUILayout.Space(10);
        GUI.skin.label.fontSize = 12;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("ChilloutVR Local Avatar Test");
        GUILayout.Space(10);
        avatar = (CVRAvatar)EditorGUILayout.ObjectField("Avatar", avatar, typeof(CVRAvatar), true);
        GUILayout.Space(10);
        if (avatar == null) EditorGUILayout.HelpBox("Please select your Avatar.", MessageType.Warning);
        if (GUILayout.Button("Start Test") && avatar != null)
        {
            Fix(avatar.gameObject);
        }
        GUILayout.EndScrollView();
    }

    private static void Fix(GameObject avatar)
    {
        avatar.SetActive(true);
        var prefabPath = "Assets/temp.prefab";
        if (PrefabUtility.SaveAsPrefabAsset(avatar, prefabPath) == null)
        {
            EditorUtility.DisplayDialog("Error", "An error occurred, please check the Console log", "OK");
            return;
        }
        var path = Application.dataPath.Replace("\\", "/");
        if (path.Contains("/")) path = path.Substring(0, path.LastIndexOf("/"));
        path += "/Build/";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        var builds = new List<AssetBundleBuild>();
        var build = new AssetBundleBuild();
        build.assetBundleName = "temp";
        build.assetNames = new string[] { prefabPath };
        builds.Add(build);
        if (BuildPipeline.BuildAssetBundles(path, builds.ToArray(), BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows) == null)
        {
            EditorUtility.DisplayDialog("Error", "An error occurred, please check the Console log", "OK");
            return;
        }
        SendUDPPacket("{\"type\":\"change_local_avatar\",\"path\":\"" + (path + "temp") + "\"}");
        EditorUtility.DisplayDialog("Tip", "The local avatar is successfully built, please go to the game to check the effect", "OK");
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
}
#endif