using System;


namespace RayFramework.UI
{
    public interface IUIInstanceHelper
    {
        void ResouceLoadUI<T>(string uiName, Action<T> OnSuccess) where T : class;

        void CloseUI(object ui);

        void ActiveUI(object ui);

        void ReleaseUI(object ui);
    }
}
