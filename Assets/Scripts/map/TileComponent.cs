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

        private bool _destroyable;
        private bool _blocked;
        private bool _isObstacle;
        public Sprite Default;

        private void Refresh()
        {
            _destroyable = _tile.GetModule<TileModule>().IsDestroyable();
            _blocked = _tile.GetModule<TileModule>().IsBlocked();
            if (_blocked)
                GetComponent<BoxCollider>().size = new Vector3(5, 5, 5);

            _isObstacle = _tile.GetModule<TileModule>().IsObstacle();
        }

        public void OnCollisionEnter(Collision collision)
        {
            if(_destroyable)
                Initialiser.Instance.GetService<IMessageBus>().Publish(new TileEnteredMessage(this, _tile.GetModule<TileModule>()));
        }

        public void OnCollisionExit(Collision collision)
        {
            _destroyable = false;
        }

        public void StartDropping(int i)
        {
            switch(i)
            {
                case 1:
                    StartCoroutine(DropTile());
                    break;
                case 2:
                    StartCoroutine(DropObstacle());
                    break;
            }
            
        }

        private IEnumerator<object> DropTile()
        {
            yield return new WaitForSeconds(3);
            if(_destroyable)
                Destroy(gameObject);
        }
        private IEnumerator<object> DropObstacle()
        {
            yield return new WaitForSeconds(3);
            GetComponent<SpriteRenderer>().sprite = Default;
            _tile.GetModule<TileModule>().BaseData.CurrentMusicType = new GameType(MusicTypes.neutral.ToString());
            GetComponent<BoxCollider>().size = new Vector3(5, 5, 1);
        }

    }
}
