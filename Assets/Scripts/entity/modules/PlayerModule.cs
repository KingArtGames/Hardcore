using Assets.Scripts.map;
using Assets.Scripts.message.custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMessenger;
using UnityEngine;

namespace Assets.Scripts.entity.modules
{
    public class PlayerModule : BaseModule
    {

        public PlayerModule(GameEntity owner, IMessageBus bus, Data data, Template template)
            : base(owner, bus, data, template)
        {
            _data = data;
            _template = template;
        }

        public override void RegisterSubscriptions()
        {
            _bus.Subscribe<TileEnteredMessage>(OnTileEntered);
        }

        private void OnTileEntered(TileEnteredMessage msg)
        {
            if (msg.Tile.IsDestroyable() && msg.Tile.BaseTemplate.MusicType.Value != _data.CurrentMusicType.Value && !msg.Tile.IsObstacle())
            {
                ((TileComponent)msg.Sender).StartDropping(1, TileComponent.Timers[msg.Tile.BaseData.CurrentMusicType.Value]--);
            }
            else if (msg.Tile.IsObstacle() && msg.Tile.BaseTemplate.MusicType.Value.Contains(_data.CurrentMusicType.Value))
            {
                ((TileComponent)msg.Sender).StartDropping(2, 3);
            }

            if (msg.Tile.IsDestroyable() && msg.Tile.BaseData.CurrentMusicType.Value == _data.CurrentMusicType.Value)
            {
                TileComponent.Timers[msg.Tile.BaseData.CurrentMusicType.Value] = TileComponent.START_TIME;
            }
        }
    }
}
