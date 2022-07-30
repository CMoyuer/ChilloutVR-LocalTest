using UnityEngine;

namespace ABI.CCK.Scripts.Runtime
{
    public class LinkOpener : MonoBehaviour
    {
        public void OpenLink(string url)
        {
            Application.OpenURL(url);
        }
    }
}
