using UnityEngine;
using UnityEngine.UI;
using FancyScrollView;

namespace App.Runtime
{
    public class EditorDrawnScrollItem : FancyScrollRectCell<EditorDrawnItemData>
    {
        [SerializeField] Text optionText = default;
        [SerializeField] Button DecreaseBtn = default;

        private EditorDrawnItemData data;

        public override void UpdateContent(EditorDrawnItemData itemData)
        {
            data = itemData;
            optionText.text = itemData.Text;
        }

        protected override void UpdatePosition(float position, float viewportPosition)
        {
            transform.localPosition = new Vector2(position, viewportPosition);
        }

        public void OnEnable()
        {
            DecreaseBtn.onClick.AddListener(OnDecrease);
        }

        public void OnDisable()
        {
            DecreaseBtn.onClick.RemoveListener(OnDecrease);
        }

        private void OnDecrease() 
        {
            AppEntry.Store.Dispatch(new OnAddEditorDrawnEvent(OnAddEditorDrawnEvent.Action.Decrease, data));
        }
    }
}