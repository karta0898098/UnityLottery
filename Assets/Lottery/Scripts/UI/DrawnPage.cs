using RayFramework.Event;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

namespace App.Runtime
{
    public class DrawnPage : MonoBehaviour
    {
        [SerializeField] Button BackButton;
        [SerializeField] Button ResetButton;
        [SerializeField] EditorDrawnScrollRect DrawnScrollRect;

        public void OnEnable()
        {
            AppEntry.Store.Subscribe(OnAddEditorDrawnEvent.EventId, OnUpdateData);
            BackButton.onClick.AddListener(Back);
            ResetButton.onClick.AddListener(ResetDrawn);
            StartCoroutine(DelayDispatch());
        }

        private void ResetDrawn()
        {
            var drawnList = AppEntry.Blackboard.GetValue(Constant.EditorDrawnList, new List<EditorDrawnItemData>());
            for (int i = 0; i < drawnList.Count; i++) 
            {
                drawnList[i].IsOn = false;
                drawnList[i].IsDrawed = false;
            }
            DrawnScrollRect.UpdateData(drawnList);
        }

        public void OnDisable()
        {
            ResetButton.onClick.RemoveListener(ResetDrawn);
            BackButton.onClick.RemoveListener(Back);
            AppEntry.Store.Unsubscribe(OnAddEditorDrawnEvent.EventId, OnUpdateData);
        }

        private void OnUpdateData(object sender, GameEventArgs ne)
        {
            var e = ne as OnAddEditorDrawnEvent;
            if (e != null) 
            {
                DrawnScrollRect.UpdateData(e.DrawnList);
            }
        }

        public void Back() 
        {
            AppEntry.Router.NavgationTo("Home");
        }

        IEnumerator DelayDispatch() 
        {
            yield return new WaitForEndOfFrame();
            AppEntry.Store.Dispatch(new OnAddEditorDrawnEvent(OnAddEditorDrawnEvent.Action.Init));
        }
    }
}