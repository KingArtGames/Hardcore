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
        private GameEntity _gameEntity;
        public GameEntity GameEntity
        {
            get { return _gameEntity; }
            set { _gameEntity = value; Refresh(); }
        }

        private void Refresh()
        {

        }

        public void OnParticleCollision(GameObject other)
        {
            Debug.Log(_gameEntity.GetModule<EnemyModule>().BaseData.CurrentMusicType.Value+" | " + other.name);
            if (other.name.Contains(_gameEntity.GetModule<EnemyModule>().BaseData.CurrentMusicType.Value))
                Destroy(transform.parent.gameObject);
        }
    }
}
