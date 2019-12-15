using System;
using UnityEngine;
using RayFramework;

namespace UnityRayFramework.Runtime
{
    public class SceneLoaderHelper : MonoBehaviour, ISceneLoaderHelper
    {
        public void LoadScene(string sceneAssetName, Action success, Action<float> progess, Action failed)
        {
            var task = new SceneLoaderTask(success, progess, failed);
            var taskIter = task.LoadScene(sceneAssetName);
            StartCoroutine(taskIter);
        }

        public void LoadScene(string sceneAssetName, object userData, Action success, Action<float> progess, Action failed)
        {
            var task = new SceneLoaderTask(success, progess, failed);
            var taskIter = task.LoadScene(sceneAssetName, (UnityEngine.SceneManagement.LoadSceneMode)userData);
            StartCoroutine(taskIter);
        }

        public void UnLoadScene(string sceneAssetName, Action success, Action<float> progess, Action failed)
        {
            var task = new SceneUnLoaderTask(success, progess, failed);
            var taskIter = task.UnLoadScene(sceneAssetName);
            StartCoroutine(taskIter);
        }
    }
}
