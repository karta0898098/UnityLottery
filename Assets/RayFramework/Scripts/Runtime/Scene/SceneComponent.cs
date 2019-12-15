using System;
using System.Collections.Generic;
using RayFramework;
using UnityEngine.SceneManagement;

namespace UnityRayFramework.Runtime
{
    public class SceneComponent : RayFrameworkComponent
    {
        ISceneManager m_SceneManager = null;
        ISceneLoaderHelper m_SceneHelper = null;

        public event Action OnLoadSuccessEvent;

        public event Action<float> OnLoadProgessEvent;

        public event Action OnLoadFailEvent;

        public event Action OnUnLoadSuccessEvent;

        public event Action<float> OnUnLoadProgessEvent;

        public event Action OnUnLoadFailEvent;

        public List<Scene> LoadedScenes = new List<Scene>();

        public void Start()
        {
            m_SceneManager = RayFramework.RayFrameworkEntry.GetModule<ISceneManager>();
            m_SceneHelper = GetComponent<ISceneLoaderHelper>();
            m_SceneManager.SetResourceManager(m_SceneHelper);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            UnityEngine.SceneManagement.SceneManager.sceneUnloaded += OnSceneUnLoaded;
            m_SceneManager.OnLoadSuccessEvent += NotifyLoadSuccessEvent;
            m_SceneManager.OnLoadProgessEvent += NotifyLoadProgressEvent;
            m_SceneManager.OnLoadFailEvent += NotifyLoadFailedEvent;
            m_SceneManager.OnUnLoadSuccessEvent += NotifyUnLoadSuccessEvent;
            m_SceneManager.OnUnLoadProgessEvent += NotifyUnLoadProgressEvent;
            m_SceneManager.OnUnLoadFailEvent += NotifyUnLoadFailedEvent;
        }

        public void OnDestroy()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
            UnityEngine.SceneManagement.SceneManager.sceneUnloaded -= OnSceneUnLoaded;
            m_SceneManager.OnLoadSuccessEvent -= NotifyLoadSuccessEvent;
            m_SceneManager.OnLoadProgessEvent -= NotifyLoadProgressEvent;
            m_SceneManager.OnLoadFailEvent -= NotifyLoadFailedEvent;
            m_SceneManager.OnUnLoadSuccessEvent -= NotifyUnLoadSuccessEvent;
            m_SceneManager.OnUnLoadProgessEvent -= NotifyUnLoadProgressEvent;
            m_SceneManager.OnUnLoadFailEvent -= NotifyUnLoadFailedEvent;
        }

        public Scene[] GetActiveLoadedScene()
        {
            return LoadedScenes.ToArray();
        }

        public void LoadScene(string sceneAssetName)
        {
            m_SceneManager.LoadScene(sceneAssetName);
        }

        public void LoadAddScene(string sceneAssetName)
        {
            m_SceneManager.LoadScene(sceneAssetName, LoadSceneMode.Additive);
        }

        public void UnLoadScene(string sceneAssetName)
        {
            m_SceneManager.UnLoadScene(sceneAssetName);
        }

        private void NotifyLoadSuccessEvent()
        {
            OnLoadSuccessEvent?.Invoke();
        }

        private void NotifyLoadProgressEvent(float progress)
        {
            OnLoadProgessEvent?.Invoke(progress);
        }

        private void NotifyLoadFailedEvent()
        {
            OnLoadFailEvent?.Invoke();
        }

        private void NotifyUnLoadSuccessEvent()
        {
            OnUnLoadSuccessEvent?.Invoke();
        }

        private void NotifyUnLoadProgressEvent(float progress)
        {
            OnUnLoadProgessEvent?.Invoke(progress);
        }

        private void NotifyUnLoadFailedEvent()
        {
            OnUnLoadFailEvent?.Invoke();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (!LoadedScenes.Contains(scene))
            {
                LoadedScenes.Add(scene);
                UnityEngine.Debug.LogFormat("Scene:{0} Loaded", scene);
            }
        }

        private void OnSceneUnLoaded(Scene scene)
        {
            if (LoadedScenes.Contains(scene))
            {
                LoadedScenes.Remove(scene);
                UnityEngine.Debug.LogFormat("Scene:{0} UnLoaded", scene);
            }
        }
    }
}