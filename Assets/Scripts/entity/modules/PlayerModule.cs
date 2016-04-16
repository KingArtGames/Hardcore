using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMessenger;

namespace Assets.Scripts.entity.modules
{
    public class PlayerModule : BaseModule<PlayerData, PlayerTemplate>
    {

        public PlayerModule(GameEntity owner, IMessageBus bus, PlayerData data, PlayerTemplate template)
            : base(owner, bus, data, template)
        {
            _data = data;
            _template = template;
        }

    }

    public class PlayerData : Data
    {

    }

    public class PlayerTemplate : Template
    {

    }
}
