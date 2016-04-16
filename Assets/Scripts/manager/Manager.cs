using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMessenger;

namespace Assets.Scripts.manager
{

    #region entity manager
    public interface IEntityManager
    {

    }

    public class EntityManager : BaseManager, IEntityManager
    {
        public EntityManager(IMessageBus bus) 
            : base(bus)
        {
            
        }

        public override void RegisterSubscriptions()
        {
            
        }

    }
    #endregion


    #region effect manager
    public interface IEffectManager
    {

    }

    public class EffectManager : BaseManager, IEffectManager
    {

        public EffectManager(IMessageBus bus)
            : base(bus)
        {

        }

        public override void RegisterSubscriptions()
        {
            
        }
    }
    #endregion


    #region base manager
    public abstract class BaseManager : IInitialiser
    {
        protected IMessageBus _bus;

        public BaseManager(IMessageBus bus)
        {
            _bus = bus;
            RegisterSubscriptions();
        }

        public abstract void RegisterSubscriptions();

    }
    #endregion

}
