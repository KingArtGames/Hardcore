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

        private bool _onTile;

        private void Refresh()
        {
            
        }

        public void OnCollisionEnter(Collision collision)
        {
            _onTile = true;
            Initialiser.Instance.GetService<IMessageBus>().Publish(new TileEnteredMessage(this, _tile.GetModule<TileModule>()));
            StartCoroutine(DropTile());
        }

        public void OnCollisionExit(Collision collision)
        {
            _onTile = false;
        }


        private IEnumerator<object> DropTile()
        {
            yield return new WaitForSeconds(3);
            if(_onTile)
                Destroy(gameObject);
        }

    }
}
