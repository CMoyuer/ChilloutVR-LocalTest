using System;
using UnityEngine;

namespace ABI.CCK.Components
{
    public class ControlPoint : MonoBehaviour
    {
        public GameInstanceController gameInstanceController;
        public string referenceID;
        public float captureTime = 10f;
        public bool captureBonusForMultiplePeople = false;
        public float recaptureDelay = 30f;
        public int scorePerSecond = 1;
        
        private void Reset()
        {
            referenceID = Guid.NewGuid().ToString();
        }
    }
}