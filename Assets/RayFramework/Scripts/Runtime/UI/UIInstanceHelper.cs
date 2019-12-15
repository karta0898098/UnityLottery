using System;
using RayFramework.UI;
using RayFramework.Resource;
using UnityEngine;

namespace UnityRayFramework.Runtime
{
    public class UIInstanceHelper : MonoBehaviour, IUIInstanceHelper
    {
        private IResource m_Resource;

        public RectTransform InstanceRoot;
        public string UiResourcePath;

        public void Start()
        {
            m_Resource = RayFramework.RayFrameworkEntry.GetModule<IResource>();
        }


        public void ResouceLoadUI<T>(string uiName, Action<T> OnSuccess) where T : class
        {
            var tempName = uiName;
            m_Resource.LoadAsset<GameObject>(string.Format(UiResourcePath + "/{0}", uiName), (asset) =>
               {
                   var go = Instantiate(asset, InstanceRoot);
                   var ui = go.GetComponent<UIControllerBase>();
                   ui.name = tempName;
                   ui.LastUseTime = DateTime.Now;
                   OnSuccess?.Invoke(ui as T);
               });
        }

        public void ActiveUI(object ui)
        {
            var castUI = ui as UIControllerBase;
            castUI.LastUseTime = DateTime.Now;
            castUI.gameObject.SetActive(true);
            castUI.transform.SetParent(InstanceRoot);
            Debug.LogFormat("Show UI: {0}", castUI.name);
        }

        public void CloseUI(object ui)
        {
            var castUI = ui as UIControllerBase;
            castUI.gameObject.SetActive(false);
            Debug.LogFormat("Close UI: {0}", castUI.name);
        }

        public void ReleaseUI(object ui)
        {
            var castUI = ui as UIControllerBase;
            Debug.LogFormat(" Release UI: {0}", castUI.name);
            Destroy(castUI.gameObject);
        }
    }
}
