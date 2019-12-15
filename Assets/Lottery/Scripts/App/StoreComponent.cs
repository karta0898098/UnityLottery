using System;
using UnityEngine;
using UnityRayFramework.Runtime;
using RayFramework.Event;

namespace App.Runtime
{
    public class StoreComponent : RayFrameworkComponent
    {
        public void InitState()
        {
            AppEntry.Blackboard.SetValue(Constant.DrawnCount, 10);
        }

        public void Dispatch(IStoreAction Action)
        {
            Action.Do();
            AppEntry.Event.Notfy(Action.Id, Action);
        }

        public void Subscribe(int id, EventHandler<GameEventArgs> handler)
        {
            AppEntry.Event.Subscribe(id,handler);
        }

        public void Unsubscribe(int id, EventHandler<GameEventArgs> handler)
        {
            AppEntry.Event.Unsubscribe(id, handler);
        }
    }
}