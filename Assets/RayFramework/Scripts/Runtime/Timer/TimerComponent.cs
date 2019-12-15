using RayFramework;
using RayFramework.Timer;
using System;
using UnityEngine;

namespace UnityRayFramework.Runtime
{
    public sealed class TimerComponent : RayFrameworkComponent
    {
        private ITimerManager m_TimerManager = null;

        protected override void Awake()
        {
            base.Awake();

            m_TimerManager = RayFramework.RayFrameworkEntry.GetModule<ITimerManager>();
            if (m_TimerManager == null)
            {
                Debug.LogError("Timer manager is invalid");
            }
        }

        public void RegisterTimer(ITimer timer)
        {
            m_TimerManager.RegisterTimer(timer);
        }

        public void PauseAllTimer()
        {
            m_TimerManager.PauseAllTimer();
        }

        public void ResumeAllTimer()
        {
            m_TimerManager.ResumeAllTimer();
        }

    }
}
