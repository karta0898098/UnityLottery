using UnityEngine;

namespace UnityRayFramework.Runtime
{
    public abstract class RayFrameworkComponent : MonoBehaviour
    {
        protected virtual void Awake()
        {
            RayFrameworkEntry.RegisterComponent(this);
        }
    }
}
