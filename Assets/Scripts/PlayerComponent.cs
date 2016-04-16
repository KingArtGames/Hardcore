using UnityEngine;
using System.Collections;
using Assets.Scripts.entity;
using Assets.Scripts.manager;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;
using TinyMessenger;
using Assets.Scripts.message.custom;

public class PlayerComponent : MonoBehaviour 
{
    public GameObject Type_Metal;
    public GameObject Type_Techno;
    public GameObject Type_Classic;

    [HideInInspector]
    public TopDownCharacter ActiveType;

    private IEntityManager _entityManager;
    private Dictionary<AudioSource, float> _activeAudioSources = new Dictionary<AudioSource, float>();
    private TopDownCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    private IMessageBus _bus;

    private GameEntity _gameEntity;
    public GameEntity GameEntity
    {
        get { return _gameEntity; }
        set { _gameEntity = value; Refresh(); }
    }

    public void Start ()
    {
        _bus = Initialiser.Instance.GetService<IMessageBus>();
        _bus.Subscribe<PlayerChangedMusikTypeMessage>(SwitchType);
    }

    private void Refresh()
    {
        ActiveType = GetComponentInChildren<TopDownCharacter>();
    }

    private void SwitchType(PlayerChangedMusikTypeMessage msg)
    {
        Refresh();
        if (ActiveType != null)
        {
            Transform lastTransform = ActiveType.transform;

            if (!_activeAudioSources.ContainsKey(ActiveType.AudioSource))
                _activeAudioSources.Add(ActiveType.AudioSource, ActiveType.AudioSource.time);
            else
                _activeAudioSources[ActiveType.AudioSource] = ActiveType.AudioSource.time;

            if (msg.Type == MusicTypes.Metal)
            {
                Type_Metal.SetActive(true);
                Type_Classic.SetActive(false);
                Type_Techno.SetActive(false);
                ActiveType = GetComponentInChildren<TopDownCharacter>();
            }
            if (msg.Type == MusicTypes.Classic)
            {
                Type_Metal.SetActive(false);
                Type_Classic.SetActive(true);
                Type_Techno.SetActive(false);
                ActiveType = GetComponentInChildren<TopDownCharacter>();
            }
            if (msg.Type == MusicTypes.Techno)
            {
                Type_Metal.SetActive(false);
                Type_Classic.SetActive(false);
                Type_Techno.SetActive(true);
                ActiveType = GetComponentInChildren<TopDownCharacter>();
            }
            ActiveType.transform.position = lastTransform.position;
            ActiveType.transform.rotation = lastTransform.rotation;

            GetAudioSourceTime();
        }
    }
    private void GetAudioSourceTime()
    {
        float audioStartTime;
        if (_activeAudioSources.TryGetValue(ActiveType.AudioSource, out audioStartTime))
        {
            ActiveType.AudioSource.SetScheduledStartTime(audioStartTime);
            ActiveType.AudioSource.PlayScheduled(audioStartTime);
        }
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
