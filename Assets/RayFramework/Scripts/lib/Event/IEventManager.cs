using System;
using System.Collections.Generic;

namespace RayFramework.Event
{
    public interface IEventManager
    {
        int EventHandlerCount { get; }

        int EventCount { get; }

        int Count(int id);

        bool Check(int id, EventHandler<GameEventArgs> handler);

        void Subscribe(int id, EventHandler<GameEventArgs> handler);

        void Unsubscribe(int id, EventHandler<GameEventArgs> handler);

        void SetDefaultHandler(EventHandler<GameEventArgs> handler);

        void Notify(object sender, GameEventArgs e);

        void NotifyNow(object sender, GameEventArgs e);
    }
}
