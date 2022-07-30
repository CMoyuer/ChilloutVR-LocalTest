using System.Collections.Generic;
using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRAudioDriver : MonoBehaviour
    {
        public AudioSource audioSource;
        [SerializeField]
        public List<AudioClip> audioClips = new List<AudioClip>();
        public int selectedAudioClip = 0;
        public bool playOnSwitch = true;

        private int _selectedAudioClip = 0;

        private void OnDidApplyAnimationProperties()
        {
            if (selectedAudioClip != _selectedAudioClip)
            {
                if(SetAudioClip(selectedAudioClip) && playOnSwitch) PlaySound();
            }
        }

        private bool SetAudioClip(int index)
        {
            if (index < audioClips.Count)
            {
                if (audioClips[index] != null && audioSource != null)
                {
                    audioSource.clip = audioClips[index];
                    _selectedAudioClip = selectedAudioClip;
                    return true;
                }
            }

            return false;
        }

        public void PlaySound(int index)
        {
            if (SetAudioClip(index)) PlaySound();
        }
        public void PlaySound()
        {
            if (audioSource != null) audioSource.Play();
        }
        public void PlayNext()
        {
            if (_selectedAudioClip + 1 >= audioClips.Count)
            {
                PlaySound(0);
            }
            else
            {
                PlaySound(_selectedAudioClip + 1);
            }
        }
        public void PlayPrev()
        {
            if (_selectedAudioClip == 0)
            {
                PlaySound(audioClips.Count - 1);
            }
            else
            {
                PlaySound(_selectedAudioClip - 1);
            }
        }
        public void SelectRandomSound()
        {
            if (SetAudioClip(Random.Range(0, audioClips.Count)) && playOnSwitch) PlaySound();
        }
    }
}