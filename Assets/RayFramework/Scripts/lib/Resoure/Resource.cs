using System;
using System.Collections.Generic;


namespace RayFramework.Resource
{
    internal sealed class Resource : RayCoreModule, IResource
    {
        private IResourceHelper resourceHelper;

        public void SetHelper(IResourceHelper resourceHelper)
        {
            this.resourceHelper = resourceHelper;
        }

        public void LoadAsset<T>(string asset, Action<T> OnSuccess) where T : class
        {
            resourceHelper.LoadAsset(asset, OnSuccess);
        }

        internal override void Update(float timeTick, float realTimeTick)
        {

        }

        internal override void Shoudown()
        {

        }
    }
}
