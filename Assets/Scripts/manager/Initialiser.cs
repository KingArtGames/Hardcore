using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMessenger;

namespace Assets.Scripts.manager
{
    public class Initialiser
    {
        private Dictionary<Type, IInitialiser> _services;

        public Initialiser()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            _services = new Dictionary<Type, IInitialiser>();

            IMessageBus bus = new MessageBus();

            _services.Add(typeof(IMessageBus), bus);
            _services.Add(typeof(IEntityManager), new EntityManager(bus));
            _services.Add(typeof(IEffectManager), new EffectManager(bus));

        }

        public T GetService<T>() where T : IInitialiser
        {
            foreach (var key in _services.Keys)
            {
                if (key.Equals(typeof(T)))
                    return (T)_services[key];
            }
            throw new Exception(typeof(T).ToString() + " not found");
        }

    }
}
