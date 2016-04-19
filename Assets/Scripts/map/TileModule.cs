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
            return BaseData.CurrentMusicType.Value == MusicTypes.metal.ToString()
                || BaseData.CurrentMusicType.Value == MusicTypes.classic.ToString()
                || BaseData.CurrentMusicType.Value == MusicTypes.techno.ToString()
                || IsObstacle();
        }

        public bool IsBlocked()
        {
            return BaseData.CurrentMusicType.Value == MusicTypes.blocked.ToString()
                || IsObstacle();
        }

        public bool IsObstacle()
        {
            return BaseData.CurrentMusicType.Value == MusicTypes.classic_vibrating.ToString()
                || BaseData.CurrentMusicType.Value == MusicTypes.metal_vibrating.ToString()
                || BaseData.CurrentMusicType.Value == MusicTypes.techno_vibrating.ToString();
        }
    }
}
