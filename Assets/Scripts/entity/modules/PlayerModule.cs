﻿using System;
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

    }
}
