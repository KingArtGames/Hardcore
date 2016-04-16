using UnityEngine;
using System.Collections;
using TinyMessenger;
using Assets.Scripts.manager;
using Assets.Scripts.message.custom;

public class ObjectsRender : MonoBehaviour 
{
    private IMessageBus _bus;

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
        
    }

}
