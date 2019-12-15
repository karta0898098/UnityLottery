using System;
using RayFramework.Timer;

namespace UnityRayFramework.Runtime
{
    public sealed class Timer : TimerDefaultHelper
    {
        public Timer(float target, Action OnComplete) : base(target, OnComplete)
        {

        }

        public Timer(float target, Action OnStart, Action OnComplete) : base(target, OnStart, OnComplete)
        {

        }

        public Timer(float target, Action OnStart, Action OnProcess, Action OnComplete) : base(target, OnStart, OnProcess, OnComplete)
        {

        }
    }
}
