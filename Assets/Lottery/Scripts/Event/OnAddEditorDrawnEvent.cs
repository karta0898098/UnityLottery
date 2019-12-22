using System;
using System.Collections.Generic;
using RayFramework.Event;
using Newtonsoft.Json;

namespace App.Runtime
{
    public class OnAddEditorDrawnEvent : IStoreAction
    {
        public override int Id => EventId;

        public static readonly int EventId = typeof(OnAddEditorDrawnEvent).GetHashCode();

        public static int inited = -1;

        public enum Action
        {
            Init = 0,
            Add,
            Decrease,
        }

        public List<EditorDrawnItemData> DrawnList => AppEntry.Blackboard.GetValue(Constant.EditorDrawnList, new List<EditorDrawnItemData>());

        public Action action { get; private set; }

        private EditorDrawnItemData newData;
        public OnAddEditorDrawnEvent(Action action)
        {
            this.action = action;
        }

        public OnAddEditorDrawnEvent(Action action, EditorDrawnItemData newData)
        {
            this.action = action;
            this.newData = newData;
        }
        public override void Clear()
        {

        }

        public override void Do()
        {
            //if (action == Action.Init && inited == -1)
            //{
            //    var jsonDrawnList = AppEntry.Setting.GetString(Constant.EditorDrawnList);
            //    if (!string.IsNullOrEmpty(jsonDrawnList))
            //    {
            //        var drawnList = JsonConvert.DeserializeObject<List<EditorDrawnItemData>>(jsonDrawnList);
            //        AppEntry.Blackboard.SetValue(Constant.EditorDrawnList, drawnList);
            //        inited = 0;
            //    }
            //    return;
            //}

            if (action == Action.Add)
            {
                var drawnList = AppEntry.Blackboard.GetValue(Constant.EditorDrawnList, new List<EditorDrawnItemData>());
                drawnList.Add(newData);
                AppEntry.Blackboard.SetValue(Constant.EditorDrawnList, drawnList);
            }

            if (action == Action.Decrease)
            {
                var drawnList = AppEntry.Blackboard.GetValue(Constant.EditorDrawnList, new List<EditorDrawnItemData>());
                drawnList.Remove(newData);
                AppEntry.Blackboard.SetValue(Constant.EditorDrawnList, drawnList);
            }
        }
    }
}