using UnityEngine;
using UnityEngine.UI;
using MaterialUI;
using UnityRayFramework.Runtime;

namespace App.Runtime
{
    public class EditorDrawnAddController : MonoBehaviour
    {
        [SerializeField] Text DisplayText;
        [SerializeField] InputField InputText;
        [SerializeField] Button ComfirmButton;
        [SerializeField] Button CancelButton;
        [SerializeField] InputFieldConfig InputTextConfig;
        private void OnEnable()
        {
            InputText.text = string.Empty;
            DisplayText.text = string.Empty;
            InputTextConfig.OnDeselect(null);
            ComfirmButton.onClick.AddListener(OnClickComfirm);
            CancelButton.onClick.AddListener(OnClickCancel);
        }

        private void OnDisable()
        {
            CancelButton.onClick.RemoveListener(OnClickCancel);
            ComfirmButton.onClick.RemoveListener(OnClickComfirm);
        }

        public void OnClickComfirm()
        {
            //TODO Add List
            if (!string.IsNullOrEmpty(InputText.text))
            {
                AppEntry.Store.Dispatch(new OnAddEditorDrawnEvent(OnAddEditorDrawnEvent.Action.Add,
                    new EditorDrawnItemData(InputText.text)));
            }
            InputText.text = string.Empty;
            DisplayText.text = string.Empty;
            var delay = new TimerAuto(0.25f, () =>
            {
                gameObject.SetActive(false);
            });
        }

        public void OnClickCancel()
        {
            //InputText.text = string.Empty;
            //var delay = new TimerAuto(0.25f, () =>
            //{
            //    gameObject.SetActive(false);
            //});
        }
    }
}