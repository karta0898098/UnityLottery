using RayFramework.Event;

namespace App.Runtime
{
    public abstract class IStoreAction : GameEventArgs
    {
        public abstract void Do();
    }
}