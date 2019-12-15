using System;
namespace RayFramework
{
    public abstract class RayCoreModule
    {
        internal virtual int Priority { get { return 0; } }
        internal abstract void Update(float timeTick, float realTimeTick);
        internal abstract void Shoudown();
    }
}
