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

        private bool _destroyTile;
        private bool _blocked;
        private bool _isObstacle;

        private void Refresh()
        {
            _destroyTile = _tile.GetModule<TileModule>().IsDestroyable();
            _blocked = _tile.GetModule<TileModule>().IsBlocked();
            if (_blocked)
                GetComponent<BoxCollider>().size = new Vector3(5, 5, 5);

            _isObstacle = _tile.GetModule<TileModule>().IsObstacle();
        }

        public void OnCollisionEnter(Collision collision)
        {
            Initialiser.Instance.GetService<IMessageBus>().Publish(new TileEnteredMessage(this, _tile.GetModule<TileModule>()));
            StartCoroutine(DropTile());
        }

        public void OnCollisionExit(Collision collision)
        {
            _destroyTile = false;
        }


        private IEnumerator<object> DropTile()
        {
            yield return new WaitForSeconds(3);
            if(_destroyTile)
                Destroy(gameObject);
        }

    }
}
