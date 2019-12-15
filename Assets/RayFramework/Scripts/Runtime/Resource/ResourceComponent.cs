using System;
using RayFramework;
using RayFramework.Resource;

namespace UnityRayFramework.Runtime
{
    public sealed class ResourceComponent : RayFrameworkComponent
    {
        private IResource m_Resource = null;

        protected override void Awake()
        {
            base.Awake();

            var resourceHelper = GetComponent<IResourceHelper>();
            m_Resource = RayFramework.RayFrameworkEntry.GetModule<IResource>();
            m_Resource.SetHelper(resourceHelper);
        }

        public void LoadAsset<T>(string asset, Action<T> OnSuccess) where T : class
        {
            m_Resource.LoadAsset(asset, OnSuccess);
        }
    }
}
