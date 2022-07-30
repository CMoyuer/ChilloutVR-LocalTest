using System;
using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRAudioMaterialParser : MonoBehaviour
    {
        public bool useSeparateAudioSources = false;
        
        public AudioSource sourceAudio;

        public AudioSource sourceAudioL;
        public AudioSource sourceAudioR;

        public enum AudioDataType
        {
            OutputData = 0,
            SpectrumData = 1
        }

        public AudioDataType audioDataType = AudioDataType.OutputData;
        
        public Material processingMaterial;

        public int fragmentSize = 1024;

        public string fragmentParameterNameL1 = "_leftSamples1";
        public string fragmentParameterNameL2 = "_leftSamples2";
        public string fragmentParameterNameL3 = "_leftSamples3";
        public string fragmentParameterNameL4 = "_leftSamples4";
        
        public string fragmentParameterNameR1 = "_rightSamples1";
        public string fragmentParameterNameR2 = "_rightSamples2";
        public string fragmentParameterNameR3 = "_rightSamples3";
        public string fragmentParameterNameR4 = "_rightSamples4";

        public string volumeParameterName = "_volume";
        public string distanceParameterName = "_distance";
        public string pitchParameterName = "_pitch";
        public string dopplerParameterName = "_doppler";
        public string spatialParameterName = "_spatial";

        private void Start()
        {
            
        }
    }
}