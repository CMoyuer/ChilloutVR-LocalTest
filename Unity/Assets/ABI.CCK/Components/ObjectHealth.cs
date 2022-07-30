using System;
using ABI.CCK.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace ABI.CCK.Components
{
    public class ObjectHealth : Health
    {
        public enum DownBehavior
        {
            Destroy = 0,
            RespawnAfterTime = 1,
        }
        [Header("Down Behavior")]
        public DownBehavior downBehavior = DownBehavior.Destroy;
        public float respawnTime = 10f;
        public Transform respawnPoint;
        
        [Header("Events")]
        public UnityEvent downEvent = new UnityEvent();
        public UnityEvent respawnEvent = new UnityEvent();

        private void Reset()
        {
            referenceID = Guid.NewGuid().ToString();
        }
    }
}