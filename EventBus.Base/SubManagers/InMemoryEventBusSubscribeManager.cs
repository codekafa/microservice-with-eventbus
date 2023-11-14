using EventBus.Base.Abstructure;
using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventBus.Base.SubManagers
{
    public class InMemoryEventBusSubscribeManager : IEventBusSubscribeManager
    {

        private readonly Dictionary<string, List<SubscribeInfo>> _handlers;

        private readonly List<Type> _eventTypes;

        public event EventHandler<string> OnEventRemoved;

        public Func<string, string> eventNameGetter;
        public InMemoryEventBusSubscribeManager(Func<string, string> eventNameGetter)
        {
            _handlers = new Dictionary<string, List<SubscribeInfo>>();
            _eventTypes = new List<Type>();
            this.eventNameGetter = eventNameGetter;
        }

        public bool IsEmty => !_handlers.Keys.Any();

        public void Clear() => _handlers.Clear();

        public void AddSubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();

            AddSubscribe(typeof(TH), eventName);

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }


        }

        private void AddSubscribe(Type handlerType, string eventName)
        {


            if (!HasSubscribeForEvent(eventName))
                _handlers.Add(eventName, new List<SubscribeInfo>());
            if (_handlers.ContainsKey(eventName) && _handlers[eventName].Any(s => s.HandlerType == handlerType))
                throw new Exception("Already registred!");

            _handlers[eventName].Add(SubscribeInfo.Typed(handlerType));
        }

        public string GetEventKey<T>()
        {
            string eventName = typeof(T).Name;
            return eventNameGetter(eventName);
        }

        public Type GetEventTypeByName(string eventName)
        {
            return _eventTypes.SingleOrDefault(e => e.Name == eventName);
        }

        public IEnumerable<SubscribeInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return GetHandlersForEvent(key);
        }

        public IEnumerable<SubscribeInfo> GetHandlersForEvent(string eventName) => _handlers[eventName];

        public bool HasSubscribeForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return HasSubscribeForEvent(key);
        }

        public bool HasSubscribeForEvent(string eventName) => _handlers.ContainsKey(eventName);

        public void RemoveSubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {

            var handlerToRemove = FindSubscribeToRemove<T, TH>();
            var eventName = GetEventKey<T>();
            RemoveHandler(eventName, handlerToRemove);

        }

        private void RemoveHandler(string eventName, SubscribeInfo subsToRemove)
        {
            if (subsToRemove != null)
            {

                _handlers[eventName].Remove(subsToRemove);

                if (!_handlers[eventName].Any())
                {
                    _handlers.Remove(eventName);

                    var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);

                    if (eventType != null)
                    {
                        _eventTypes.Remove(eventType);
                    }

                    RaiseOnEventRemoved(eventName);

                }

            }
        }

        private void RaiseOnEventRemoved(string eventName)
        {
            var handler = OnEventRemoved;
            handler?.Invoke(this, eventName);
        }

        private SubscribeInfo FindSubscribeToRemove<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();
            return FindSubscribeToRemove(eventName, typeof(TH));
        }

        private SubscribeInfo FindSubscribeToRemove(string eventName, Type handlerType)
        {
            if (!HasSubscribeForEvent(eventName))
            {
                return null;
            }

            return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);

        }
    }
}
