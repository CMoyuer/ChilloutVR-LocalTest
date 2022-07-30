using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ABI.CCK.Components
{
    public class CVRParticleSound : MonoBehaviour
    {
        public ParticleSystem particleSystem;
        
        public AudioClip[] spawnSound;
        public AudioClip[] dieSound;

        public float spawnPlaybackVolume = 1f;
        public float diePlaybackVolume = 1f;
        
        private IDictionary<uint, ParticleSystem.Particle> _particles = new Dictionary<uint, ParticleSystem.Particle>();

        public AudioSource spawnAudioSourceReference;
        public AudioSource dieAudioSourceReference;
        
        private void Update()
        {
            if (particleSystem == null) return;

            ParticleSystem.Particle[] currentParticles = new ParticleSystem.Particle[particleSystem.particleCount];
            particleSystem.GetParticles(currentParticles);

            ParticleChange particleChange = GetParticleChange(currentParticles);

            if (spawnSound.Length > 0)
            {
                foreach (ParticleSystem.Particle particleAdded in particleChange.Added)
                {
                    AudioSource.PlayClipAtPoint(spawnSound[Random.Range(0, spawnSound.Length)], particleAdded.position, spawnPlaybackVolume);
                }
            }

            if (dieSound.Length > 0)
            {
                foreach (ParticleSystem.Particle particleRemoved in particleChange.Removed)
                {
                    AudioSource.PlayClipAtPoint(dieSound[Random.Range(0, dieSound.Length)], particleRemoved.position, diePlaybackVolume);
                }
            }
        }

        private ParticleChange GetParticleChange(ParticleSystem.Particle[] currentParticles)
        {
            ParticleChange particleChange = new ParticleChange();

            foreach (ParticleSystem.Particle activeParticle in currentParticles)
            {
                ParticleSystem.Particle foundParticle;
                if(_particles.TryGetValue(activeParticle.randomSeed, out foundParticle))
                {
                    _particles[activeParticle.randomSeed] = activeParticle;
                }
                else
                {
                    particleChange.Added.Add(activeParticle);
                    _particles.Add(activeParticle.randomSeed, activeParticle);
                }
            }

            var updatedParticleAsDictionary = currentParticles.ToDictionary(x => x.randomSeed, x => x);
            var dictionaryKeysAsList = _particles.Keys.ToList();

            foreach (uint dictionaryKey in dictionaryKeysAsList)
            {
                if (updatedParticleAsDictionary.ContainsKey(dictionaryKey) == false)
                {
                    particleChange.Removed.Add(_particles[dictionaryKey]);
                    _particles.Remove(dictionaryKey);
                }
            }
            
            return particleChange;
        }
        
        private class ParticleChange
        {
            public IList<ParticleSystem.Particle> Added = new List<ParticleSystem.Particle>();
            public IList<ParticleSystem.Particle> Removed = new List<ParticleSystem.Particle>();
        }
    }
}