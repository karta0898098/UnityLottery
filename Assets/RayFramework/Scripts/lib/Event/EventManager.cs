using System;

namespace RayFramework.Event
{
    internal sealed class EventManager:RayCoreModule,IEventManager
    {
        private readonly EventPool<GameEventArgs> m_EventPool;

        public EventManager()
        {
            m_EventPool = new EventPool<GameEventArgs>(EventPoolMode.AllowNoHandler | EventPoolMode.AllowMultiHandler);
        }

        public int EventHandlerCount => m_EventPool.EventHandlerCount;

        public int EventCount => m_EventPool.EventCount;

        internal override int Priority => 100;

        internal override void Update(float timeTick, float realTimeTick)
        {
            m_EventPool.Update(timeTick,realTimeTick);
        }

        internal override void Shoudown()
        {
            m_EventPool.Shutdown();
        }

        public int Count(int id)
        {
            return m_EventPool.Count(id);
        }

        public bool Check(int id, EventHandler<GameEventArgs> handler)
        {
            return m_EventPool.Check(id, handler);
        }

        public void Subscribe(int id, EventHandler<GameEventArgs> handler)
        {
            m_EventPool.Subscribe(id, handler);
        }

        public void Unsubscribe(int id, EventHandler<GameEventArgs> handler)
        {
            m_EventPool.Unsubscribe(id, handler);
        }

        public void SetDefaultHandler(EventHandler<GameEventArgs> handler)
        {
            m_EventPool.SetDefaultHandler(handler);
        }

        public void Notify(object sender,GameEventArgs e)
        {
            m_EventPool.Notify(sender, e);
        }

        public void NotifyNow(object sender, GameEventArgs e)
        {
            m_EventPool.NotifyNow(sender, e);
        }
    }
}
