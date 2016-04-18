using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMessenger;
using UnityEngine;

namespace Assets.Scripts.entity
{
    public abstract class BaseModule
    {
        protected Data _data;
        protected Template _template;
        protected IMessageBus _bus;

        public BaseModule(GameEntity owner, IMessageBus bus, Data data, Template template)
        {
            _data = data;
            _template = template;
            _bus = bus;
            RegisterSubscriptions();
        }

        public Data BaseData
        {
            get { return _data; }
        }
        public Template BaseTemplate
        {
            get { return _template; }
        }

        public abstract void RegisterSubscriptions();
    }

    [Serializable]public class Data
    {
        [SerializeField]
        public Vector3 CurrentPosition;
        [SerializeField]
        public GameType CurrentMusicType;
        [SerializeField]
        public float MusicHealthMeter = 0;
    }
    [Serializable]public class Template
    {
        [SerializeField]
        public GameType GameType;
        [SerializeField]
        public GameType MusicType;
    }

    [Serializable]public class Connection
    {
        [SerializeField]
        public Data Data;
        [SerializeField]
        public Template Template;
    }

    public class Enemies
    {
        public Connection[] Connections;
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
