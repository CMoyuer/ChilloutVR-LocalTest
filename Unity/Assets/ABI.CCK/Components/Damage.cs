using UnityEngine;

namespace ABI.CCK.Components
{
    public class Damage : MonoBehaviour
    {
        public enum DamageType
        {
            Stack = 0,
            Shield = 1,
            Armor = 2,
            Health = 3,
        }

        public DamageType damageType = DamageType.Stack;

        public float damageAmount = 10f;

        [Header("Damage over time")] 
        public float damageOverTimeAmount = 0f;
        public float damageOverTimeDuration = 0f;
        public bool damageOverTimeContact = false;

        [Header("Damage Multiplier")]
        public float healthMultiplier = 1f;
        public float armorMultiplier = 1f;
        public float shieldMultiplier = 1f;
    }
}