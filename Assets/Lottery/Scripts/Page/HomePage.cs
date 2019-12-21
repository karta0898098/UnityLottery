using System;
using System.Collections.Generic;
using RayFramework.Event;
using UnityEngine;
using UnityEngine.UI;
using UnityRayFramework.Runtime;

namespace App.Runtime
{
    public class HomePage : MonoBehaviour
    {
        [SerializeField] Text DrawnCountText;
        [SerializeField] Button DrawButton;
        [SerializeField] Button AddDrawnButton;
        [SerializeField] GameObject AlertNoDrawn;

        public readonly string DrawnText = "簽桶剩餘 : {0}";
        private void OnEnable()
        {
            DrawButton.onClick.AddListener(OnClickDraw);
            AddDrawnButton.onClick.AddListener(OnClickAddDrawn);
            AppEntry.Store.Subscribe(OnUIDrawEvent.EventId, UpdateDrawnCount);
            AppEntry.Store.Dispatch(new OnUIDrawEvent(OnUIDrawEvent.Action.Init));
        }


        private void OnDisable()
        {
            AppEntry.Store.Unsubscribe(OnUIDrawEvent.EventId, UpdateDrawnCount);
            AddDrawnButton.onClick.RemoveListener(OnClickAddDrawn);
            DrawButton.onClick.RemoveListener(OnClickDraw);
        }

        private void OnClickDraw()
        {
            DrawButton.interactable = false;
            AppEntry.Store.Dispatch(new OnUIDrawEvent(OnUIDrawEvent.Action.OnClick));
        }

        private void OnClickAddDrawn() 
        {
            AppEntry.Router.NavgationTo("DrawnPage");
        }

        private void UpdateDrawnCount(object sender, GameEventArgs ne)
        {
            var e = ne as OnUIDrawEvent;
            if (e != null)
            {
                if (e.Count > 0)
                {
                    DrawButton.interactable = true;
                }
                else
                {   
                    if(e.action == OnUIDrawEvent.Action.OnClick)
                    {
                        AlertNoDrawn.SetActive(true);
                    }                    
                }
                DrawnCountText.text = string.Format(DrawnText, e.Count);
            }
        }
    }
}