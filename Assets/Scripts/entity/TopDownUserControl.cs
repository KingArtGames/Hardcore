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

        private PlayerComponent _player;
        private IMessageBus _bus;
        
        private void Start()
        {
            _player = GetComponent<PlayerComponent>();
            _bus = Initialiser.Instance.GetService<IMessageBus>();
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                _bus.Publish(new PlayerChangedMusikTypeMessage(this, MusicTypes.metal));
            if (Input.GetKeyDown(KeyCode.Alpha2))
                _bus.Publish(new PlayerChangedMusikTypeMessage(this, MusicTypes.classic));
            if (Input.GetKeyDown(KeyCode.Alpha3))
                _bus.Publish(new PlayerChangedMusikTypeMessage(this, MusicTypes.techno));
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            if (_player != null && _player.activeAttack != null)
            {
                if (Input.GetMouseButton(0))
                {
                    if (!_player.activeAttack.isPlaying)
                        _player.activeAttack.Play();
                }
                else if (!Input.GetMouseButton(0))
                {
                    if (_player.activeAttack.isStopped)
                        _player.activeAttack.Stop();
                }
            }
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

            _player.FollowMouse();
            // pass all parameters to the character control script
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                _player.MoveForeward(-v * MovementSpeed); 
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                _player.MoveSidewards(h * MovementSpeed);
        }
    }
}
