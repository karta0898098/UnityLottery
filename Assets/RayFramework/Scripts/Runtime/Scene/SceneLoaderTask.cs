using System;
using System.Collections;

namespace UnityRayFramework.Runtime
{
    public class SceneLoaderTask
    {
        readonly Action OnSuccess;
        readonly Action<float> OnProgess;
        readonly Action OnFailed;

        public SceneLoaderTask(Action success, Action<float> progess, Action failed)
        {
            OnSuccess = success;
            OnProgess = progess;
            OnFailed = failed;
        }

        public IEnumerator LoadScene(string sceneAssetName)
        {
            var task = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneAssetName);

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

        public IEnumerator LoadScene(string sceneAssetName, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            var task = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneAssetName, mode);

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
