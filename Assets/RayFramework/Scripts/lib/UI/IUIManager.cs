using System;
using System.Collections.Generic;


namespace RayFramework.UI
{
    public interface IUIManager
    {
        void SetHelper(IUIInstanceHelper helper);

        void SetClearInterval(float interval);

        void SetReleaeInterval(float interval);

        void Show<T>(string uiName, Action<T> OnSuccess = null) where T : class;

        void Show(string uiName, Action<object> OnSuccess);

        T GetActiveUI<T>(string uiName) where T : class;

        void Close(string uiName);

        void ClearCache();
    }
}
