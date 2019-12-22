using UnityEngine;
using UnityEngine.UI;
using FancyScrollView;
using System.Collections.Generic;

namespace App.Runtime
{
    public class EditorDrawnScrollItem : FancyScrollRectCell<EditorDrawnItemData>
    {
        [SerializeField] Text optionText = default;
        [SerializeField] Button DecreaseBtn = default;
        [SerializeField] Button ImageBtn = default;
        [SerializeField] Image Image = default;

        private EditorDrawnItemData data;

        public override void UpdateContent(EditorDrawnItemData itemData)
        {
            data = itemData;
            optionText.text = itemData.Text;
            var imageCache = AppEntry.Blackboard.GetValue(Constant.ImageCache, new Dictionary<string, Sprite>());
            if (!string.IsNullOrEmpty(itemData.ImagePath))
            {
                var exist = imageCache.TryGetValue(itemData.ImagePath, out var s);
                if (exist)
                {
                    Image.sprite = s;
                }
            }
        }

        protected override void UpdatePosition(float position, float viewportPosition)
        {
            transform.localPosition = new Vector2(position, viewportPosition);
        }

        public void OnEnable()
        {
            DecreaseBtn.onClick.AddListener(OnDecrease);
            ImageBtn.onClick.AddListener(OnPickerImage);
        }

        public void OnDisable()
        {
            ImageBtn.onClick.RemoveListener(OnPickerImage);
            DecreaseBtn.onClick.RemoveListener(OnDecrease);
        }

        private void OnDecrease()
        {
            AppEntry.Store.Dispatch(new OnAddEditorDrawnEvent(OnAddEditorDrawnEvent.Action.Decrease, data));
        }

        private void OnPickerImage()
        {

            NativeGallery.GetImageFromGallery((path) =>
            {
                var texture = NativeGallery.LoadImageAtPath(path);
                if (texture != null)
                {
                    var s = Sprite.Create(texture, new Rect(0, 0, texture.width * 0.8f, texture.height * 0.8f), Vector2.zero);
                    data.ImagePath = System.Guid.NewGuid().ToString();
                    Image.sprite = s;
                    var imageCache = AppEntry.Blackboard.GetValue(Constant.ImageCache, new Dictionary<string, Sprite>());

                    if (imageCache.ContainsKey(data.ImagePath))
                    {
                        imageCache[data.ImagePath] = s;
                    }
                    else
                    {
                        imageCache.Add(data.ImagePath, s);
                    }
                    AppEntry.Blackboard.SetValue(Constant.ImageCache, imageCache);
                }
            });
            //NativeToolkit.PickImage();
            //NativeToolkit.OnImagePicked += OnImagePicked;
        }

        private void OnImagePicker(string path)
        {
            OnImagePicked(NativeGallery.LoadImageAtPath(path), path);
        }

        private void OnImagePicked(Texture2D texture, string path)
        {
            var s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            data.ImagePath = path;
            Image.sprite = s;
            var imageCache = AppEntry.Blackboard.GetValue(Constant.ImageCache, new Dictionary<string, Sprite>());

            if (imageCache.ContainsKey(path))
            {
                imageCache[path] = s;
            }
            else
            {
                imageCache.Add(path, s);
            }
            Debug.Log(path);
            AppEntry.Blackboard.SetValue(Constant.ImageCache, imageCache);
            //Destroy(texture, 10);
            //NativeToolkit.OnImagePicked -= OnImagePicked;
        }
    }
}