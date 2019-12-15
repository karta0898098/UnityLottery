using System;
using UnityEngine;

namespace UnityRayFramework.Runtime
{
    public class AudioSFX : MonoBehaviour
    {
        public DateTime LastUseTime;
        public AudioSource AudioSource;
        public float delayBack = 0.2f;

        public void Start()
        {
            if (AudioSource == null)
            {
                AudioSource = GetComponent<AudioSource>();
                AudioSource.playOnAwake = false;
            }
        }

        public void Play(object target, Action<AudioSFX> OnComplete)
        {
            var clip = target as AudioClip;
            var timer = new TimerAuto(clip.length + delayBack, () =>
            {
                OnComplete(this);
            });
            if (AudioSource == null)
            {
                AudioSource = GetComponent<AudioSource>();
                AudioSource.playOnAwake = false;
            }
            AudioSource.PlayOneShot(clip);
        }
    }
}
