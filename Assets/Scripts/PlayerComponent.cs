using UnityEngine;
using System.Collections;
using Assets.Scripts.entity;
using Assets.Scripts.manager;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;
using TinyMessenger;
using Assets.Scripts.message.custom;
using UnityEngine.Audio;
using UnityStandardAssets.Utility;
using Assets.Scripts.entity.modules;

public class PlayerComponent : MonoBehaviour 
{
    public GameObject EffectContainer;
    public Animator SpriteAnimator;

    public Light Spotlight;
    public AudioSource AudioSource;
    public float MorphSoundDelayed;
    public SpriteRenderer HeadSprite;
    public MeterFillScript UiFillBar;
    public float CoolDownTimer;

    [HideInInspector]
    public ParticleSystem activeAttack;
    public bool isWalking;

    private IEntityManager _entityManager;
    private Dictionary<AudioClip, float> _activeAudioSources;
    private IMessageBus _bus;
    private bool musicIsDropped = false;
    private bool PlayerIsInMusicBubble;
    private Time _startTime;

    private MusicTypes _activeMusikType;
    private AudioMixer _mixer; 

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

        _gameEntity = new GameEntity(new GameType(EntityTypes.player.ToString()));
        _gameEntity.AddModule<PlayerModule>(new PlayerModule(_gameEntity, _bus, new Data() { CurrentMusicType = new GameType(MusicTypes.metal.ToString()) }, new Template()));

        SwitchType(MusicTypes.metal);
        CoolDownTimer = 13;
    }

    private void Update()
    {
        if (!SpriteAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            SpriteAnimator.SetBool("isWalking", isWalking);

        if (transform.position.y < -10)
            _bus.Publish(new GameOverMessage(this));

        if (PlayerIsInMusicBubble)
        {
            if (_gameEntity.GetModule<PlayerModule>().BaseData.MusicHealthMeter <=100)
                _gameEntity.GetModule<PlayerModule>().BaseData.MusicHealthMeter += 1;
            UiFillBar.increaseByAmount(0.1f);
        }
        else
        {
            if (_gameEntity.GetModule<PlayerModule>().BaseData.MusicHealthMeter >= 0)
                _gameEntity.GetModule<PlayerModule>().BaseData.MusicHealthMeter -= 1;
            UiFillBar.reduceByAmount(0.1f);
        }

        if(CoolDownTimer >=0)
            CoolDownTimer -= Time.deltaTime;
    }
    private void Refresh()
    {
    }
    private void SwitchType(MusicTypes musikType)
    {
        Debug.Log(CoolDownTimer);
        if (musikType != _activeMusikType && CoolDownTimer < 0)
        {
            Refresh();

            GameEntity.GetModule<PlayerModule>().BaseData.CurrentMusicType.Value = musikType.ToString();

            if (AudioSource.clip != null && !_activeAudioSources.ContainsKey(AudioSource.clip))
                _activeAudioSources.Add(AudioSource.clip, AudioSource.time);
            else if (AudioSource.clip != null)
                _activeAudioSources[AudioSource.clip] = AudioSource.time;

            AudioSource.Stop();
            switchAnimation(_activeMusikType, musikType);

            if (musikType == MusicTypes.metal)
            {
                Spotlight.color = Color.blue;
                foreach (Light sp in Spotlight.GetComponentsInChildren<Light>())
                {
                    sp.color = new Color(0.0f, 0.082f, 1.0f, 1.0f);
                }
                AudioSource.clip = Resources.Load<AudioClip>("Audio/music/metal");
                AudioSource.outputAudioMixerGroup = _mixer.FindMatchingGroups("music_metal")[0];
            }
            if (musikType == MusicTypes.classic)
            {
                Spotlight.color = Color.red;
                foreach (Light sp in Spotlight.GetComponentsInChildren<Light>())
                {
                    sp.color = new Color(0.992f, 0.102f, 0.102f, 1.0f);
                }
                AudioSource.clip = Resources.Load<AudioClip>("Audio/music/classic");
                AudioSource.outputAudioMixerGroup = _mixer.FindMatchingGroups("music_classic")[0];
            }
            if (musikType == MusicTypes.techno)
            {
                Spotlight.color = Color.green;
                foreach (Light sp in Spotlight.GetComponentsInChildren<Light>())
                {
                    sp.color = new Color(0.176f, 0.659f, 0.176f, 1.0f);
                }
                AudioSource.clip = Resources.Load<AudioClip>("Audio/music/electro");
                AudioSource.outputAudioMixerGroup = _mixer.FindMatchingGroups("music_techno")[0];
            }
            _activeMusikType = musikType;
            InstantiateParticleEffect(musikType);

            GetAudioSourceTime();

            _gameEntity.GetModule<PlayerModule>().BaseData.MusicHealthMeter = 0;
            UiFillBar.setFillAmount(0);
            CoolDownTimer = 3;
        }
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
            AudioSource.Play();
        }
        else if (!AudioSource.isPlaying)
        {
            AudioSource.Play();
        }
    }

    private void switchAnimation(MusicTypes lastType, MusicTypes activeType)
    {
        Debug.Log(lastType.ToString() + "_to_" + activeType.ToString());
        SpriteAnimator.SetTrigger(lastType.ToString() + "_to_" + activeType.ToString());
        PlayMusicSwitchSound();
        Sprite sprite = Resources.Load<Sprite>("Animation/"+ activeType);
        Debug.Log(SpriteAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
        if (sprite != null && SpriteAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || SpriteAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            StartCoroutine(LateSetSpriteAfterAnimation(sprite));
    }

    private void PlayMusicSwitchSound()
    {
        AudioSource warpSource = Instantiate<AudioSource>(Resources.Load<AudioSource>("Audio/AudioSource"));
        warpSource.clip = Resources.Load<AudioClip>("Audio/sfx/warp");
        warpSource.outputAudioMixerGroup = _mixer.FindMatchingGroups("sfx")[0];
        warpSource.transform.SetParent(gameObject.transform);
        if (!warpSource.isPlaying)
            warpSource.Play();
        StartCoroutine(DestroyObjectAfterTime(warpSource.gameObject));
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
    public void ToggleDropMusic()
    {
        if (!musicIsDropped)
        {
            Spotlight.GetComponent<FollowTarget>().target = null;
            musicIsDropped = true;
        }
        else if(PlayerIsInMusicBubble)
        {
            Spotlight.GetComponent<FollowTarget>().target = transform;
            Spotlight.transform.position = transform.position;
            musicIsDropped = false;
        }
    }

    IEnumerator DestroyObjectAfterTime(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        Destroy(obj);
    }
    IEnumerator LateSetSpriteAfterAnimation(Sprite sprite)
    {
        HeadSprite.gameObject.SetActive(false); 
        yield return new WaitForSeconds(SpriteAnimator.GetCurrentAnimatorClipInfo(0).Length + 0.75f);
        HeadSprite.gameObject.SetActive(true); 
        HeadSprite.sprite = sprite;
    }

    void OnTriggerStay(Collider other)
    {
        if(!PlayerIsInMusicBubble)
            PlayerIsInMusicBubble = true;
    }
    void OnTriggerExit(Collider other)
    {
        if(PlayerIsInMusicBubble)
            PlayerIsInMusicBubble = false;
    }
}
