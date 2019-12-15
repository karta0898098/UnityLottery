using System;
using System.Collections.Generic;


namespace RayFramework.Resource
{
    public interface IResource
    {
        void SetHelper(IResourceHelper resourceHelper);

        void LoadAsset<T>(string asset, Action<T> OnSuccess) where T : class;
    }
}
