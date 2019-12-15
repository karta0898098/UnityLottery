using System;
using RayFramework.Timer;

namespace UnityRayFramework.Runtime
{
    public sealed class TimerRepeatAuto : TimerRepeatAutoStartHelper
    {
        public TimerRepeatAuto(float target, Action OnComplete) : base(target, OnComplete)
        {
        }

        public TimerRepeatAuto(float target, Action OnStart, Action OnComplete) : base(target, OnStart, OnComplete)
        {
        }

        public TimerRepeatAuto(float target, Action OnStart, Action OnProcess, Action OnComplete) : base(target, OnStart, OnProcess, OnComplete)
        {
        }
    }
}
