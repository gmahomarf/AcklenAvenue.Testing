﻿using System;
using System.Collections.Generic;

namespace AcklenAvenue.DomainEvents
{
    public class ActionEventDispatcher : IDispatcher
    {
        readonly IDictionary<Type, Delegate> _handlers = new Dictionary<Type, Delegate>();

        #region IDispatcher Members

        public void Dispatch<T>(T @event) where T : IEvent
        {
            if (!_handlers.ContainsKey(typeof(T))) throw new NoHandlerAvailable<T>();
            var handler = (Action<T>)_handlers[typeof(T)];
            handler.Invoke(@event);
        }

        #endregion

        public void Register<T>(Action<T> action)
        {
            _handlers.Add(typeof (T), action);
        }
    }
}