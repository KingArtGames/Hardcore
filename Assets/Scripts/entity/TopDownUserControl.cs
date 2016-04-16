using Assets.Scripts.manager;
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Assets.Scripts.entity;
using System.Collections.Generic;
using TinyMessenger;
using Assets.Scripts.message.custom;

namespace UnityStandardAssets.Characters.ThirdPerson
{ 
    public class TopDownUserControl : MonoBehaviour
    {
        public float MovementSpeed;

        private TopDownCharacter m_Character;
        private IMessageBus _bus;
        
        private void Start()
        {
            m_Character = GetComponentInChildren<TopDownCharacter>();
            _bus = Initialiser.Instance.GetService<IMessageBus>();
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _bus.Publish(new PlayerChangedMusikTypeMessage(this, MusicTypes.Metal));
                m_Character = GetComponentInChildren<TopDownCharacter>();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _bus.Publish(new PlayerChangedMusikTypeMessage(this, MusicTypes.Classic));
                m_Character = GetComponentInChildren<TopDownCharacter>();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _bus.Publish(new PlayerChangedMusikTypeMessage(this, MusicTypes.Techno));
                m_Character = GetComponentInChildren<TopDownCharacter>();
            }
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
    }
}
