using System;
using UnityEngine;
using RayFramework;
using RayFramework.UI;

namespace UnityRayFramework.Runtime
{
    public sealed class UIComponent : RayFrameworkComponent
    {
        private IUIManager m_UIManager;

        [Range(10.0f, 600.0f)]
        public float ReleaseInterval;

        [Range(10.0f, 600.0f)]
        public float ClearCacheInterval;

        protected override void Awake()
        {
            base.Awake();

            var UIInstanceHelper = GetComponent<IUIInstanceHelper>();
            m_UIManager = RayFramework.RayFrameworkEntry.GetModule<IUIManager>();
            m_UIManager.SetHelper(UIInstanceHelper);
            m_UIManager.SetReleaeInterval(ReleaseInterval);
            m_UIManager.SetClearInterval(ClearCacheInterval);
        }

        public void Show<T>(string uiName, Action<T> OnSuccess = null) where T : UIControllerBase
        {
            m_UIManager.Show(uiName, OnSuccess);
        }

        public void Show(string uiName, Action<object> OnSuccess = null)
        {
            m_UIManager.Show(uiName, OnSuccess);
        }

        public T GetActiveUI<T>(string uiName) where T : UIControllerBase
        {
            return m_UIManager.GetActiveUI<T>(uiName);
        }

        public void Close(string uiName)
        {
            m_UIManager.Close(uiName);
        }
    }
}
