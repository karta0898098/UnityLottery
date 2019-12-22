using System.Linq;
using System.Collections;
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
        [SerializeField] Text DrawedText;
        [SerializeField] Button DrawButton;
        [SerializeField] Button AddDrawnButton;
        [SerializeField] GameObject AlertNoDrawn;
        [SerializeField] Image Pic;

        private WaitForSeconds wait = new WaitForSeconds(0.125f);

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
            var drawnList = AppEntry.Blackboard.GetValue(Constant.EditorDrawnList, new List<EditorDrawnItemData>());
            StartCoroutine(DrawEffect(drawnList));
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
                    DrawButton.interactable = false;
                    if (e.action == OnUIDrawEvent.Action.OnClick)
                    {
                        //AlertNoDrawn.SetActive(true);
                    }
                }
                DrawnCountText.text = string.Format(DrawnText, e.Count);
            }
        }

        IEnumerator DrawEffect(List<EditorDrawnItemData> drawnList)
        {
            var imageCache = AppEntry.Blackboard.GetValue(Constant.ImageCache, new Dictionary<string, Sprite>());
            int j = 0;
            for (int i = 0; i < 20; i++)
            {
                if (j >= drawnList.Count)
                {
                    j = 0;
                }

                yield return wait;
                //if (!string.IsNullOrEmpty(drawnList[j].ImagePath))
                //{
                //    imageCache.TryGetValue(drawnList[j].ImagePath, out var s);
                //    if (s != null)
                //    {
                //        Pic.sprite = s;
                //    }
                //}

                DrawedText.text = drawnList[j].Text;
                j++;
            }
            yield return new WaitForEndOfFrame();
            var drawn = drawnList.Where((item) => item.IsOn == true && item.IsDrawed == false).ToList();
            if (drawn.Count > 0)
            {
                int rand = Random.Range(0, drawn.Count);
                var index = drawnList.IndexOf(drawn[rand]);
                drawnList[index].IsDrawed = true;
                drawn[rand].IsDrawed = true;
                DrawedText.text = drawn[rand].Text;

                if (!string.IsNullOrEmpty(drawnList[rand].ImagePath))
                {
                    imageCache.TryGetValue(drawnList[rand].ImagePath, out var s);
                    if (s != null)
                    {
                        Pic.sprite = s;
                    }
                }
            }

            AppEntry.Store.Dispatch(new OnUIDrawEvent(OnUIDrawEvent.Action.OnClick));
        }
    }
}