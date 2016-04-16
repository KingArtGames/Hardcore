using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class WaveLengthSimulator : MonoBehaviour {

    public AudioSource AudioSource;
    public AudioListener AudioListener;
    public int Detail = 10;
    public float Amplitude = 0.1f;
    public float delay;
    public Light SpotLight;
    public int LightMultiplyer;

    private float startTime;
    private Vector3 _startPostion;
    private float _secondTimer;
    private float _lastValue;
    private float _velocity;
    private float _targetValue;

	void Start () {
        _startPostion = transform.localScale;
        _secondTimer = delay;
        _lastValue = 0.0f;
	}

	void Update () 
    {
        if (_secondTimer <= 0)
        {
            SetScaleByWave();
            _secondTimer = delay;
        }

        float newPosition = Mathf.SmoothDamp(_lastValue, _targetValue, ref _velocity, delay);
        transform.localScale = new Vector3(newPosition, newPosition, newPosition);
        if (SpotLight != null)
            SpotLight.spotAngle = newPosition * LightMultiplyer;
        _lastValue = newPosition;

        _secondTimer -= Time.smoothDeltaTime;
	}
    
    public void SetScaleByWave()
    {
        float[] audioWaveInfo = new float[Detail];
        float WaveData = 0.0f;
        AudioSource.GetOutputData(audioWaveInfo, 0);
        for (int i = 0; i < audioWaveInfo.Length; i++)
        {
            WaveData += System.Math.Abs(audioWaveInfo[i]);
        }
        _targetValue = _startPostion.y + (WaveData * Amplitude);
    }
}
