using UnityEngine;
using UnityEngine.UI;
using FancyScrollView;

namespace App.Runtime
{
    public class DrawnScrollItem : FancyScrollRectCell<DrawItemData>
    {
        [SerializeField] Text optionText = default;

        public override void UpdateContent(DrawItemData itemData)
        {
            optionText.text = itemData.Text;
        }

        protected override void UpdatePosition(float position, float viewportPosition)
        {
            transform.localPosition = new Vector2(position, viewportPosition);
        }
    }
}