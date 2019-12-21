using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using FancyScrollView;

namespace App.Runtime
{
    public class DrawnScrollRect : FancyScrollRect<DrawnItemData>
    {
        [SerializeField] float cellSize = 100f;
        [SerializeField] GameObject cellPrefab = default;

        protected override float CellSize => cellSize;
        protected override GameObject CellPrefab => cellPrefab;

        public void Start()
        {
            var dealy = new UnityRayFramework.Runtime.TimerAuto(0.5f, () =>
            {
                var items = Enumerable.Range(0, 35).Select(i => new DrawnItemData($"Option {i}")).ToArray();

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

        public void UpdateData(IList<DrawnItemData> items)
        {
            UpdateContents(items);
        }
    }

    public class DrawnItemData
    {
        public string Text { get; private set; }

        public DrawnItemData(string text)
        {
            Text = text;
        }
    }

    //public class DrawContext : FancyScrollRectContext
    //{
    //    public int SelectedIndex = -1;
    //}


}