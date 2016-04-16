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

            _bus.Subscribe<LoadMapMessage>(OnMapLoaded);
        }

        private void OnMapLoaded(LoadMapMessage msg)
        {
            Sprite[] all = Resources.LoadAll<Sprite>(TILES + msg.Map.tilesets[0].name);
            foreach (Sprite sp in all)
            {
                _tiles[sp.name] = sp;
            }

            foreach (TileLayer layer in msg.Map.layers)
            {
                for (int i = 0; i < layer.width; i++ )
                {
                    for(int j = 0; j < layer.height; j++)
                    {
                        GameObject tile = Instantiate(TilePrefab);
                        tile.transform.SetParent(gameObject.transform, true);
                        tile.transform.position = new Vector2(i * layer.width, j * layer.height);

                        string id = msg.Map.tilesets[0].name + "_" + layer.data[i * j];
                        tile.GetComponent<SpriteRenderer>().sprite = _tiles[id];
                    }
                }
            }
        }

    }
}
