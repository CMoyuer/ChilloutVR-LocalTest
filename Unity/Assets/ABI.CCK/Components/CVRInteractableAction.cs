using System;
using System.Collections.Generic;
using UnityEngine;

namespace ABI.CCK.Components
{
    [System.Serializable]
    public class CVRInteractableAction
    {
        public enum ActionRegister
        {
            OnGrab = 1,
            OnDrop = 2,
            OnInteractDown = 3,
            OnInteractUp = 4,
            OnEnterTrigger = 5,
            OnExitTrigger = 6,
            OnEnterCollider = 7,
            OnExitCollider = 8,
            OnEnable = 9,
            OnDisable = 10,
            OnTimer = 11,
            OnParticleHit = 12,
            OnVariableBufferUpdate = 13,
            OnVariableBufferComparision = 14,
            OnCron = 15,
            OnPointerEnter = 16,
            OnWorldTrigger = 17,
            OnCustomTrigger = 18,
            OnInputDown = 19,
            OnInputUp = 20,
            OnAPFTrigger = 21,
            OnAPFBoolChange = 22,
            OnAPFIntChange = 23,
            OnAPFFloatChange = 24,
            OnAPFStringChange = 27,
            OnGazeEnter = 25,
            OnGazeExit = 26
        }

        public enum ExecutionType
        {
            LocalNotNetworked = 1,
            GlobalNetworked = 2,
            GlobalNetworkedBuffered = 4,
            GlobalInstanceOwnerOnly = 3,
            GlobalInstanceOwnerOnlyBuffered = 5,
            GlobalNetworkedAllInstanceModerators = 6,
            GlobalNetworkedAllInstanceModeratorsBuffered = 7
        }

        public float delay = 0f;
        
        public List<CVRInteractableActionOperation> operations = new List<CVRInteractableActionOperation>();
        
        public ActionRegister actionType = ActionRegister.OnInteractDown;
        public ExecutionType execType = ExecutionType.GlobalNetworked;

        public LayerMask layerMask = 0;

        public float floatVal = 0;
        public float floatVal2 = 0;
        public float floatVal3 = 0;
        public bool boolVal;
        public CVRVariableBuffer varBufferVal;
        public CVRVariableBuffer varBufferVal2;
        public string stringVal = "";

        public List<CVRPointer> allowedPointer = new List<CVRPointer>();
        public bool allowedPointerCollapse = false;
        public List<string> allowedTypes = new List<string>();
        public bool allowedTypesCollapse = false;

        public List<ParticleSystem> specificParticleSystems = new List<ParticleSystem>();
        public bool specificParticleSystemsCollapse = false;

        public enum InteractionFilter
        {
            Global = 0,
            Looking = 1,
            Attached = 2,
            Held = 3,
            Sitting = 4
        }

        public InteractionFilter interactionFilter = InteractionFilter.Global;

        public enum InteractionInput
        {
            A = KeyCode.A,
            B = KeyCode.B,
            C = KeyCode.C,
            D = KeyCode.D,
            E = KeyCode.E,
            F = KeyCode.F,
            G = KeyCode.G,
            H = KeyCode.H,
            I = KeyCode.I,
            J = KeyCode.J,
            K = KeyCode.K,
            L = KeyCode.L,
            M = KeyCode.M,
            N = KeyCode.N,
            O = KeyCode.O,
            P = KeyCode.P,
            Q = KeyCode.Q,
            R = KeyCode.R,
            S = KeyCode.S,
            T = KeyCode.T,
            U = KeyCode.U,
            V = KeyCode.V,
            W = KeyCode.W,
            X = KeyCode.X,
            Y = KeyCode.Y,
            Z = KeyCode.Z,
            Alpha0 = KeyCode.Alpha0,
            Alpha1 = KeyCode.Alpha1,
            Alpha2 = KeyCode.Alpha2,
            Alpha3 = KeyCode.Alpha3,
            Alpha4 = KeyCode.Alpha4,
            Alpha5 = KeyCode.Alpha5,
            Alpha6 = KeyCode.Alpha6,
            Alpha7 = KeyCode.Alpha7,
            Alpha8 = KeyCode.Alpha8,
            Alpha9 = KeyCode.Alpha9,
            InputHorizontalNegative = 10000,
            InputHorizontalPositive = 10001,
            InputVerticalNegative = 10002,
            InputVerticalPositive = 10003,
            InputJump = 10004,
            InputAccelerate = 10005,
            InputBrake = 10006,
            InputLeftClick = 10007,
            InputRightClick = 10008
        }

        public InteractionInput interactionInput = InteractionInput.Alpha0;

        public enum InteractionInputModifier
        {
            LeftCtrl = 1,
            LeftShift = 2,
            LeftAlt = 4,
            RightCtrl = 8,
            RightShift = 16,
            RightAlt = 32
        }

        public InteractionInputModifier interactionInputModifier = 0;
        
        [HideInInspector]
        public string guid = "";

        private bool IsInLayerMask(GameObject obj, LayerMask inLayerMask)
        {
            return ((inLayerMask.value & (1 << obj.layer)) > 0);
        }
    }
}