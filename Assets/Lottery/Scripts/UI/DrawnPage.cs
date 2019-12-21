using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.Runtime
{
    public class DrawnPage : MonoBehaviour
    {
        [SerializeField] Button BackButton;

        public void OnEnable()
        {
            BackButton.onClick.AddListener(Back);
        }

        public void OnDisable()
        {
            BackButton.onClick.RemoveListener(Back);
        }

        public void Back() 
        {
            AppEntry.Router.NavgationTo("Home");
        }
    }
}