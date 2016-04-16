using UnityEngine;
using System.Collections;
using Assets.Scripts.manager;
using TinyMessenger;
using Assets.Scripts.message.custom;
using Assets.Scripts.entity;
using System.Collections.Generic;
using Assets.Scripts.entity.modules;

public class Loader : MonoBehaviour 
{
    private IMessageBus _bus;

	// Use this for initialization
	void Start () 
    {
        _bus = Initialiser.Instance.GetService<IMessageBus>();
        Load();
	}

    private void Load()
    {
        _bus.Publish(new SpawnEnemiesMessage(this, GetEnemies()));
        _bus.Publish(new SpawnPlayerMessage(this, GetPlayer()));
    }

    private PlayerModule GetPlayer()
    {
        GameEntity ent = new GameEntity(new GameEntityType("player"));
        PlayerModule pm = new PlayerModule(ent, _bus, new PlayerData(), new PlayerTemplate());
        return pm;
    }

    private IEnumerable<GameEntity> GetEnemies()
    {
        IEnumerable<GameEntity> result = new List<GameEntity>();

        return result;
    }
	
}
