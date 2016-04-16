using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveLengthSimulator : MonoBehaviour {

    public AudioSource AudioSource;
    public AudioListener AudioListener;
    public int Detail = 400;
    public float Amplitude = 0.1f;
    private Vector3 _startPostion;
	void Start () {
        _startPostion = transform.position;
	}

	void Update () {

        float[] audioWaveInfo = new float[Detail];
        float WaveData = 0.0f;
        AudioSource.GetOutputData(audioWaveInfo, 0);
        for(int i = 0 ; i < audioWaveInfo.Length; i++)
        {
            WaveData += System.Math.Abs(audioWaveInfo[i]);
        }

        float value = _startPostion.y * WaveData * Amplitude;
        transform.localPosition.Set(0, value, 0);
	}
}
