using Assets.Scripts.entity;
using Assets.Scripts.entity.modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMessenger;

namespace Assets.Scripts.message.custom
{
    public class SpawnEnemiesMessage : BaseMessage
    {
        public IEnumerable<GameEntity> Enemies { get; set; }

        public SpawnEnemiesMessage(object sender, IEnumerable<GameEntity> enemies)
            : base(sender)
        {
            Enemies = enemies;
        }

    }

    public class RemoveEnemiesMessage : BaseMessage
    {
        public IEnumerable<GameEntity> Enemies { get; set; }

        public RemoveEnemiesMessage(object sender, IEnumerable<GameEntity> enemies)
            : base(sender)
        {
            Enemies = enemies;
        }
    }

    public class SpawnPlayerMessage : BaseMessage
    {
        public PlayerModule Player { get; set; }

        public SpawnPlayerMessage(object sender, PlayerModule player)
            : base(sender)
        {
            Player = player;
        }

    }

}
