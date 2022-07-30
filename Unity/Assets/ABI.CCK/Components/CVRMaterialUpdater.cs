using System;
using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRMaterialUpdater : MonoBehaviour
    {
        public enum UpdateType
        {
            Update = 0,
            FixedUpdate = 1,
        }

        public UpdateType updateType = UpdateType.Update;

        private Renderer renderer;
        private Vector3 lastPos;
        private Vector3 velocity;
        private Vector3 lastRot;  
        private Vector3 angularVelocity;

        private void Start()
        {
            renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            if (updateType == UpdateType.FixedUpdate || renderer == null) return;
            ProcessUpdate();
        }

        private void FixedUpdate()
        {
            if (updateType == UpdateType.Update || renderer == null) return;
            ProcessUpdate();
        }

        private void ProcessUpdate()
        {
            velocity = (lastPos - transform.position) / (updateType == UpdateType.Update?Time.deltaTime:Time.fixedDeltaTime);
            angularVelocity = transform.rotation.eulerAngles - lastRot;
            
            renderer.material.SetVector("_CVR_Velocity", velocity);
            renderer.material.SetVector("_CVR_Angular_Velocity", angularVelocity);
            
            lastPos = transform.position;
            lastRot = transform.rotation.eulerAngles;
        }
    }
}