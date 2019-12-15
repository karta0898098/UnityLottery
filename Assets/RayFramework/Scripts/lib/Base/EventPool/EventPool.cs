using System;
using System.Collections.Generic;
namespace RayFramework
{
    internal sealed partial class EventPool<T> where T : BaseEventArgs
    {
        private readonly Dictionary<int, LinkedList<EventHandler<T>>> m_EventHandlers;
        private readonly Queue<Event> m_Events;
        private readonly EventPoolMode m_EventPoolMode;
        private EventHandler<T> m_DefaultHandler;

        public EventPool(EventPoolMode mode)
        {
            m_EventHandlers = new Dictionary<int, LinkedList<EventHandler<T>>>();
            m_Events = new Queue<Event>();
            m_EventPoolMode = mode;
            m_DefaultHandler = null;
        }

        public int EventHandlerCount => m_EventHandlers.Count;

        public int EventCount => m_Events.Count;

        public void Update(float timeTick, float realTimeTick)
        {
            lock (m_Events)
            {
                while (m_Events.Count > 0)
                {
                    var e = m_Events.Dequeue();
                    HandleEvent(e.Sender, e.EventArgs);
                }
            }
        }

        public void Shutdown()
        {
            Clear();
            m_EventHandlers.Clear();
            m_DefaultHandler = null;
        }

        public void Clear()
        {
            lock (m_Events)
            {
                m_Events.Clear();
            }
        }

        public int Count(int id)
        {
            if (m_EventHandlers.TryGetValue(id, out LinkedList<EventHandler<T>> handlers))
            {
                return handlers.Count;
            }

            return 0;
        }

        public bool Check(int id, EventHandler<T> handler)
        {
            if (handler == null)
            {
                throw new RayFrameworkException("Event handler is invalid.");
            }

            if (!m_EventHandlers.TryGetValue(id, out LinkedList<EventHandler<T>> handlers))
            {
                return false;
            }

            return handlers.Contains(handler);
        }

        public void Subscribe(int id, EventHandler<T> handler)
        {
            if (handler == null)
            {
                throw new RayFrameworkException("Event handler is invalid.");
            }

            if (!m_EventHandlers.TryGetValue(id, out LinkedList<EventHandler<T>> handlers))
            {
                handlers = new LinkedList<EventHandler<T>>();
                handlers.AddLast(handler);
                m_EventHandlers.Add(id, handlers);
            }
            else if ((m_EventPoolMode & EventPoolMode.AllowMultiHandler) == 0)
            {
                throw new RayFrameworkException(string.Format("Event '{0}' not allow multi handler.", id.ToString()));
            }
            else if ((m_EventPoolMode & EventPoolMode.AllowDuplicateHandler) == 0 && Check(id, handler))
            {
                throw new RayFrameworkException(string.Format("Event '{0}' not allow duplicate handler.", id.ToString()));
            }
            else
            {
                handlers.AddLast(handler);
            }
        }

        public void Unsubscribe(int id, EventHandler<T> handler)
        {
            if (handler == null)
            {
                throw new RayFrameworkException("Event handler is invalid.");
            }

            if (!m_EventHandlers.TryGetValue(id, out LinkedList<EventHandler<T>> handlers))
            {
                throw new RayFrameworkException(string.Format("Event '{0}' not exists any handler.", id.ToString()));
            }

            if (!handlers.Remove(handler))
            {
                throw new RayFrameworkException(string.Format("Event '{0}' not exists specified handler.", id.ToString()));
            }
        }

        public void SetDefaultHandler(EventHandler<T> handler)
        {
            m_DefaultHandler = handler;
        }

        public void Notify(object sender, T e)
        {
            var enentNode = new Event(sender, e);
            lock (m_Events)
            {
                m_Events.Enqueue(enentNode);
            }
        }

        public void NotifyNow(object sender, T e)
        {
            HandleEvent(sender, e);
        }

        private void HandleEvent(object sender, T e)
        {
            var eventId = e.Id;
            var noHarndlerException = false;
            if (m_EventHandlers.TryGetValue(eventId, out LinkedList<EventHandler<T>> handlers) && handlers.Count > 0)
            {
                LinkedListNode<EventHandler<T>> current = handlers.First;
                while (current != null)
                {
                    LinkedListNode<EventHandler<T>> next = current.Next;
                    current.Value(sender, e);
                    current = next;
                }
            }
            else
            {
                if (m_DefaultHandler != null)
                {
                    m_DefaultHandler(sender, e);
                }
                else noHarndlerException |= (m_EventPoolMode & EventPoolMode.AllowNoHandler) == 0;
            }

            if (noHarndlerException)
            {
                throw new RayFrameworkException(string.Format("Event '{0}' not allow no handler.", eventId.ToString()));
            }
        }
    }
}
