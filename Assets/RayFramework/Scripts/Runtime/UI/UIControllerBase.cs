using System;
using UnityEngine;
using RayFramework.UI;

namespace UnityRayFramework.Runtime
{
    public abstract class UIControllerBase : MonoBehaviour, IUIController
    {
        public DateTime LastUseTime { get; set; }

        [SerializeField]
        private bool m_NeverRelease;
        public bool NeverRelease { get => m_NeverRelease; set => m_NeverRelease = value; }

        [SerializeField]
        private bool m_AllowMulitActive;
        public bool AllowMulitActive { get => m_AllowMulitActive; set => m_AllowMulitActive = value; }

        public abstract void OnEnter();

        public abstract void OnUpdate();

        public abstract void OnLeave(Action OnAnimComplete);
    }
}
