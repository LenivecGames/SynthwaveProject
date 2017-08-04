using UnityEngine;
using System.Collections;
using System;

using Messaging;

namespace NeonSpace
{
    public static class EventManager
    {
        public static bool EnableDebugLog;
        private static IEventAggregator _EventAggregator = new MessageBus();

        public static void Subscribe<T>(Action<T> handler) where T : IMessage
        {
            _EventAggregator.Subscribe(handler);
            if (EnableDebugLog) Debug.Log(String.Format("Subscribe: {0} | Target: {1} | Method: {2}", handler, handler.Target, handler.Method));
        }

        public static void Unsubscribe<T>(Action<T> handler) where T : IMessage
        {
            _EventAggregator.Unsubscribe(handler);
            if (EnableDebugLog) Debug.Log(String.Format("Unsubscribe: {0} | Target: {1} | Method: {2}", handler, handler.Target, handler.Method));
        }

        public static void Publish<T>(T message) where T : IMessage
        {
            _EventAggregator.Publish(message);
            if (EnableDebugLog) Debug.Log("Publish: " + message);
        }

    }
}
