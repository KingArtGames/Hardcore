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

        public bool IsDestroyable()
        {
            return BaseTemplate.MusicType.Value == MusicTypes.metal.ToString()
                || BaseTemplate.MusicType.Value == MusicTypes.classic.ToString()
                || BaseTemplate.MusicType.Value == MusicTypes.techno.ToString();
        }

        public bool IsBlocked()
        {
            return BaseTemplate.MusicType.Value == MusicTypes.blocked.ToString();
        }

        public bool IsObstacle()
        {
            return BaseTemplate.MusicType.Value == MusicTypes.classic_vibrating.ToString()
                || BaseTemplate.MusicType.Value == MusicTypes.metal_vibrating.ToString()
                || BaseTemplate.MusicType.Value == MusicTypes.techno_vibrating.ToString();
        }
    }
}
