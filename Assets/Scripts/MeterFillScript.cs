﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.entity;
using TinyMessenger;
using Assets.Scripts.manager;
using Assets.Scripts.message.custom;
using System;

public class MeterFillScript : MonoBehaviour {

    const float MAXAMOUNT = 100; // "Prozent"
    public float gameTimer;
    public float fillAmount;
    private IMessageBus _bus;

    public Image fillImage;
    public Image timeImage;

	// Use this for initialization
	void Awake () {
        gameTimer = 300f;
        fillImage.fillAmount = 0.5f;
        _bus = Initialiser.Instance.GetService<IMessageBus>();
	}
	
	// Update is called once per frame
	void Update () {

        gameTimer -= Time.deltaTime;
        if (gameTimer <= 5)
        {
            _bus.Publish(new GameOverMessage(this));
        }
        try
        {
            timeImage.fillAmount = gameTimer / 300;
        }
        catch (NullReferenceException e)
        {

        }
        finally { }
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

}
