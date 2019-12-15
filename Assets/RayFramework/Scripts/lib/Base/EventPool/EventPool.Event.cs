namespace RayFramework
{
    internal partial class EventPool<T>
    {
        private sealed class Event
        {
            public object Sender { get; }
            public T EventArgs { get; }

            public Event(object sender, T e)
            {
                Sender = sender;
                EventArgs = e;
            }
        }
    }
}
