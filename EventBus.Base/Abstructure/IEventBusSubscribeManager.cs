using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Base.Abstructure
{
    public interface IEventBusSubscribeManager
    {

        bool IsEmty { get; }

        event EventHandler<string> OnEventRemoved;
        void AddSubscribe<T, TH>() where TH : IIntegrationEventHandler<T> where T : IntegrationEvent;
        void RemoveSubscribe<T, TH>() where TH : IIntegrationEventHandler<T> where T : IntegrationEvent;
        bool HasSubscribeForEvent<T>() where T : IntegrationEvent;
        bool HasSubscribeForEvent(string eventName);
        Type GetEventTypeByName(string eventName);
        void Clear();
        IEnumerable<SubscribeInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;
        IEnumerable<SubscribeInfo> GetHandlersForEvent(string eventName);
        string GetEventKey<T>();
    }
}
