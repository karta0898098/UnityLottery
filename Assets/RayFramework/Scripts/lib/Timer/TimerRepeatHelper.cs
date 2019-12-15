using System;

namespace RayFramework.Timer
{
    public class TimerRepeatHelper : TimerBase, ITimer, IManualStartTimer, ITimerRepeat
    {
        public TimerRepeatHelper(float target, Action OnComplete)
        {
            TargerTimer = target;
            OnCompleteTask = OnComplete;
            Create(this);
        }

        public TimerRepeatHelper(float target, Action OnStart, Action OnComplete)
        {
            TargerTimer = target;
            OnStartTask = OnStart;
            OnCompleteTask = OnComplete;
            Create(this);
        }

        public TimerRepeatHelper(float target, Action OnStart, Action OnProcess, Action OnComplete)
        {
            TargerTimer = target;
            OnStartTask = OnStart;
            OnProcessTask = OnProcess;
            OnCompleteTask = OnComplete;
            Create(this);
        }
        protected bool m_Start { get; set; }

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

        public void StartTimer()
        {
            m_Start = true;
        }

        public void StopTimer(bool DoCompleteTask)
        {
            if (DoCompleteTask && OnCompleteTask != null)
            {
                OnCompleteTask();
            }
            Finish = true;
        }

        public void Repeat()
        {
            NowTimer = 0;
            m_Start = true;
            m_AtStart = true;
        }

        public void UpdateTimer(float deltaTime)
        {
            //開始計時
            if (!m_Start)
                return;

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
                Repeat();
            }
            else
            {
                NowTimer += deltaTime;
                OnProcessTask?.Invoke();
            }
        }
    }
}
