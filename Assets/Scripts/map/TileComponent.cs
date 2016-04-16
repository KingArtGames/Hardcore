using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.map
{
    public class TileComponent : MonoBehaviour
    {
        private Tile _tile;
        public Tile Tile 
        {
            get { return _tile; }
            set { _tile = value; Refresh(); } 
        }

        private void Refresh()
        {
            
        }

        public void OnCollisionEnter(Collision collision)
        {
            Debug.Log("collision");
        }

    }
}
