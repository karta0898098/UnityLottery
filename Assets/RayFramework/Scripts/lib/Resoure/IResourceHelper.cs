using System;


namespace RayFramework.Resource
{
    public interface IResourceHelper
    {
        void LoadAsset<T>(string asset, Action<T> OnSuccess) where T : class;
    }
}
