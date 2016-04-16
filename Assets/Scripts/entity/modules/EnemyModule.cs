using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMessenger;

namespace Assets.Scripts.entity.modules
{
    public class EnemyModule : BaseModule<EnemyData, EnemyTemplate>
    {

        public EnemyModule(GameEntity owner, IMessageBus bus, EnemyData data, EnemyTemplate template)
            : base(owner, bus, data, template)
        {
            _data = data;
            _template = template;
        }

    }

    public class EnemyData : Data
    {

    }

    public class EnemyTemplate : Template
    {

    }
}
