using System;
using System.Linq;
using System.Collections.Generic;
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
            var drawnList = AppEntry.Blackboard.GetValue(Constant.EditorDrawnList, new List<EditorDrawnItemData>());
            if (action == Action.Init) 
            {
                Count = drawnList.Where((item) => item.IsOn == true && item.IsDrawed == false).Count();
            }

            //var drawnCount = AppEntry.Blackboard.GetValue(Constant.DrawnCount, 0);
            if (action == Action.OnClick)
            {
                Count = drawnList.Where((item) => item.IsOn == true && item.IsDrawed == false).Count();                                         
            }         
        }
    }
}