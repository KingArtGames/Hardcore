using Assets.Scripts.entity;
using Assets.Scripts.entity.modules;
using Assets.Scripts.manager;
using Assets.Scripts.message.custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMessenger;
using UnityEngine;

namespace Assets.Scripts.map
{
    public class TileComponent : MonoBehaviour
    {
        private GameEntity _tile;
        public GameEntity Tile 
        {
            get { return _tile; }
            set { _tile = value; Refresh(); } 
        }
        private TileModule _tileModule;

        private bool _blocked;

        public Sprite Metal;
        public Sprite Classic;
        public Sprite Techno;
        public Sprite Default;

        private bool _onTile;

        public static Dictionary<string, int> Timers = new Dictionary<string,int>();
        public static int START_TIME = 5;

        private static string _currentTileType = "";

        private static Dictionary<string, Sprite> _musics;

        private void Refresh()
        {
            _tileModule = _tile.GetModule<TileModule>();
            if(_musics == null)
            {
                _musics = new Dictionary<string, Sprite>();
                _musics["metal"] = Metal;
                _musics["techno"] = Techno;
                _musics["classic"] = Classic;
            }

            if (!Timers.ContainsKey(_tileModule.BaseData.CurrentMusicType.Value))
                Timers[_tileModule.BaseData.CurrentMusicType.Value] = START_TIME;

            _blocked = _tileModule.IsBlocked();
            if (_blocked || _tileModule.IsObstacle())
                GetComponent<BoxCollider>().size = new Vector3(7, 7, 50);
        }

        public void OnCollisionEnter(Collision collision)
        {
            PlayerComponent player = collision.collider.GetComponent<PlayerComponent>();
            EnemyComponent enemy = collision.collider.GetComponent<EnemyComponent>();
            if (player != null)
            {
                if (_currentTileType != _tileModule.BaseData.CurrentMusicType.Value)
                {
                    _currentTileType = _tileModule.BaseData.CurrentMusicType.Value;
                    Timers[_currentTileType] = START_TIME;
                }
                _onTile = true;
                if (_tileModule.IsDestroyable() || _tileModule.IsObstacle())
                    Initialiser.Instance.GetService<IMessageBus>().Publish(new TileEnteredMessage(this, _tileModule));
            }
            else if (enemy != null && !_tileModule.IsObstacle() && !_tileModule.IsBlocked())
            {
                _tileModule.BaseData.CurrentMusicType = enemy.GameEntity.GetModule<EnemyModule>().BaseData.CurrentMusicType;
                GetComponent<SpriteRenderer>().sprite = _musics[enemy.GameEntity.GetModule<EnemyModule>().BaseData.CurrentMusicType.Value];
            }
        }

        public void OnCollisionExit(Collision collision)
        {
            _onTile = false;
        }

        public void StartDropping(int i, int timer)
        {
            switch(i)
            {
                case 1:
                    StartCoroutine(DropTile(timer));
                    break;
                case 2:
                    StartCoroutine(DropObstacle(timer));
                    break;
            }
            
        }

        private IEnumerator<object> DropTile(int timer)
        {
            this.GetComponentInParent<Animation>().Play("falling");
            if(timer > 0)
                yield return new WaitForSeconds(timer);
            if (_tileModule.IsDestroyable() && _onTile)
                Destroy(transform.parent.gameObject);
        }
        private IEnumerator<object> DropObstacle(int timer)
        {
            this.GetComponentInParent<Animation>().Play("Wobble");
            yield return new WaitForSeconds(timer);
            GetComponent<SpriteRenderer>().sprite = Default;
            _tileModule.BaseData.CurrentMusicType = new GameType(MusicTypes.neutral.ToString());
            GetComponent<BoxCollider>().size = new Vector3(5, 5, 1);
        }

    }
}
