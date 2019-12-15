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

        [SerializeField] GameObject AlertNoDrawn;

        public readonly string DrawnText = "簽桶剩餘 : {0}";
        private void OnEnable()
        {
            DrawButton.onClick.AddListener(OnClickDraw);
            AppEntry.Store.Subscribe(OnUIDrawEvent.EventId, UpdateDrawnCount);
            AppEntry.Store.Dispatch(new OnUIDrawEvent(OnUIDrawEvent.Action.Init));
        }


        private void OnDisable()
        {
            AppEntry.Store.Unsubscribe(OnUIDrawEvent.EventId, UpdateDrawnCount);
            DrawButton.onClick.RemoveListener(OnClickDraw);
        }

        private void OnClickDraw()
        {
            DrawButton.interactable = false;
            AppEntry.Store.Dispatch(new OnUIDrawEvent(OnUIDrawEvent.Action.OnClick));
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