using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityRayFramework.Runtime;

namespace App.Runtime
{
    public class RouterComponent : RayFrameworkComponent
    {
        [SerializeField] GameObject HomePage;
        [SerializeField] GameObject DrawnPage;
        [SerializeField] GameObject EditorDrawnPage;

        [SerializeField] Image HomeIcon;
        [SerializeField] Image EditorDrawnIcon;

        [SerializeField] Text Title;

        public Color ActiveColor;
        public Color DeactiveColor;

        Dictionary<string, GameObject> Routers;
        Dictionary<string, Image> ActiveIcon;
        Dictionary<string, string> Titles;
        public string NowPage { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            Routers = new Dictionary<string, GameObject>();
            Routers.Add("Home", HomePage);
            Routers.Add("DrawnPage", DrawnPage);
            Routers.Add("EditorDrawn", EditorDrawnPage);

            ActiveIcon = new Dictionary<string, Image>();
            ActiveIcon.Add("Home", HomeIcon);
            ActiveIcon.Add("EditorDrawn", EditorDrawnIcon);

            Titles = new Dictionary<string, string>();
            Titles.Add("Home", "交換禮物抽獎");
            Titles.Add("EditorDrawn", "新增簽桶");
        }


        public void NavgationTo(string page)
        {     
            //Previous
            if (!string.IsNullOrEmpty(NowPage))
            {
                Routers.TryGetValue(NowPage, out var nowPageGO);
                if (nowPageGO != null)
                {
                    nowPageGO.SetActive(false);                    
                }
            }

            if (!string.IsNullOrEmpty(NowPage))
            {
                ActiveIcon.TryGetValue(NowPage, out var deactiveIcon);
                if (deactiveIcon != null)
                {
                    deactiveIcon.color = DeactiveColor;
                }
            }

            //Next Page
            Routers.TryGetValue(page, out var pageGO);
            if (pageGO != null)
            {
                pageGO.SetActive(true);
                NowPage = page;
            }

            ActiveIcon.TryGetValue(page, out var activeIcon);
            if (activeIcon != null) 
            {
                activeIcon.color = ActiveColor;
            }

            Titles.TryGetValue(page, out var title);
            if (title != null) 
            {
                Title.text = title;
            }
        }
    }
}
