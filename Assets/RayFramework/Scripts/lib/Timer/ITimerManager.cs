using System;

namespace RayFramework.Timer
{
    public interface ITimerManager
    {
        void RegisterTimer(ITimer timer);

        void UnRegisterTimer(ITimer timer);

        void PauseAllTimer();

        void ResumeAllTimer();
    }
}
