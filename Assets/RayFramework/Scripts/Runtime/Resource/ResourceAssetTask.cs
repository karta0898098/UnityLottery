using System;
using System.Collections;
using UnityEngine;


namespace UnityRayFramework.Runtime
{
    public class ResourceAssetTask
    {
        public IEnumerator AsyncLoadAsset<T>(string assetPath, Action<T> OnSuccess) where T : class
        {
            var loader = Resources.LoadAsync(assetPath);
            while (!loader.isDone)
            {
                yield return null;
            }

            if (loader.asset != null)
            {
                OnSuccess?.Invoke(loader.asset as T);
            }
        }
    }
}
