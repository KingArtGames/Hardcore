using Assets.Scripts.entity;
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

        private bool _blocked;
        public Sprite Default;
        private bool _onTile;

        public static Dictionary<string, int> Timers = new Dictionary<string,int>();
        public static int START_TIME = 5;

        private static string _currentTileType = "";

        private void Refresh()
        {
            if (!Timers.ContainsKey(_tile.GetModule<TileModule>().BaseData.CurrentMusicType.Value))
                Timers[_tile.GetModule<TileModule>().BaseData.CurrentMusicType.Value] = START_TIME;

            _blocked = _tile.GetModule<TileModule>().IsBlocked();
            if (_blocked || _tile.GetModule<TileModule>().IsObstacle())
                GetComponent<BoxCollider>().size = new Vector3(7, 7, 50);
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (_currentTileType != _tile.GetModule<TileModule>().BaseData.CurrentMusicType.Value)
            {
                _currentTileType = _tile.GetModule<TileModule>().BaseData.CurrentMusicType.Value;
                Timers[_currentTileType] = START_TIME;
            }
            _onTile = true;
            if (_tile.GetModule<TileModule>().IsDestroyable() || _tile.GetModule<TileModule>().IsObstacle())
                Initialiser.Instance.GetService<IMessageBus>().Publish(new TileEnteredMessage(this, _tile.GetModule<TileModule>()));
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
            if (_tile.GetModule<TileModule>().IsDestroyable() && _onTile)
                Destroy(transform.parent.gameObject);
        }
        private IEnumerator<object> DropObstacle(int timer)
        {
            this.GetComponentInParent<Animation>().Play("Wobble");
            yield return new WaitForSeconds(timer);
            GetComponent<SpriteRenderer>().sprite = Default;
            _tile.GetModule<TileModule>().BaseData.CurrentMusicType = new GameType(MusicTypes.neutral.ToString());
            GetComponent<BoxCollider>().size = new Vector3(5, 5, 1);
        }

    }
}
