using UnityEngine;
using RayFramework.Timer;


namespace UnityRayFramework.Runtime
{
    public sealed class BaseComponent : RayFrameworkComponent
    {
        [SerializeField]
        private bool m_RunInBackground = true;

        [SerializeField]
        private bool m_NeverSleep = true;

        private ITimerManager m_TimerManager = null;

        public bool RunInBackground
        {
            get => m_RunInBackground;
            set => Application.runInBackground = m_RunInBackground = value;
        }

        public bool NeverSleep
        {
            get => m_NeverSleep;
            set
            {
                m_NeverSleep = value;
                Screen.sleepTimeout = value ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            m_TimerManager = RayFramework.RayFrameworkEntry.GetModule<ITimerManager>();
            if (m_TimerManager == null)
            {
                Debug.LogError("Timer manager is invalid");
            }

            Application.runInBackground = m_RunInBackground;
            Screen.sleepTimeout = m_NeverSleep ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;
        }

        private void Update()
        {
            RayFramework.RayFrameworkEntry.Update(Time.deltaTime, Time.realtimeSinceStartup);
        }

        private void OnDestroy()
        {
            RayFramework.RayFrameworkEntry.Shutdown();
        }

        internal void Shutdown()
        {
            Destroy(transform.root);
        }

        public void PauseGame()
        {
            Time.timeScale = 0.0f;
            m_TimerManager.PauseAllTimer();
        }

        public void ResumeGame()
        {
            Time.timeScale = 1.0f;
            m_TimerManager.ResumeAllTimer();
        }
    }
}
