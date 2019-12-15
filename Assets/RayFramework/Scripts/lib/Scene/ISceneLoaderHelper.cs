using System;

namespace RayFramework
{
    public interface ISceneLoaderHelper
    {
        void LoadScene(string sceneAssetName, Action success, Action<float> progess, Action failed);

        void LoadScene(string sceneAssetName,object userData ,Action success, Action<float> progess, Action failed);

        void UnLoadScene(string sceneAssetName, Action success, Action<float> progess, Action failed);
    }
}
