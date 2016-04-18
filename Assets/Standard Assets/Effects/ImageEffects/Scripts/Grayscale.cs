using System;
using UnityEngine;
using UnityEngine.Audio;

namespace UnityStandardAssets.ImageEffects
{
    [ExecuteInEditMode]
    [AddComponentMenu("Image Effects/Color Adjustments/Grayscale")]
    public class Grayscale : ImageEffectBase {
        public Texture  textureRamp;

        public AudioSource AudioSource;
        public float Amplitude = 0.1f;
        public int Detail = 150;
        public float Delay = 5;

        float _velocity;


        [Range(-1.0f,1.0f)]
        public float    rampOffset;
        [Range(-1.0f, 1.0f)]
        public float Radius;
        public float Beat;

        // Called by camera to apply image effect
        void OnRenderImage (RenderTexture source, RenderTexture destination) {
            material.SetTexture("_RampTex", textureRamp);
            material.SetFloat("_RampOffset", rampOffset);
            var cam = GetComponent<Camera>();
            float ar = cam.aspect;
            material.SetFloat("_RadiusX", Radius * (1+Beat));
            material.SetFloat("_RadiusY", Radius * ar * (1 +Beat));
            Graphics.Blit (source, destination, material);
        }

        void Update()
        {
            float[] audioWaveInfo = new float[Detail];
            float WaveData = 0.0f;
            AudioSource.GetOutputData(audioWaveInfo, 0);
            for (int i = 0; i < audioWaveInfo.Length; i++)
            {
                WaveData += System.Math.Abs(audioWaveInfo[i]);
            }
            float beat = (WaveData * Amplitude);

            Beat = Mathf.SmoothDamp(Beat, beat, ref _velocity, Delay);
        }
    }
}
