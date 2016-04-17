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
            if (msg.Tile.BaseTemplate.MusicType.Value == _data.CurrentMusicType.Value)
                Debug.Log("same music type");
        }
    }
}
