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

        public BaseModule(GameEntity owner, IMessageBus bus, Data data, Template template)
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

    [Serializable]public class Data
    {
        [SerializeField]
        public Vector2 CurrentPosition;
        [SerializeField]
        public GameType CurrentMusicType;
    }
    [Serializable]public class Template
    {
        [SerializeField]
        public GameType GameType;
        [SerializeField]
        public GameType MusicType;
        [SerializeField]
        public Vector2 SpawnPosition;
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
