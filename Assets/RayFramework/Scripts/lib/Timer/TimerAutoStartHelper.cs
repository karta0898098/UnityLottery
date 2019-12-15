using System;

namespace RayFramework.Timer
{
    public class TimerAutoStartHelper : TimerBase, ITimer
    {
        public TimerAutoStartHelper(float target, Action OnComplete)
        {
            TargerTimer = target;
            OnCompleteTask = OnComplete;
            Create(this);
        }

        public TimerAutoStartHelper(float target, Action OnStart, Action OnComplete)
        {
            TargerTimer = target;
            OnStartTask = OnStart;
            OnCompleteTask = OnComplete;
            Create(this);
        }

        public TimerAutoStartHelper(float target, Action OnStart, Action OnProcess, Action OnComplete)
        {
            TargerTimer = target;
            OnStartTask = OnStart;
            OnProcessTask = OnProcess;
            OnCompleteTask = OnComplete;
            Create(this);
        }

        public Action OnStartTask { get; set; }
        public Action OnProcessTask { get; set; }
        public Action OnCompleteTask { get; set; }

        public bool IsFinish => Finish;

        public void PasueTimer()
        {
            Pause = true;
        }

        public void ResumeTimer()
        {
            Pause = false;
        }

        public void StopTimer(bool DoCompleteTask)
        {
            if (DoCompleteTask && OnCompleteTask != null)
            {
                OnCompleteTask();
            }
            Finish = true;
        }

        public void UpdateTimer(float deltaTime)
        {
            //是否暫停
            if (Pause)
                return;

            //剛開始的任務
            //只執行一次
            if (m_AtStart)
            {
                m_AtStart = false;
                OnStartTask?.Invoke();
            }

            if (NowTimer >= TargerTimer)
            {
                OnCompleteTask?.Invoke();
                Finish = true;
            }
            else
            {
                NowTimer += deltaTime;
                OnProcessTask?.Invoke();
            }
        }
    }
}
