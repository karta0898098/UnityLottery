using System;
using RayFramework.Event;

namespace App.Runtime
{
    public class OnUIDrawEvent : IStoreAction
    {
        public override int Id => EventId;

        public static readonly int EventId = typeof(OnUIDrawEvent).GetHashCode();

        public int Count { get; private set; }

        public enum Action
        {
            Init = 0,
            OnClick,
        }

        public Action action { get; private set; }
        public OnUIDrawEvent(Action action)
        {
            this.action = action;
        }

        public override void Clear()
        {
            
        }

        public override void Do()
        {
            var drawnCount = AppEntry.Blackboard.GetValue(Constant.DrawnCount, 0);
            if (action == Action.OnClick)
            {              
                var calculation = drawnCount - 1;
                if (calculation >= 0)
                {
                    Count = calculation;
                    AppEntry.Blackboard.SetValue(Constant.DrawnCount, calculation);
                }
            }

            if(action == Action.Init)
            {
                Count = drawnCount;
            }            
        }
    }
}