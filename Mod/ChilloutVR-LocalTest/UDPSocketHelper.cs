using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ChilloutVR_LocalTest
{
    public class UDPSocketHelper : MonoBehaviour
    {
        public static UDPSocketHelper _interface;
        public static UDPSocketHelper Interface
        {
            get
            {
                if (_interface == null)
                {
                    var obj = new GameObject("UDPSocketHelper");
                    DontDestroyOnLoad(obj);
                    _interface = obj.AddComponent<UDPSocketHelper>();
                }
                return _interface;
            }
        }

        private UdpClient client;
        private Thread thread = null;
        private IPEndPoint remotePoint;
        public int port = 25588;

        public Action<string, string> OnMessageReceive = null;

        private string receiveString = null;

        void Start()
        {
            remotePoint = new IPEndPoint(IPAddress.Any, 0);
            thread = Loom.RunAsync(ReceiveMsg);
        }

        void ReceiveMsg()
        {
            while (true)
            {
                client = new UdpClient(port);
                byte[] receiveData = client.Receive(ref remotePoint);
                receiveString = Encoding.UTF8.GetString(receiveData);
                if (!string.IsNullOrEmpty(receiveString))
                {
                    Loom.QueueOnMainThread(() =>
                    {
                        var remoteIP = remotePoint.Address.ToString();
                        if (OnMessageReceive != null)
                        {
                            // Debug.Log(remotePoint.Address + ":" + remotePoint.Port + " ---> " + receiveString);
                            OnMessageReceive.Invoke(remoteIP, receiveString);
                            receiveString = null;
                        }
                    });
                }
                client.Close();
            }
        }

        public void SendMsg(string ip, int port, string _msg)
        {
            Loom.RunAsync(() =>
            {
                IPEndPoint remotePoint = new IPEndPoint(IPAddress.Parse(ip), port);
                if (_msg != null)
                {
                    byte[] sendData = Encoding.Default.GetBytes(_msg);
                    UdpClient client = new UdpClient();
                    client.Send(sendData, sendData.Length, remotePoint);
                    client.Close();
                }
            });
        }

        void OnDestroy()
        {
            if (thread != null)
            {
                thread.Abort();
                thread.Interrupt();
                thread = null;
            }
            if (client != null)
            {
                client.Close();
                client = null;
            }
            _interface = null;
        }
    }
}