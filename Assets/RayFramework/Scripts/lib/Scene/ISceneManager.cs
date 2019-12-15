using System;
using RayFramework.Resource;

namespace RayFramework
{
    public interface ISceneManager
    {
        event Action OnLoadSuccessEvent;

        event Action<float> OnLoadProgessEvent;

        event Action OnLoadFailEvent;

        event Action OnUnLoadSuccessEvent;

        event Action<float> OnUnLoadProgessEvent;

        event Action OnUnLoadFailEvent;

        void SetResourceManager(ISceneLoaderHelper sceneHelper);

        void LoadScene(string sceneAssetName);

        void LoadScene(string sceneAssetName, object userData);

        void UnLoadScene(string sceneAssetName);
    }
}
