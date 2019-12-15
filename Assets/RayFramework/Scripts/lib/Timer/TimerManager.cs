using System;
using System.Collections.Generic;

namespace RayFramework.Timer
{
    internal sealed class TimerManager : RayCoreModule, ITimerManager
    {
        private static List<ITimer> m_LiveTimerList = new List<ITimer>();
        private ITimer m_TimerCache;
        private float m_LastRealTime;

        internal override int Priority => 10;

        public TimerManager()
        {
            m_LastRealTime = 0;
            TimerBase.OnCreatEvent += RegisterTimer;
        }

        ~TimerManager()
        {
            TimerBase.OnCreatEvent -= RegisterTimer;
            m_LiveTimerList.Clear();
        }

        public void PauseAllTimer()
        {
            foreach (var timer in m_LiveTimerList)
            {
                timer.PasueTimer();
            }
        }

        public void RegisterTimer(ITimer timer)
        {
            if (!m_LiveTimerList.Contains(timer))
            {
                m_LiveTimerList.Add(timer);
            }
        }

        public void ResumeAllTimer()
        {
            foreach (var timer in m_LiveTimerList)
            {
                timer.ResumeTimer();
            }
        }

        public void UnRegisterTimer(ITimer timer)
        {
            lock (m_LiveTimerList)
            {
                if (m_LiveTimerList.Contains(timer))
                {
                    m_LiveTimerList.Remove(timer);
                }
            }
        }

        internal override void Shoudown()
        {
            lock (m_LiveTimerList)
            {
                m_LiveTimerList.Clear();
            }
        }

        internal override void Update(float timeTick, float realTimeTick)
        {
            m_LastRealTime = realTimeTick - m_LastRealTime;
            if (m_LiveTimerList.Count > 0)
            {
                for (int i = 0; i < m_LiveTimerList.Count; i++)
                {
                    m_TimerCache = m_LiveTimerList[i];
                    m_TimerCache.UpdateTimer(m_LastRealTime);
                    if (m_TimerCache.IsFinish)
                    {
                        UnRegisterTimer(m_TimerCache);
                    }
                }
            }
            m_LastRealTime = realTimeTick;
        }
    }
}
