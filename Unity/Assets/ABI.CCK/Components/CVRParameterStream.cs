using System.Collections.Generic;
using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRParameterStream : MonoBehaviour
    {
        public enum ReferenceType
        {
            World = 0,
            Avatar = 1,
            Spawnable = 2
        }

        public ReferenceType referenceType = ReferenceType.World;
        
        public List<CVRParameterStreamEntry> entries = new List<CVRParameterStreamEntry>();
    }

    [System.Serializable]
    public class CVRParameterStreamEntry
    {
        public enum Type
        {
            TimeSeconds = 0,
            TimeSecondsUtc = 10,
            DeviceMode = 20,
            HeadsetOnHead = 30,
            ZoomFactor = 40,
            ZoomFactorCurve = 50,
            EyeMovementLeftX = 60,
            EyeMovementLeftY = 70,
            EyeMovementRightX = 80,
            EyeMovementRightY = 90,
            EyeBlinkingLeft = 100,
            EyeBlinkingRight = 110,
            VisemeLevel = 120,
            TimeSinceHeadsetRemoved = 130,
            TimeSinceLocalAvatarLoaded = 140,
            LocalWorldDownloadPercentage = 150,
            LocalFPS = 160,
            LocalPing = 170,
            LocalPlayerCount = 180,
            LocalTimeSinceFirstWorldJoin = 190,
            LocalTimeSinceWorldJoin = 200,
            LocalPlayerMuted = 210,
            LocalPlayerHudEnabled = 220,
            LocalPlayerNameplatesEnabled = 230,
            LocalPlayerHeight = 240,
            LocalPlayerControllerType = 250,
            LocalPlayerFullBodyEnabled = 260,
            TriggerLeftValue = 270,
            TriggerRightValue = 280,
            GripLeftValue = 290,
            GripRightValue = 300,
            GrippedObjectLeft = 310,
            GrippedObjectRight = 320
        }

        public Type type = Type.TimeSeconds;

        public enum TargetType
        {
            Animator = 0,
            VariableBuffer = 1,
            AvatarAnimator = 2,
            CustomFloat = 3,
        }

        public TargetType targetType = TargetType.Animator;

        public enum ApplicationType
        {
            Override = 0,
            AddToCurrent = 10,
            AddToStatic = 21,
            SubtractFromCurrent = 30,
            SubtractFromStatic = 41,
            SubtractWithCurrent = 50,
            SubtractWithStatic = 61,
            MultiplyWithCurrent = 70,
            MultiplyWithStatic = 81,
            CompareLessThen = 91,
            CompareLessThenEquals = 101,
            CompareEquals = 111,
            CompareMoreThenEquals = 121,
            CompareMoreThen = 131,
            Mod = 141,
            Pow = 151,
        }

        public ApplicationType applicationType = ApplicationType.Override;

        public float staticValue;
        
        public GameObject target;

        public string parameterName;
    }
}