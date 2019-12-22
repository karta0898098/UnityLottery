using System;
using UnityEngine;
using UnityRayFramework.Runtime;
using RayFramework.Event;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace App.Runtime
{
    public class StoreComponent : RayFrameworkComponent
    {
        public void InitState()
        {
            var jsonDrawnList = AppEntry.Setting.GetString(Constant.EditorDrawnList);
            if (!string.IsNullOrEmpty(jsonDrawnList))
            {
                Debug.Log(jsonDrawnList);
                var drawnList = JsonConvert.DeserializeObject<List<EditorDrawnItemData>>(jsonDrawnList);
                var dict = new Dictionary<string, Sprite>();
                AppEntry.Blackboard.SetValue(Constant.EditorDrawnList, drawnList);
                AppEntry.Blackboard.SetValue(Constant.ImageCache, dict);
                for (int i = 0; i < drawnList.Count; i++) 
                {
                    if (!string.IsNullOrEmpty(drawnList[i].ImagePath))
                    {
                        var texture = NativeToolkit.LoadImageFromFile(drawnList[i].ImagePath);
                        var s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                        dict.Add(drawnList[i].ImagePath, s);
                        Destroy(texture);
                    }
                }
            }
            //AppEntry.Blackboard.SetValue(Constant.DrawnCount, 0);
        }

        public void Dispatch(IStoreAction Action)
        {
            Action.Do();
            AppEntry.Event.Notfy(Action.Id, Action);
        }

        public void Subscribe(int id, EventHandler<GameEventArgs> handler)
        {
            AppEntry.Event.Subscribe(id,handler);
        }

        public void Unsubscribe(int id, EventHandler<GameEventArgs> handler)
        {
            AppEntry.Event.Unsubscribe(id, handler);
        }        

        private void OnApplicationQuit()
        {
            var drawnList = AppEntry.Blackboard.GetValue(Constant.EditorDrawnList, new List<EditorDrawnItemData>());
            var jsonDrawnList = JsonConvert.SerializeObject(drawnList);
            AppEntry.Setting.SetString(Constant.EditorDrawnList, jsonDrawnList);
        }
    }
}