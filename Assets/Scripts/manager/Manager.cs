using Assets.Scripts.entity;
using Assets.Scripts.message.custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMessenger;

namespace Assets.Scripts.manager
{

    #region entity manager
    public interface IEntityManager : IInitialiser
    {

        IEnumerable<GameEntity> GetEnitiesOfType(GameType gameType);
    }

    public class EntityManager : BaseManager, IEntityManager
    {
        private List<GameEntity> _entities;

        public EntityManager(IMessageBus bus) 
            : base(bus)
        {
            _entities = new List<GameEntity>();
        }

        public override void RegisterSubscriptions()
        {
            _bus.Subscribe<LoadPlayerMessage>(OnPlayerLoaded);
            _bus.Subscribe<LoadEnemiesMessage>(OnEnemiesLoaded);
        }

        private void OnEnemiesLoaded(LoadEnemiesMessage obj)
        {
            _entities.AddRange(obj.Enemies);
        }

        private void OnPlayerLoaded(LoadPlayerMessage obj)
        {
            _entities.Add(obj.Player);
        }

        public IEnumerable<GameEntity> GetEnitiesOfType(GameType gameType)
        {
            return _entities.Where<GameEntity>(p => p.GameEntityType.Value == gameType.Value);
        }
    }
    #endregion


    #region effect manager
    public interface IEffectManager : IInitialiser
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
    public abstract class BaseManager
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
