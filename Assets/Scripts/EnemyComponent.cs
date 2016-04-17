using Assets.Scripts.entity;
using Assets.Scripts.entity.modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyComponent : MonoBehaviour
    {
        public Animator SpriteAnimator;
        public Light Spotlight;
        public SpriteRenderer HeadSprite;

        private GameEntity _gameEntity;
        public GameEntity GameEntity
        {
            get { return _gameEntity; }
            set { _gameEntity = value; Refresh(); }
        }

        private void Refresh()
        {
            HeadSprite.sprite = Resources.Load<Sprite>("Characters/enemies/enemy_" + _gameEntity.GetModule<EnemyModule>().BaseData.CurrentMusicType.Value);
            //SpriteAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Enemy");
        }

        public void OnParticleCollision(GameObject other)
        {
            Debug.Log(_gameEntity.GetModule<EnemyModule>().BaseData.CurrentMusicType.Value+" | " + other.name);
            if (CheckToKill(other))
                Destroy(transform.parent.gameObject);
        }

        public bool CheckToKill(GameObject particleSource)
        {
            if (particleSource.name.Contains(MusicTypes.metal.ToString()) && _gameEntity.GetModule<EnemyModule>().BaseData.CurrentMusicType.Value == MusicTypes.techno.ToString())
            {
                return true;
            }
            else if (particleSource.name.Contains(MusicTypes.classic.ToString()) && _gameEntity.GetModule<EnemyModule>().BaseData.CurrentMusicType.Value == MusicTypes.metal.ToString())
            {
                return true;
            }
            else if (particleSource.name.Contains(MusicTypes.techno.ToString()) && _gameEntity.GetModule<EnemyModule>().BaseData.CurrentMusicType.Value == MusicTypes.classic.ToString())
            {
                return true;
            }
            else
                return false;
            
        }
    }
}
