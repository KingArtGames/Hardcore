using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.entity
{
    public class GameEntity
    {
        public GameType GameEntityType;

        public GameEntity(GameType type)
        {
            GameEntityType = type;
            _modules = new Dictionary<Type, BaseModule>();
        }

        #region modules
        private Dictionary<Type, BaseModule> _modules;

        public bool HasModule<T>() where T : BaseModule
        {
            return _modules.ContainsKey(typeof(T));
        }

        public void AddModule<T>(BaseModule module) where T : BaseModule
        {
            if (HasModule<T>())
                throw new ModuleAvailableException(typeof(T).ToString());
            _modules.Add(module.GetType(), module);
        }

        public T GetModule<T>() where T : BaseModule
        {
            if (HasModule<T>())
                return (T)_modules[typeof(T)];
            throw new ModuleNotFoundException(typeof(T).ToString());
        }
        #endregion

    }

    [Serializable]
    public class GameType
    {
        [SerializeField]
        public string Value;
        public GameType(string value)
        {
            Value = value;
        }
    }

    public enum MusicTypes
    {
        metal,
        classic,
        techno,
        neutral,
        metal_vibrating,
        classic_vibrating,
        techno_vibrating,
        musicBattery,
        blocked
    }

    public enum EntityTypes
    {
        player,
        enemy,
        tile
    }

}
