using UnityEngine;
using UnityEngine.UI;
using FancyScrollView;
using System.Collections.Generic;
using Crosstales.FB;
using System.IO;

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
                imageCache.TryGetValue(itemData.ImagePath, out var s);
                if (s != null)
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

#if UNITY_EDITOR
            ExtensionFilter[] extensions = new[] {
                new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
            };
            string path = FileBrowser.OpenSingleFile("Open File", string.Empty, extensions);
            OnImagePicker(path);
#else
            NativeGallery.GetImageFromGallery((path) =>
            {
                OnImagePicker(path);
            });

#endif
        }

        private void OnImagePicker(string path)
        {
            var texture = NativeGallery.LoadImageAtPath(path, -1, false);
            if (texture != null)
            {
                texture.Apply();
                var s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
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


                byte[] bytes = texture.EncodeToPNG();

#if !UNITY_EDITOR
                var dirPath = Application.persistentDataPath + "/cacheImages/";
#else
                var dirPath = Application.dataPath + "/cacheImages/";
#endif
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                File.WriteAllBytes(dirPath + data.ImagePath + ".png", bytes);
            }
        }
    }
}