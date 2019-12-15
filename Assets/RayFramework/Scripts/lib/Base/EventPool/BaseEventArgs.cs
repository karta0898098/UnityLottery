using System;
namespace RayFramework
{
    public abstract class BaseEventArgs
    {
        public abstract int Id { get; }
        public abstract void Clear();
    }
}
