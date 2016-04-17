using Assets.Scripts.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMessenger;

namespace Assets.Scripts.map
{
    public class TileModule : BaseModule
    {

        public TileModule(GameEntity owner, IMessageBus bus, Data data, Template template)
            : base(owner, bus, data, template)
        {
            _data = data;
            _template = template;
        }

        public override void RegisterSubscriptions()
        {
            
        }
    }
}
