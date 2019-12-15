using System;
using RayFramework;
using RayFramework.Event;
using UnityEngine;

namespace UnityRayFramework.Runtime
{
    public sealed class EventComponent : RayFrameworkComponent
    {
        private IEventManager m_EventManager = null;

        public int EventHandlerCount => m_EventManager.EventHandlerCount;

        public int EventCount => m_EventManager.EventCount;

        protected override void Awake()
        {
            base.Awake();

            m_EventManager = RayFramework.RayFrameworkEntry.GetModule<IEventManager>();
            if (m_EventManager == null)
            {
                Debug.LogError("Event manager is invalid.");
            }
        }

        public int Count(int id)
        {
            return m_EventManager.Count(id);
        }

        public bool Check(int id, EventHandler<GameEventArgs> handler)
        {
            return m_EventManager.Check(id, handler);
        }

        public void Subscribe(int id, EventHandler<GameEventArgs> handler)
        {
            m_EventManager.Subscribe(id, handler);
        }

        public void Unsubscribe(int id, EventHandler<GameEventArgs> handler)
        {
            m_EventManager.Unsubscribe(id, handler);
        }

        public void SetDefaultHandler(EventHandler<GameEventArgs> handler)
        {
            m_EventManager.SetDefaultHandler(handler);
        }

        public void Notfy(object sender,GameEventArgs e)
        {
            m_EventManager.Notify(sender,e);
        }

        public void NotfyNow(object sender, GameEventArgs e)
        {
            m_EventManager.NotifyNow(sender, e);
        }
    }

}
