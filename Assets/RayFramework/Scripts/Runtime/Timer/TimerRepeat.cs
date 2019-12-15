using System;
using RayFramework.Timer;

namespace UnityRayFramework.Runtime
{
    public sealed class TimerRepeat : TimerRepeatHelper
    {
        public TimerRepeat(float target, Action OnComplete) : base(target, OnComplete)
        {
        }

        public TimerRepeat(float target, Action OnStart, Action OnComplete) : base(target, OnStart, OnComplete)
        {
        }

        public TimerRepeat(float target, Action OnStart, Action OnProcess, Action OnComplete) : base(target, OnStart, OnProcess, OnComplete)
        {
        }
    }
}
