using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.entity;

public class MeterFillScript : MonoBehaviour {

    const float MAXAMOUNT = 100; // "Prozent"
    public float fillAmount;
    public float changeRate;

    public int musicState; // 1 = Metal, 2 = Techno, 3 = Klassik (ALTERNATIV VON PLAYER HOLEN?)

    public Image fillImage;
    public Image iconImage;

	// Use this for initialization
	void Awake () {
        fillImage.fillAmount = 30 / MAXAMOUNT;
        fillAmount = 30 / MAXAMOUNT;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void reduceByAmount(float amount)
    {
        this.fillAmount -= amount;
        fillImage.fillAmount -= amount / MAXAMOUNT;
    }

    public void increaseByAmount(float amount)
    {
        this.fillAmount += amount;
        fillImage.fillAmount += amount / MAXAMOUNT;
    }

    public void setFillAmount(float amount)
    {
        fillImage.fillAmount = amount;
    }

    public void changeMusicStyle(int newStyle)
    {
        musicState = newStyle;
        // TODO: Change icon according to music style

    }
}
