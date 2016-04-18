using Assets.Scripts.entity;
using Assets.Scripts.entity.modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityStandardAssets.Utility;

namespace Assets.Scripts
{
    public class EnemyComponent : MonoBehaviour
    {
        public Animator SpriteAnimator;
        public Light Spotlight;
        public SpriteRenderer HeadSprite;
        public SphereCollider Trigger;
        public float move = 10;

        public GameObject _target;

        private GameEntity _gameEntity;
        public GameEntity GameEntity
        {
            get { return _gameEntity; }
            set { _gameEntity = value; Refresh(); }
        }

        private void Refresh()
        {
            HeadSprite.sprite = Resources.Load<Sprite>("Characters/enemies/enemy_" + _gameEntity.GetModule<EnemyModule>().BaseData.CurrentMusicType.Value);
            SpriteAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/EnemyController/"+ _gameEntity.GetModule<EnemyModule>().BaseData.CurrentMusicType.Value);
            SetEnemyType(_gameEntity.GetModule<EnemyModule>().BaseData.CurrentMusicType.Value);
        }

        public void SetEnemyType(String musikType)
        {
            if (musikType == MusicTypes.metal.ToString())
            {
                Spotlight.color = Color.blue;
            }
            if (musikType == MusicTypes.classic.ToString())
            {
                Spotlight.color = Color.red;
            }
            if (musikType == MusicTypes.techno.ToString())
            {
                Spotlight.color = Color.green;
            }
        }
        public void Update()
        {
            AttackPlayer();
        }
        public void AttackPlayer()
        {
            if (_target != null)
            {
                transform.LookAt(_target.transform.position, Vector3.down);
                transform.localEulerAngles = new Vector3(90, transform.localEulerAngles.y, 0);
                transform.Translate(new Vector3(0, 1, 0) * move * Time.fixedDeltaTime);
                SpriteAnimator.SetBool("OnWalk",true);
            }
            else
            {
                SpriteAnimator.SetBool("OnWalk", false);
            }
        }

        public void OnParticleCollision(GameObject other)
        {
            Debug.Log(_gameEntity.GetModule<EnemyModule>().BaseData.CurrentMusicType.Value + " | " + other.name);
            if (CheckToKill(other.name))
                Destroy(transform.parent.gameObject);
        }

        public bool CheckToKill(String MusikType)
        {
            if (MusikType.Contains(MusicTypes.metal.ToString()) && _gameEntity.GetModule<EnemyModule>().BaseData.CurrentMusicType.Value == MusicTypes.techno.ToString())
            {
                return true;
            }
            else if (MusikType.Contains(MusicTypes.classic.ToString()) && _gameEntity.GetModule<EnemyModule>().BaseData.CurrentMusicType.Value == MusicTypes.metal.ToString())
            {
                return true;
            }
            else if (MusikType.Contains(MusicTypes.techno.ToString()) && _gameEntity.GetModule<EnemyModule>().BaseData.CurrentMusicType.Value == MusicTypes.classic.ToString())
            {
                return true;
            }
            else
                return false;

        }

        public void OnTriggerStay(Collider other)
        {
            PlayerComponent comp = other.gameObject.GetComponentInChildren<PlayerComponent>();
            if (comp != null && _gameEntity.GetModule<EnemyModule>().BaseData.CurrentMusicType.Value != comp.GameEntity.GetModule<PlayerModule>().BaseData.CurrentMusicType.Value)
                _target = comp.gameObject;
            else if (comp != null && _gameEntity.GetModule<EnemyModule>().BaseData.CurrentMusicType.Value == comp.GameEntity.GetModule<PlayerModule>().BaseData.CurrentMusicType.Value)
                _target = null;
        }
    }
}
