using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMessenger;

namespace Assets.Scripts.entity
{
    public abstract class BaseModule
    {
        protected GameEntity _owner;
        protected IMessageBus _messageBus;
        public BaseModule(GameEntity owner, IMessageBus bus)
        {
            _owner = owner;
            _messageBus = bus;
        }
    }

    public abstract class BaseModule<Data, Template> : BaseModule
    {
        protected Data _data;
        protected Template _template;

        public BaseModule(GameEntity owner, IMessageBus bus, Data data, Template template)
            : base(owner, bus)
        {
            _data = data;
            _template = template;
        }

        public Data BaseData
        {
            get { return _data; }
        }
        public Template BaseTemplate
        {
            get { return _template; }
        }
    }

    public class Data
    {

    }
    public class Template
    {

    }


    #region exceptions

    public class ModuleNotFoundException : Exception
    {
        public ModuleNotFoundException(string message)
            : base(message)
        {

        }
    }

    public class ModuleAvailableException : Exception
    {
        public ModuleAvailableException(string message)
            : base(message)
        {

        }
    }

    #endregion

}
