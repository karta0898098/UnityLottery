using System;
using System.Collections.Generic;
using RayFramework.Event;

namespace App.Runtime
{
    public class OnAddEditorDrawnEvent : IStoreAction
    {
        public override int Id => EventId;

        public static readonly int EventId = typeof(OnAddEditorDrawnEvent).GetHashCode();

        public enum Action
        {
            Init = 0,
            Add,
        }

        public List<EditorDrawnItemData> DrawnList => AppEntry.Blackboard.GetValue(Constant.EditorDrawnList, new List<EditorDrawnItemData>());

        public Action action { get; private set; }

        private EditorDrawnItemData newData;
        public OnAddEditorDrawnEvent(Action action)
        {
            this.action = action;
        }

        public OnAddEditorDrawnEvent(Action action,EditorDrawnItemData newData)
        {
            this.action = action;
            this.newData = newData;
        }
        public override void Clear()
        {

        }

        public override void Do() 
        {
            var drawnList = AppEntry.Blackboard.GetValue(Constant.EditorDrawnList, new List<EditorDrawnItemData>());
            if (action == Action.Add) 
            {
                drawnList.Add(newData);
                AppEntry.Blackboard.SetValue(Constant.EditorDrawnList, drawnList);
            }
        }
    }
}