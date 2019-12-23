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
        [SerializeField] Sprite DefaultImage;
        public void InitState()
        {
            var dict = new Dictionary<string, Sprite>();
            dict.Add("default", DefaultImage);
            AppEntry.Blackboard.SetValue(Constant.ImageCache, dict);

            var jsonDrawnList = AppEntry.Setting.GetString(Constant.EditorDrawnList);
            if (!string.IsNullOrEmpty(jsonDrawnList))
            {
                Debug.Log(jsonDrawnList);
                var drawnList = JsonConvert.DeserializeObject<List<EditorDrawnItemData>>(jsonDrawnList);
                AppEntry.Blackboard.SetValue(Constant.EditorDrawnList, drawnList);

                for (int i = 0; i < drawnList.Count; i++)
                {
#if !UNITY_EDITOR
                var dirPath = Application.persistentDataPath + "/cacheImages/";
#else
                var dirPath = Application.dataPath + "/cacheImages/";
#endif
                    if (!string.IsNullOrEmpty(drawnList[i].ImagePath) && drawnList[i].ImagePath!= "default")
                    {
                        var texture = NativeGallery.LoadImageAtPath(dirPath + drawnList[i].ImagePath + ".png");
                        if (texture != null)
                        {
                            var s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                            dict.Add(drawnList[i].ImagePath, s);
                        }
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
            AppEntry.Event.Subscribe(id, handler);
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