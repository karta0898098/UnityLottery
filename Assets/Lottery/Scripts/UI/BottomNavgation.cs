using UnityEngine;
using UnityEngine.UI;

namespace App.Runtime
{
    public class BottomNavgation : MonoBehaviour
    {
        [SerializeField] Button Home;
        [SerializeField] Button EditorDrawn;
        private void OnEnable()
        {
            Home.onClick.AddListener(OnClickHome);
            EditorDrawn.onClick.AddListener(OnClickEditorDrawn);
        }
        private void OnDisable()
        {
            EditorDrawn.onClick.RemoveListener(OnClickEditorDrawn);
            Home.onClick.RemoveListener(OnClickHome);
        }

        public void OnClickHome() 
        {
            AppEntry.Router.NavgationTo("Home");
        }

        public void OnClickEditorDrawn() 
        {
            AppEntry.Router.NavgationTo("EditorDrawn");
        }
    }
}