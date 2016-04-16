using Assets.Scripts.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.map
{
    public class Tile
    {

        public Tile()
        {
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
}
