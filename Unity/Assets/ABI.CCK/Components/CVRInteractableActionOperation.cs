using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace ABI.CCK.Components
{
    [System.Serializable]
    public class CVRInteractableActionOperation
    {
        public enum ActionType
        {
            SetGameObjectActive = 1,
            //SetComponentActive = 2,
            SetAnimatorFloatValue = 3,
            SetAnimatorBoolValue = 4,
            SetAnimatorIntValue = 17,
            TriggerAnimatorTrigger = 18,
            SpawnObject = 5,
            TeleportPlayer = 6,
            TeleportObject = 7,
            ToggleAnimatorBoolValue = 8,
            SetAnimatorFloatRandom = 9,
            SetAnimatorBoolRandom = 10,
            SetAnimatorIntRandom = 19,
            SetAnimatorFloatByVar = 11,
            SetAnimatorIntByVar = 20,
            VariableBufferArithmetic = 12,
            DisplayWorldDetailPage = 13,
            DisplayInstanceDetailPage = 14,
            DisplayAvatarDetailPage = 15,
            SitAtPosition = 16,
            MethodCall = 21,
            SetSpawnableValue = 22,
            PlayAudio = 23,
            StopAudio = 24,
            SetAnimatorBoolByAPF= 25,
            SetAnimatorIntByAPF = 26,
            SetAnimatorFloatByAPF = 27,
            SetVariableBufferByAPF= 28,
            UpdateAPFTrigger = 29,
            UpdateAPFBool = 30,
            UpdateAPFInt = 31,
            UpdateAPFFloat = 32,
            UpdateAPFString = 33,
            SetPropertyByApf = 34,
            SetPropertyByValue = 35,
            DeleteGameObject = 36,
        }
        
        public ActionType type = ActionType.SetGameObjectActive;
        
        public List<GameObject> targets = new List<GameObject>();

        public float floatVal;
        public string stringVal;
        public string stringVal2;
        public string stringVal3;
        public string stringVal4;
        public bool boolVal;
        public bool boolVal2;
        public GameObject gameObjectVal;
        public float floatVal2 = 0f;
        public float floatVal3 = 0f;
        public float floatVal4 = 0f;
        public CVRVariableBuffer varBufferVal;
        public CVRVariableBuffer varBufferVal2;
        public CVRVariableBuffer varBufferVal3;
        public AnimationClip animationVal;
        
        [SerializeField]
        public UnityEvent customEvent;
    }
}