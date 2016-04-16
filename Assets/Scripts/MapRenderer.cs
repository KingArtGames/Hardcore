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
        private const string TILES = "";

        public GameObject TilePrefab;

        private Dictionary<int, Sprite> _tiles;

        public void Start()
        {
            _tiles = new Dictionary<int, Sprite>();
            _bus = Initialiser.Instance.GetService<IMessageBus>();

            _bus.Subscribe<LoadMapMessage>(OnMapLoaded);
        }

        private void OnMapLoaded(LoadMapMessage msg)
        {
            foreach (TileLayer layer in msg.Map.layers)
            {
                for (int i = 0; i < layer.width; i++ )
                {
                    for(int j = 0; j < layer.height; j++)
                    {
                        GameObject tile = Instantiate(TilePrefab);
                        tile.transform.SetParent(transform, true);
                        tile.transform.position = new Vector2(i * layer.width, j * layer.height);

                        int id = layer.data[i*j];
                        if (!_tiles.ContainsKey(id))
                        {
                            _tiles[id] = Resources.Load<Sprite>(TILES + msg.Map.tilesets[0].name + "_" + id);
                        }

                        tile.GetComponent<Image>().sprite = _tiles[id];
                    }
                }
            }
        }

    }
}
