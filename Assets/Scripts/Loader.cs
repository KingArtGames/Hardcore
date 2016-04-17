using UnityEngine;
using System.Collections;
using Assets.Scripts.manager;
using TinyMessenger;
using Assets.Scripts.message.custom;
using Assets.Scripts.entity;
using System.Collections.Generic;
using Assets.Scripts.entity.modules;
using System.IO;
using Assets.Scripts.map;

public class Loader : MonoBehaviour 
{
    private IMessageBus _bus;
    private const string CHARACTERS_PATH = "/Resources/Characters";
    private const string PLAYER = "/player.json";
    private const string ENEMIES = "/enemies.json";
    private const string MAP = "/Resources/Map/Test/test_final_unity.json";

	// Use this for initialization
	void Start() 
    {
        _bus = Initialiser.Instance.GetService<IMessageBus>();
        _bus.Subscribe<LoadMapMessage>(OnLoadMap);
        //TestSavePlayer();
        //TestSaveEnemies();
        Load();
	}

    private void OnLoadMap(LoadMapMessage obj)
    {
        _bus.Publish(new MapLoadedMessage(this, GetTiles()));
    }

    private void TestSaveEnemies()
    {
        Enemies enemies = new Enemies();

        Connection enemy1 = new Connection();
        enemy1.Data = new Data()
        {
            CurrentMusicType = new GameType(MusicTypes.metal.ToString()),
            CurrentPosition = new Vector2(5, 5)
        };
        enemy1.Template = new Template()
        {
            MusicType = new GameType(MusicTypes.metal.ToString()),
            GameType = new GameType(EntityTypes.enemy.ToString()),
            SpawnPosition = new Vector2(5, 5)
        };

        Connection enemy2 = new Connection();
        enemy2.Data = new Data()
        {
            CurrentMusicType = new GameType(MusicTypes.metal.ToString()),
            CurrentPosition = new Vector2(5, 5)
        };
        enemy2.Template = new Template()
        {
            MusicType = new GameType(MusicTypes.metal.ToString()),
            GameType = new GameType(EntityTypes.enemy.ToString()),
            SpawnPosition = new Vector2(5, 5)
        };

        enemies.Connections[0] = enemy1;
        enemies.Connections[1] = enemy2;

        string jsonEnemies = JsonUtility.ToJson(enemies, true);
        File.WriteAllText(Application.dataPath + CHARACTERS_PATH + "/enemies.json", jsonEnemies);
    }

    private void TestSavePlayer()
    {
        Connection player = new Connection();
        player.Data = new Data() { CurrentMusicType = new GameType(MusicTypes.classic.ToString()), 
                                CurrentPosition = new Vector2(0, 0) };
        player.Template = new Template() { MusicType = new GameType(MusicTypes.classic.ToString()), 
                                        SpawnPosition = new Vector2(0, 0), 
                                        GameType = new GameType(EntityTypes.player.ToString()) };

        string jsonPlayer = JsonUtility.ToJson(player, true);
        File.WriteAllText(Application.dataPath + CHARACTERS_PATH + "/player.json", jsonPlayer);
    }

    private void Load()
    {
        _bus.Publish(new LoadEnemiesMessage(this, GetEnemies()));
        _bus.Publish(new LoadPlayerMessage(this, GetPlayer()));
    }

    private TileMap GetTiles()
    {
        TileMap map = JsonUtility.FromJson<TileMap>(File.ReadAllText(Application.dataPath + MAP));
        return map;
    }

    private GameEntity GetPlayer()
    {
        GameEntity ent = new GameEntity(new GameType("player"));
        Connection con = JsonUtility.FromJson<Connection>(File.ReadAllText(Application.dataPath + CHARACTERS_PATH + PLAYER));
        ent.AddModule<PlayerModule>(new PlayerModule(ent, _bus, con.Data, con.Template));
        return ent;
    }

    private IEnumerable<GameEntity> GetEnemies()
    {
        List<GameEntity> result = new List<GameEntity>();

        Enemies enemies = JsonUtility.FromJson<Enemies>(File.ReadAllText(Application.dataPath + CHARACTERS_PATH + ENEMIES));
        foreach (Connection con in enemies.Connections)
        {
            GameEntity enemy = new GameEntity(con.Template.GameType);
            enemy.AddModule<EnemyModule>(new EnemyModule(enemy, _bus, con.Data, con.Template));
            result.Add(enemy);
        }

        return result;
    }
	
}
