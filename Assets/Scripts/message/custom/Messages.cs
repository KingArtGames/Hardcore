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

    public class MapLoadedMessage : BaseMessage
    {
        public TileMap Map;

        public MapLoadedMessage(object sender, TileMap map) 
            : base(sender)
        {
            Map = map;
        }

    }

    public class LoadMapMessage : BaseMessage
    {
        public LoadMapMessage(object sender) 
            : base(sender)
        {
        }
    }

    public class StartGameMessage : BaseMessage
    {

        public StartGameMessage(object sender)
            : base(sender)
        {

        }

    }

    public class LoadEnemiesMessage : BaseMessage
    {
        public IEnumerable<GameEntity> Enemies;

        public LoadEnemiesMessage(object sender, IEnumerable<GameEntity> enemies)
            : base(sender)
        {
            Enemies = enemies;
        }
    }

    public class LoadPlayerMessage : BaseMessage
    {
        public GameEntity Player;

        public LoadPlayerMessage(object sender, GameEntity player)
            : base(sender)
        {
            Player = player;
        }

    }

    public class PlayerChangedMusikTypeMessage : BaseMessage
    {
        public MusicTypes Type;
        public PlayerChangedMusikTypeMessage(object sender, MusicTypes type)
            : base(sender)
        {
            Type = type;
        }
    }

    public class TileEnteredMessage : BaseMessage
    {
        public TileModule Tile;
        public TileEnteredMessage(object sender, TileModule tile)
            : base(sender)
        {
            Tile = tile;
        }
    }


    public class GoalReachedMessage : BaseMessage
    {
        public GoalReachedMessage(object sender)
            : base(sender)
        {

        }
    }
    public class GameOverMessage : BaseMessage
    {
        public GameOverMessage (object sender)
            : base(sender)
        {
        }
    }
}
