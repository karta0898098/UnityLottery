using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using FancyScrollView;

namespace App.Runtime
{
    public class DrawnScrollItem : FancyScrollRectCell<EditorDrawnItemData>
    {
        [SerializeField] Text optionText = default;
        [SerializeField] Toggle toggle = default;

        private EditorDrawnItemData data;

        public override void UpdateContent(EditorDrawnItemData itemData)
        {
            //Debug.LogFormat("{0}:{1}", itemData.Text, itemData.IsOn);
            data = itemData;
            optionText.text = itemData.Text;            
            toggle.isOn = itemData.IsOn;

            if (toggle.interactable)
            {
                StartCoroutine(DelayDraw(itemData));
            }
            else 
            {
                toggle.interactable = !itemData.IsDrawed;
            }
        }

        protected override void UpdatePosition(float position, float viewportPosition)
        {
            transform.localPosition = new Vector2(position, viewportPosition);
        }

        public void OnEnable()
        {
            toggle.onValueChanged.AddListener(OnClickToggle);
        }

        public void OnDisable()
        {
            toggle.onValueChanged.RemoveListener(OnClickToggle);
        }

        private void OnClickToggle(bool sw) 
        {
            data.IsOn = sw;
        }

        IEnumerator DelayDraw(EditorDrawnItemData itemData) 
        {

            yield return new WaitForSeconds(0.5f);
            toggle.interactable = !itemData.IsDrawed;
        }
    }
}