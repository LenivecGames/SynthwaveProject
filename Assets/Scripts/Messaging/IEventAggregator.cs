using System;
using System.Collections.Generic;

namespace Messaging
{
    interface IEventAggregator
    {
        void Subscribe<T>(Action<T> handler) where T : IMessage;
        void Unsubscribe<T>(Action<T> handler) where T : IMessage;
        void Publish<T>(T message) where T : IMessage;
    }
}
