using UnityEngine;

namespace ABI.CCK.Scripts
{
    public class Health : MonoBehaviour
    {
        public string referenceID = string.Empty;
        
        [Header("Health")]
        public float healthBaseAmount = 100f;
        public float healthMaxAmount = 100f;
        [Header("Health Regeneration")] 
        public float healthRegenerationDelay = 0f;
        public float healthRegenerationRate = 0f;
        public float healthRegenerationCap = 0f;
        
        [Header("Armor")]
        public float armorBaseAmount = 0f;
        public float armorMaxAmount = 0f;
        [Header("Armor Regeneration")] 
        public float armorRegenerationDelay = 0f;
        public float armorRegenerationRate = 0f;
        public float armorRegenerationCap = 0f;
        
        [Header("Shield")]
        public float shieldBaseAmount = 0f;
        public float shieldMaxAmount = 0f;
        [Header("Shield Regeneration")] 
        public float shieldRegenerationDelay = 0f;
        public float shieldRegenerationRate = 0f;
        public float shieldRegenerationCap = 0f;
    }
}