using Assets.Scripts.map;
using Assets.Scripts.message.custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMessenger;
using UnityEngine;

namespace Assets.Scripts.entity.modules
{
    public class PlayerModule : BaseModule, IDisposable
    {
        private TinyMessageSubscriptionToken _tileEnteredMessage;
        public PlayerModule(GameEntity owner, IMessageBus bus, Data data, Template template)
            : base(owner, bus, data, template)
        {
            _data = data;
            _template = template;
        }

        public override void RegisterSubscriptions()
        {
            _tileEnteredMessage = _bus.Subscribe<TileEnteredMessage>(OnTileEntered);
        }

        private void OnTileEntered(TileEnteredMessage msg)
        {
            if (msg.Tile.IsDestroyable() && msg.Tile.BaseData.CurrentMusicType.Value != _data.CurrentMusicType.Value && !msg.Tile.IsObstacle())
            {
                ((TileComponent)msg.Sender).StartDropping(1, TileComponent.Timers[msg.Tile.BaseData.CurrentMusicType.Value]--);
            }
            else if (msg.Tile.IsObstacle() && msg.Tile.BaseData.CurrentMusicType.Value.Contains(_data.CurrentMusicType.Value))
            {
                ((TileComponent)msg.Sender).StartDropping(2, 3);
            }

            if (msg.Tile.IsDestroyable() && msg.Tile.BaseData.CurrentMusicType.Value == _data.CurrentMusicType.Value)
            {
                TileComponent.Timers[msg.Tile.BaseData.CurrentMusicType.Value] = TileComponent.START_TIME;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                _bus.Unsubscribe<TileEnteredMessage>(_tileEnteredMessage);
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PlayerModule() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
