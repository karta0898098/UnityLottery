using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using FancyScrollView;

namespace App.Runtime
{
    public class DrawnScrollRect : FancyScrollRect<DrawItemData>
    {
        [SerializeField] float cellSize = 100f;
        [SerializeField] GameObject cellPrefab = default;

        protected override float CellSize => cellSize;
        protected override GameObject CellPrefab => cellPrefab;

        public void Start()
        {
            var dealy = new UnityRayFramework.Runtime.TimerAuto(0.5f, () => 
            {
                var items = Enumerable.Range(0, 5).Select(i => new DrawItemData($"Option {i}")).ToArray();

                UpdateData(items);
            });
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

        public void UpdateData(IList<DrawItemData> items)
        {
            UpdateContents(items);
        }
    }

    public class DrawItemData
    {
        public string Text { get; private set; }

        public DrawItemData(string text)
        {
            Text = text;
        }
    }

    //public class DrawContext : FancyScrollRectContext
    //{
    //    public int SelectedIndex = -1;
    //}


}