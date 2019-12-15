using System;

namespace RayFramework.Timer
{
    public abstract class TimerBase
    {
        public float NowTimer { get; protected set; }

        public float TargerTimer { get; protected set; }

        public bool Pause { get; protected set; }

        public bool Finish { get; protected set; }

        protected bool m_AtStart { get; set; } = true;

        public static event Action<ITimer> OnCreatEvent;

        public virtual void Create(ITimer timer)
        {
            OnCreatEvent?.Invoke(timer);
        }
    }
}
