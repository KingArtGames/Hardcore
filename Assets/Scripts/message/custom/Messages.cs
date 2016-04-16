using Assets.Scripts.entity;
using Assets.Scripts.entity.modules;
using Assets.Scripts.map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMessenger;

namespace Assets.Scripts.message.custom
{
    public class SpawnEnemiesMessage : BaseMessage
    {
        public IEnumerable<GameEntity> Enemies;

        public SpawnEnemiesMessage(object sender, IEnumerable<GameEntity> enemies)
            : base(sender)
        {
            Enemies = enemies;
        }

    }

    public class RemoveEnemiesMessage : BaseMessage
    {
        public IEnumerable<GameEntity> Enemies;

        public RemoveEnemiesMessage(object sender, IEnumerable<GameEntity> enemies)
            : base(sender)
        {
            Enemies = enemies;
        }
    }

    public class SpawnPlayerMessage : BaseMessage
    {
        public GameEntity Player;

        public SpawnPlayerMessage(object sender, GameEntity player)
            : base(sender)
        {
            Player = player;
        }

    }

    public class LoadMapMessage : BaseMessage
    {
        public TileMap Map;

        public LoadMapMessage(object sender, TileMap map) 
            : base(sender)
        {
            Map = map;
        }

    }

}
