using System;
using System.Collections.Generic;


namespace RayFramework.Timer
{
    public interface ITimer
    {
        Action OnStartTask { get; set; }

        Action OnProcessTask { get; set; }

        Action OnCompleteTask { get; set; }

        bool IsFinish { get; }

        void UpdateTimer(float deltaTime);

        void ResumeTimer();

        void PasueTimer();

        void StopTimer(bool DoCompleteTask);
    }
}
