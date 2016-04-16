using UnityEngine;
using System.Collections;
using TinyMessenger;
using Assets.Scripts.manager;
using Assets.Scripts.message.custom;
using Assets.Scripts.entity;
using Assets.Scripts.entity.modules;

public class ObjectsRender : MonoBehaviour 
{
    private IMessageBus _bus;

    public GameObject CharacterPrefab;
    public GameObject EnemyPrefab;

	void Start () 
    {
        _bus = Initialiser.Instance.GetService<IMessageBus>();
        RegisterSubscriptions();
	}

    private void RegisterSubscriptions()
    {
        _bus.Subscribe<SpawnEnemiesMessage>(OnSpawnEnemies);
        _bus.Subscribe<SpawnPlayerMessage>(OnSpawnPlayer);
    }

    private void OnSpawnPlayer(SpawnPlayerMessage msg)
    {
        
    }

    private void OnSpawnEnemies(SpawnEnemiesMessage msg)
    {
        foreach (GameEntity ent in msg.Enemies)
        {
            EnemyModule enemy = ent.GetModule<EnemyModule>();
        }
    }

}
