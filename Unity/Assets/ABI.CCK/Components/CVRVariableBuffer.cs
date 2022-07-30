using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

#pragma warning disable

namespace ABI.CCK.Components
{
    public class CVRVariableBuffer : MonoBehaviour
    {
        public float defaultValue = 0f;
        
        [HideInInspector]
        public float value = 0f;
        
        [HideInInspector]
        public List<CVRInteractable> affectedInteractables = new List<CVRInteractable>();
        private bool sendUpdate = true;

        public void Start()
        {
            value = defaultValue;
        }

        public void AddInteracteable(CVRInteractable interactable)
        {
            if (!affectedInteractables.Contains(interactable))
            {
                affectedInteractables.Add(interactable);
            }

            RemoveOrphans();
        }

        private void RemoveOrphans()
        {
            var interactablesToRemove = new List<CVRInteractable>();
            
            foreach (var interactable in affectedInteractables)
            {
                var included = false;
                
                if (interactable == null) continue;
                
                foreach (var action in interactable.actions)
                {
                    if (action.varBufferVal == this) included = true;
                    if (action.varBufferVal2 == this) included = true;
                    foreach (var operation in action.operations)
                    {
                        if (operation.varBufferVal == this) included = true;
                        if (operation.varBufferVal2 == this) included = true;
                        if (operation.varBufferVal3 == this) included = true;
                    }
                }

                if (!included) interactablesToRemove.Add(interactable);
            }

            foreach (var interactable in interactablesToRemove)
            {
                affectedInteractables.Remove(interactable);
            }
        }
        
        public void SetValue(float _value){}
    }
}