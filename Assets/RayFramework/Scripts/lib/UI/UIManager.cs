using System;
using System.Collections.Generic;

namespace RayFramework.UI
{
    internal sealed class UIManager : RayCoreModule, IUIManager
    {
        private float m_ReleaseInterval = 60;
        private float m_IntervalTimer;
        private float m_ClearInterval;

        private IUIInstanceHelper m_InstanceHelper;
        internal Dictionary<string, IUIController> m_Actives;
        internal Dictionary<string, IUIController> m_ObjectPool;

        public UIManager()
        {
            m_Actives = new Dictionary<string, IUIController>();
            m_ObjectPool = new Dictionary<string, IUIController>();
        }

        public void SetHelper(IUIInstanceHelper helper)
        {
            m_InstanceHelper = helper;
        }

        public void SetClearInterval(float interval)
        {
            m_ClearInterval = interval;
        }

        public void SetReleaeInterval(float interval)
        {
            m_ReleaseInterval = interval;
        }

        public void ClearCache()
        {
            var ReleaseQueue = new List<IUIController>();
            var ReleaseKey = new List<string>();
            foreach (var item in m_ObjectPool)
            {
                var intervalTime = DateTime.Now - item.Value.LastUseTime;
                if (intervalTime.TotalSeconds >= m_ReleaseInterval && !item.Value.NeverRelease)
                {
                    ReleaseKey.Add(item.Key);
                    ReleaseQueue.Add(item.Value);
                }
            }

            for (int i = 0; i < ReleaseKey.Count; i++)
            {
                m_ObjectPool.Remove(ReleaseKey[i]);
                m_InstanceHelper.ReleaseUI(ReleaseQueue[i]);
            }

            ReleaseQueue.Clear();
            ReleaseKey.Clear();
        }

        public void Close(string uiName)
        {
            if (m_Actives.ContainsKey(uiName))
            {
                var ui = m_Actives[uiName];
                ui.OnLeave(() =>
                {
                    m_Actives.Remove(uiName);
                    m_ObjectPool.Add(uiName, ui);
                    m_InstanceHelper.CloseUI(ui);
                });
            }
        }

        public void Show<T>(string uiName, Action<T> OnSuccess = null) where T : class
        {
            if (m_Actives.ContainsKey(uiName))
            {
                if (!m_Actives[uiName].AllowMulitActive)
                {
                    return;
                }
            }

            if (m_ObjectPool.ContainsKey(uiName))
            {
                var ui = m_ObjectPool[uiName];
                m_ObjectPool.Remove(uiName);
                m_Actives.Add(uiName, ui);
                m_InstanceHelper.ActiveUI(ui);
                ui.OnEnter();
                OnSuccess?.Invoke(ui as T);
            }
            else
            {
                m_InstanceHelper.ResouceLoadUI<T>(uiName, (ui) =>
                {
                    var castUI = ui as IUIController;
                    castUI.OnEnter();
                    m_Actives.Add(uiName, castUI);
                    OnSuccess?.Invoke(ui as T);
                });
            }
        }

        public void Show(string uiName, Action<object> OnSuccess)
        {
            if (m_Actives.ContainsKey(uiName))
            {
                if (!m_Actives[uiName].AllowMulitActive)
                {
                    return;
                }
            }

            if (m_ObjectPool.ContainsKey(uiName))
            {
                var ui = m_ObjectPool[uiName];
                m_ObjectPool.Remove(uiName);
                m_Actives.Add(uiName, ui);
                m_InstanceHelper.ActiveUI(ui);
                ui.OnEnter();
                OnSuccess?.Invoke(ui);
            }
            else
            {
                m_InstanceHelper.ResouceLoadUI<object>(uiName, (ui) =>
                {
                    var castUI = ui as IUIController;
                    castUI.OnEnter();
                    m_Actives.Add(uiName, castUI);
                    OnSuccess?.Invoke(ui);
                });
            }
        }

        public T GetActiveUI<T>(string uiName) where T:class
        {
            foreach (var item in m_Actives)
            {
                if (item.Key == uiName)
                {
                    return item.Value as T;
                }
            }
            return null;
        }

        internal override void Shoudown()
        {

        }

        internal override void Update(float timeTick, float realTimeTick)
        {
            if (m_IntervalTimer < m_ClearInterval)
            {
                m_IntervalTimer += timeTick;
            }
            else
            {
                m_IntervalTimer = 0;
                ClearCache();
            }

            foreach (var item in m_Actives)
            {
                item.Value.OnUpdate();
            }
        }
    }
}
