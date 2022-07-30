using System.Collections.Generic;
using UnityEngine;

namespace ABI.CCK.Components
{
    public class GunController : MonoBehaviour
    {
        [ReadOnly]
        public string referenceID;
        
        public ParticleSystem particleSystem;
        public List<AudioClip> shootSounds = new List<AudioClip>();
        public List<AudioClip> reloadSounds = new List<AudioClip>();
        public List<AudioClip> emptyShootSounds = new List<AudioClip>();
        public int magazineSize = 1;
        public int ammoCapacity = 10;

        public enum FiringMode
        {
            Single = 0,
            HalfAuto = 1,
            FullAuto = 2,
        }

        public FiringMode firingMode = FiringMode.HalfAuto;
        public float firingRate = 1f;
        public float reloadTime = 1f;

        public void Shoot()
        {
            
        }

        public void ShootDown()
        {
            
        }

        public void ShootUp()
        {
            
        }

        public void Reload()
        {
            
        }

        public void GrantReserveAmmo(int amount)
        {
            
        }
        
        public void GrantMagazineAmmo(int amount)
        {
            
        }
    }
}