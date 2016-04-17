using Assets.Scripts.entity;
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
        private IEntityManager _entityManger;
        private const string TILES = "Tiles/";

        public GameObject TilePrefab;

        private Dictionary<string, Sprite> _tiles;

        public void Awake()
        {
            _tiles = new Dictionary<string, Sprite>();
            _bus = Initialiser.Instance.GetService<IMessageBus>();
            _entityManger = Initialiser.Instance.GetService<IEntityManager>();
            _bus.Subscribe<MapLoadedMessage>(OnMapLoaded);

            StartCoroutine(WaitForLoad());
        }

        private IEnumerator<object> WaitForLoad()
        {
            yield return new WaitForSeconds(1);
            _bus.Publish<LoadMapMessage>(new LoadMapMessage(this));
        }

        private void OnMapLoaded(MapLoadedMessage msg)
        {
            List<GameEntity> enemies = new List<GameEntity>();

            Sprite[] all = Resources.LoadAll<Sprite>(TILES + msg.Map.tilesets[0].name);
            foreach (Sprite sp in all)
            {
                _tiles[sp.name] = sp;
            }

            foreach (TileLayer layer in msg.Map.layers)
            {
                if (layer.name == "GameObjects")
                {
                    foreach (TiledObject to in layer.objects)
                    {
                        // add enemies based on the json file/object names
                    }
                }
                else if(layer.name == "Background")
                {
                    int row = 0;
                    int column = 0;
                    for (int i = 0; i < layer.data.Length; i++)
                    {
                        column = i % msg.Map.width;

                        if (layer.data[i] - 1 >= 0)
                        {
                            GameObject go = Instantiate(TilePrefab);
                            Vector3 pos = new Vector3(column * msg.Map.tilewidth * 0.01f, 0.1f, row * msg.Map.tileheight * 0.01f);
                            go.transform.position = pos;
                            go.transform.SetParent(transform, false);

                            string id = msg.Map.tilesets[0].name + "_" + (layer.data[i] - 1);
                            go.GetComponent<SpriteRenderer>().sprite = _tiles[id];

                            GameEntity tile = new GameEntity(new GameType(EntityTypes.Tile.ToString()));
                            // add tile module based on how tile is designed
                            tile.AddModule<TileModule>(GetTileModule(tile, new Data(), new Template()));
                            go.GetComponent<TileComponent>().Tile = tile;
                        }

                        if (i % (msg.Map.width) == 0 && i > 0) row++;
                    }
                }

            }
            _bus.Publish<LoadEnemiesMessage>(new LoadEnemiesMessage(this, enemies));
            _bus.Publish<LoadPlayerMessage>(new LoadPlayerMessage(this, _entityManger.GetEnitiesOfType(new GameType(EntityTypes.Player.ToString())).First()));
        }

        private TileModule GetTileModule(GameEntity tile, Data data, Template template)
        {
            TileModule result = new TileModule(tile, _bus, data, template);
            return result;
        }

    }
}
