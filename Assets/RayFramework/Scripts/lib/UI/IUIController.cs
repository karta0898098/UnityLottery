using System;
using System.Collections.Generic;

namespace RayFramework.UI
{
    public interface IUIController
    {
        DateTime LastUseTime { get; set; }

        bool NeverRelease { get; set; }

        bool AllowMulitActive { get; set; }

        void OnEnter();

        void OnUpdate();

        void OnLeave(Action OnAnimComplete);
    }
}
