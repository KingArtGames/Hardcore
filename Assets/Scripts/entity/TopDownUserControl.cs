using Assets.Scripts.manager;
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Assets.Scripts.entity;
using System.Collections.Generic;

namespace UnityStandardAssets.Characters.ThirdPerson
{ 
    public class TopDownUserControl : MonoBehaviour
    {
        public float MovementSpeed;
        public GameObject Type_Metal;
        public GameObject Type_Techno;
        public GameObject Type_Classic;


        private TopDownCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Dictionary<AudioSource, float> _activeAudioSources;

        IEntityManager _entityManager;
        
        private void Start()
        {
            _entityManager =  Initialiser.Instance.GetService<IEntityManager>();
            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponentInChildren<TopDownCharacter>();
            _activeAudioSources = new Dictionary<AudioSource,float>();
        }


        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
                SwitchType(MusicTypes.Metal);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SwitchType(MusicTypes.Classic);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                SwitchType(MusicTypes.Techno);
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                if (!m_Character.AttackRay.isPlaying)
                    m_Character.AttackRay.Play();
            }
            else if (!Input.GetMouseButton(0))
            {
                if (!m_Character.AttackRay.isStopped)
                    m_Character.AttackRay.Stop();
            }

            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

            m_Character.FollowMouse();
            // pass all parameters to the character control script
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                m_Character.MoveForeward(-v * MovementSpeed); 
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                m_Character.MoveSidewards(h * MovementSpeed);
        }

        private void SwitchType(MusicTypes type)
        {
            if (m_Character != null)
            {
                Transform lastTransform = m_Character.transform;

                if (!_activeAudioSources.ContainsKey(m_Character.AudioSource))
                    _activeAudioSources.Add(m_Character.AudioSource, m_Character.AudioSource.time);
                else
                    _activeAudioSources[m_Character.AudioSource] = m_Character.AudioSource.time;

                if (type == MusicTypes.Metal)
                {
                    Type_Metal.SetActive(true);
                    Type_Classic.SetActive(false);
                    Type_Techno.SetActive(false);
                    m_Character = GetComponentInChildren<TopDownCharacter>();
                }
                if (type == MusicTypes.Classic)
                {
                    Type_Metal.SetActive(false);
                    Type_Classic.SetActive(true);
                    Type_Techno.SetActive(false);
                    m_Character = GetComponentInChildren<TopDownCharacter>();
                }
                if (type == MusicTypes.Techno)
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
               // if(!m_Character.AudioSource.isPlaying)
                    m_Character.AudioSource.PlayScheduled(audioStartTime);     
        }
    }
}
