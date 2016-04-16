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
        m_Character = GetComponentInChildren<TopDownCharacter>();
    }

    private void SwitchType(PlayerChangedMusikTypeMessage msg)
    {
        Refresh();
        if (m_Character != null)
        {
            Transform lastTransform = m_Character.transform;

            if (!_activeAudioSources.ContainsKey(m_Character.AudioSource))
                _activeAudioSources.Add(m_Character.AudioSource, m_Character.AudioSource.time);
            else
                _activeAudioSources[m_Character.AudioSource] = m_Character.AudioSource.time;

            if (msg.Type == MusicTypes.Metal)
            {
                Type_Metal.SetActive(true);
                Type_Classic.SetActive(false);
                Type_Techno.SetActive(false);
                m_Character = GetComponentInChildren<TopDownCharacter>();
            }
            if (msg.Type == MusicTypes.Classic)
            {
                Type_Metal.SetActive(false);
                Type_Classic.SetActive(true);
                Type_Techno.SetActive(false);
                m_Character = GetComponentInChildren<TopDownCharacter>();
            }
            if (msg.Type == MusicTypes.Techno)
            {
                Type_Metal.SetActive(false);
                Type_Classic.SetActive(false);
                Type_Techno.SetActive(true);
                m_Character = GetComponentInChildren<TopDownCharacter>();
            }
            m_Character.transform.position = lastTransform.position;
            m_Character.transform.rotation = lastTransform.rotation;

            GetAudioSourceTime();
        }
    }
    private void GetAudioSourceTime()
    {
        float audioStartTime;
        if (_activeAudioSources.TryGetValue(m_Character.AudioSource, out audioStartTime))
        {
            m_Character.AudioSource.SetScheduledStartTime(audioStartTime);
            m_Character.AudioSource.PlayScheduled(audioStartTime);
        }
    } 
}
