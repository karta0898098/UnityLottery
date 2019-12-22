using UnityEngine;
using UnityEngine.UI;

namespace App.Runtime
{
    public class EditorDrawnListController : MonoBehaviour
    {
        [SerializeField] Button FloatAddButton = default;
        [SerializeField] GameObject UIAddPanel = default;

        public void Start()
        {
            AppEntry.Store.Dispatch(new OnAddEditorDrawnEvent(OnAddEditorDrawnEvent.Action.Init));
        }

        private void OnEnable()
        {
            FloatAddButton.onClick.AddListener(OnClickAddFloatButton);
        }

        private void OnDisable()
        {
            FloatAddButton.onClick.RemoveListener(OnClickAddFloatButton);
            UIAddPanel.SetActive(false);
        }

        public void OnClickAddFloatButton() 
        {
            UIAddPanel.SetActive(true);
        }
    }
}