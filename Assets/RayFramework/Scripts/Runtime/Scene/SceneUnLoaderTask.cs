using System;
using System.Collections;

namespace UnityRayFramework.Runtime
{
    public class SceneUnLoaderTask
    {
        readonly Action OnSuccess;
        readonly Action<float> OnProgess;
        readonly Action OnFailed;

        public SceneUnLoaderTask(Action success, Action<float> progess, Action failed)
        {
            OnSuccess = success;
            OnProgess = progess;
            OnFailed = failed;
        }

        public IEnumerator UnLoadScene(string sceneAssetName)
        {
            var task = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneAssetName);

            if (task == null)
            {
                OnFailed?.Invoke();
            }

            while (!task.isDone)
            {
                yield return null;
                OnProgess?.Invoke(task.progress);
            }

            OnSuccess?.Invoke();
        }
    }
}
