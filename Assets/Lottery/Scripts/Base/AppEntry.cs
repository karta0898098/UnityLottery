using UnityEngine;


namespace App.Runtime
{
    public partial class AppEntry : MonoBehaviour
    {
        private void Start()
        {
            InitBuiltInCompoment();
            InitCustomComponent();
        }
    }
}