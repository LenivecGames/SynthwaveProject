using System;
using System.Collections.Generic;

namespace Messaging
{
    public sealed class MessageBus : IEventAggregator
    {
        private Dictionary<Type, List<Object>> _Subscribers = new Dictionary<Type, List<Object>>();

        public void Subscribe<T>(Action<T> handler) where T : IMessage
        {
            if (_Subscribers.ContainsKey(typeof(T)))
            {
                List<Object> handlers = _Subscribers[typeof(T)];
                handlers.Add(handler);
            }
            else
            {
                List<Object> handlers = new List<Object>();
                handlers.Add(handler);
                _Subscribers[typeof(T)] = handlers;
            }
        }

        public void Unsubscribe<T>(Action<T> handler) where T : IMessage
        {
            if (_Subscribers.ContainsKey(typeof(T)))
            {
                List<Object> handlers = _Subscribers[typeof(T)];
                handlers.Remove(handler);

                if (handlers.Count == 0)
                {
                    _Subscribers.Remove(typeof(T));
                }
            }
        }

        public void Publish<T>(T message) where T : IMessage
        {
            if (_Subscribers.ContainsKey(typeof(T)))
            {
                List<Object> handlers = _Subscribers[typeof(T)];
                foreach (Action<T> handler in handlers.ToArray())
                {
                    handler.Invoke(message);
                }
            }
        }
    }
}