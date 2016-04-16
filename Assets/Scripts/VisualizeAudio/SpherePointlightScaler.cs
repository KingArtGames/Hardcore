using UnityEngine;
using System.Collections;
using System;

public class SpherePointlightScaler : MonoBehaviour {

    public Light SpotLight;
	
	void Update () {

        float radian = SpotLight.spotAngle * (Mathf.PI / 180);
        float value = Mathf.Tan(radian) * SpotLight.range;

        transform.localScale = new Vector3(value, value, value);
	}
}
