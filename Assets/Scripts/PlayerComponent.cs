﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.entity;
using Assets.Scripts.manager;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;
using TinyMessenger;
using Assets.Scripts.message.custom;

public class PlayerComponent : MonoBehaviour 
{
    public GameObject EffectContainer;
    public Animator SpriteAnimator;

    public Light Spotlight;
    public AudioSource AudioSource;

    [HideInInspector]
    public ParticleSystem activeAttack;

    private IEntityManager _entityManager;
    private Dictionary<AudioClip, float> _activeAudioSources;
    private IMessageBus _bus;

    private MusicTypes _activeMusikType;

    private GameEntity _gameEntity;
    public GameEntity GameEntity
    {
        get { return _gameEntity; }
        set { _gameEntity = value; Refresh(); }
    }

    public void Start ()
    {
        _bus = Initialiser.Instance.GetService<IMessageBus>();
        _bus.Subscribe<PlayerChangedMusikTypeMessage>(OnSwitchType);
        _activeAudioSources = new Dictionary<AudioClip, float>();
        _mixer = Resources.Load<AudioMixer>("Audio/Master");
        SwitchType(MusicTypes.metal);
    }

    private void Refresh()
    {
    }

    private void SwitchType(MusicTypes musikType)
    {
        Refresh();

        if (AudioSource.clip != null && !_activeAudioSources.ContainsKey(AudioSource.clip))
            _activeAudioSources.Add(AudioSource.clip, AudioSource.time);
        else if(AudioSource.clip != null)
            _activeAudioSources[AudioSource.clip] = AudioSource.time;

        switchAnimation(_activeMusikType, musikType);

        if (musikType == MusicTypes.metal)
        {
            Spotlight.color = Color.blue;
            AudioSource.clip = Resources.Load<AudioClip>("Audio/music/metal");
        }
        if (musikType == MusicTypes.classic)
        {
            Spotlight.color = Color.red;
            AudioSource.clip = Resources.Load<AudioClip>("Audio/music/classic");
        }
        if (musikType == MusicTypes.techno)
        {
            Spotlight.color = Color.green;
            AudioSource.clip = Resources.Load<AudioClip>("Audio/music/electro");
        }
        _activeMusikType = musikType;
        InstantiateParticleEffect(musikType);

        GetAudioSourceTime();      
    }

    public void OnSwitchType(PlayerChangedMusikTypeMessage msg)
    {
        SwitchType(msg.Type);
    }
    private void GetAudioSourceTime()
    {
        float audioStartTime;
        if (_activeAudioSources.TryGetValue(AudioSource.clip, out audioStartTime))
        {
            AudioSource.SetScheduledStartTime(audioStartTime);
            AudioSource.PlayScheduled(audioStartTime);
        }
        else if(!AudioSource.isPlaying)
            AudioSource.Play();
    }

    private void switchAnimation(MusicTypes lastType, MusicTypes activeType)
    {
        Debug.Log(lastType.ToString() + "To" + activeType.ToString());
        SpriteAnimator.SetTrigger(lastType.ToString() + "To" + activeType.ToString());
    }

    private void InstantiateParticleEffect(MusicTypes type)
    {
        if(activeAttack != null)
            Destroy(activeAttack.gameObject);
        ParticleSystem resource = Resources.Load<ParticleSystem>("Effects/Attacks/" + type.ToString());
        activeAttack = Instantiate<ParticleSystem>(resource);
        activeAttack.transform.SetParent(EffectContainer.transform);
        activeAttack.transform.localEulerAngles = new Vector3(270, 0, 0);
        activeAttack.transform.localPosition = Vector3.zero;

    }
    public void FollowMouse()
    {
        transform.rotation = GetMousePosition();
        transform.localEulerAngles = new Vector3(90, transform.localEulerAngles.y, 0);
    }

    public void MoveForeward(float move)
    {
        float distance = Vector3.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (distance > 5.5f)
        {
            transform.Translate(transform.forward * move * Time.fixedDeltaTime);
        }
    }
    public void MoveSidewards(float move)
    {
        transform.Translate(new Vector3(1, 0, 0) * move * Time.fixedDeltaTime);
    }

    private Quaternion GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;

        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 relativePos = targetPosition - transform.position;
        relativePos.y = 90;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        return rotation;
    }
}
