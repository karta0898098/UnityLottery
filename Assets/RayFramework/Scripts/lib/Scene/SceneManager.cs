using System;
using RayFramework.Resource;

namespace RayFramework
{
    internal class SceneManager : RayCoreModule, ISceneManager
    {
        private ISceneLoaderHelper m_SceneHelper;

        public event Action OnLoadSuccessEvent;
        public event Action<float> OnLoadProgessEvent;
        public event Action OnLoadFailEvent;

        public event Action OnUnLoadSuccessEvent;
        public event Action<float> OnUnLoadProgessEvent;
        public event Action OnUnLoadFailEvent;

        public void SetResourceManager(ISceneLoaderHelper sceneHelper)
        {
            m_SceneHelper = sceneHelper;
        }

        public void LoadScene(string sceneAssetName)
        {
            m_SceneHelper.LoadScene(sceneAssetName, UpdateLoadSuccessEvent, UpdateLoadProgessEvent, UpdateLoadFailEvent);
        }

        public void LoadScene(string sceneAssetName, object userData)
        {
            m_SceneHelper.LoadScene(sceneAssetName, userData, UpdateLoadSuccessEvent, UpdateLoadProgessEvent, UpdateLoadFailEvent);
        }

        public void UnLoadScene(string sceneAssetName)
        {
            m_SceneHelper.UnLoadScene(sceneAssetName, UpdateUnLoadSuccessEvent,UpdateUnLoadProgessEvent,UpdateUnLoadFailEvent);
        }

        private void UpdateLoadSuccessEvent()
        {
            OnLoadSuccessEvent?.Invoke();
        }

        private void UpdateLoadProgessEvent(float progess)
        {
            OnLoadProgessEvent?.Invoke(progess);
        }

        private void UpdateLoadFailEvent()
        {
            OnLoadFailEvent?.Invoke();
        }

        private void UpdateUnLoadSuccessEvent()
        {
            OnUnLoadSuccessEvent?.Invoke();
        }

        private void UpdateUnLoadProgessEvent(float progess)
        {
            OnUnLoadProgessEvent?.Invoke(progess);
        }

        private void UpdateUnLoadFailEvent()
        {
            OnUnLoadFailEvent?.Invoke();
        }

        internal override void Update(float timeTick, float realTimeTick)
        {

        }

        internal override void Shoudown()
        {

        }
    }
}
