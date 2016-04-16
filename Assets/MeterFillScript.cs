using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    void reduceByAmount(int amount)
    {
        this.fillAmount -= amount;
        fillImage.fillAmount -= amount / MAXAMOUNT;
    }

    void increaseByAmount(int amount)
    {
        this.fillAmount += amount;
        fillImage.fillAmount += amount / MAXAMOUNT;
    }

    void changeMusicStyle(int newStyle)
    {
        musicState = newStyle;
        // TODO: Change icon according to music style

    }
}
