using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FancyScrollView;
using RayFramework.Event;

namespace App.Runtime
{
    public class EditorDrawnScrollRect : FancyScrollRect<EditorDrawnItemData>
    {
        [SerializeField] float cellSize = 100f;
        [SerializeField] GameObject cellPrefab = default;

        protected override float CellSize => cellSize;
        protected override GameObject CellPrefab => cellPrefab;

        public void Start()
        {
            //var dealy = new UnityRayFramework.Runtime.TimerAuto(0.5f, () =>
            //{
            //    var items = Enumerable.Range(0, 35).Select(i => new EditorDrawnItemData($"Item {i}")).ToArray();

            //    UpdateData(items);
            //});            
        }

        public void OnEnable()
        {
            AppEntry.Store.Subscribe(OnAddEditorDrawnEvent.EventId, OnAddNewData);
        }

        public void OnDisable()
        {
            AppEntry.Store.Unsubscribe(OnAddEditorDrawnEvent.EventId, OnAddNewData);
        }

        public float PaddingTop
        {
            get => paddingHead;
            set
            {
                paddingHead = value;
                Refresh();
            }
        }

        public float PaddingBottom
        {
            get => paddingTail;
            set
            {
                paddingTail = value;
                Refresh();
            }
        }

        public float Spacing
        {
            get => spacing;
            set
            {
                spacing = value;
                Refresh();
            }
        }        

        public void UpdateData(IList<EditorDrawnItemData> items)
        {
            UpdateContents(items);
        }

        private void OnAddNewData(object sender, GameEventArgs ne) 
        {
            var e = ne as OnAddEditorDrawnEvent;
            if (e != null) 
            {
                UpdateContents(e.DrawnList);
            }
        }
    }

    public class EditorDrawnItemData 
    {
        public string ImagePath { get; set; } = "default";
        public bool IsOn { get; set; }
        public bool IsDrawed { get; set; }
        public string Text { get; private set; }

        public EditorDrawnItemData(string text)
        {
            Text = text;
        }
    }
}