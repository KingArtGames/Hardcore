using Assets.Scripts.manager;
using Assets.Scripts.map;
using Assets.Scripts.message.custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMessenger;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MapRenderer : MonoBehaviour
    {
        private IMessageBus _bus;
        private const string TILES = "Map/Tilesets/";

        public GameObject TilePrefab;

        private Dictionary<string, Sprite> _tiles;

        public void Start()
        {
            _tiles = new Dictionary<string, Sprite>();
            _bus = Initialiser.Instance.GetService<IMessageBus>();

            _bus.Subscribe<MapLoadedMessage>(OnMapLoaded);
            _bus.Publish<LoadMapMessage>(new LoadMapMessage(this));
        }

        private void OnMapLoaded(MapLoadedMessage msg)
        {
            Sprite[] all = Resources.LoadAll<Sprite>(TILES + msg.Map.tilesets[0].name);
            foreach (Sprite sp in all)
            {
                _tiles[sp.name] = sp;
            }

            foreach (TileLayer layer in msg.Map.layers)
            {
                int row = 0;
                int column = 0;
                for (int i = 0; i < layer.data.Length; i++ )
                {
                    column = i % msg.Map.width;

                    if(layer.data[i] - 1 >= 0)
                    {
                        GameObject tile = Instantiate(TilePrefab);
                        Vector3 pos = new Vector3(column * msg.Map.tilewidth * 0.01f, 0.1f, -row * msg.Map.tileheight * 0.01f);
                        tile.transform.position = pos;
                        tile.transform.SetParent(transform, false);

                        string id = msg.Map.tilesets[0].name + "_" + (layer.data[i] - 1);
                        tile.GetComponent<SpriteRenderer>().sprite = _tiles[id];
                    }
                   
                    if (i % (msg.Map.width) == 0 && i > 0) row++;
                }
            }
        }

    }
}
