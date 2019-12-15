﻿using UnityRayFramework.Runtime;

namespace App.Runtime
{
    public partial class AppEntry
    {
        public static BlackboardComponent Blackboard { get; private set; }

        public static EventComponent Event { get; private set; }

        public static UIComponent UI { get; private set; }

        public static TimerComponent Timer { get; private set; }

        private void InitBuiltInCompoment()
        {
            Blackboard = RayFrameworkEntry.GetComponent<BlackboardComponent>();
            Event = RayFrameworkEntry.GetComponent<EventComponent>();
            UI = RayFrameworkEntry.GetComponent<UIComponent>();
            Timer = RayFrameworkEntry.GetComponent<TimerComponent>();
        }
    }
}