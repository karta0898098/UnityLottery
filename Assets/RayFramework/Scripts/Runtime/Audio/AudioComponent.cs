using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RayFramework.Audio;

namespace UnityRayFramework.Runtime
{
    public class AudioComponent : RayFrameworkComponent
    {
        private IAudioManager m_AudioManager;

        [Range(10.0f, 600.0f)]
        public float ReleaseInterval;

        [Range(10.0f, 600.0f)]
        public float ClearCacheInterval;

        public bool IsMasterMute => m_AudioManager.IsMasterMute;
        public bool IsBGMMute => m_AudioManager.IsBGMMute;
        public bool IsSFXMute => m_AudioManager.IsSFXMute;

        protected override void Awake()
        {
            base.Awake();

            var audioHelper = GetComponent<IAudioHelper>();
            m_AudioManager = RayFramework.RayFrameworkEntry.GetModule<IAudioManager>();
            m_AudioManager.SetAudioHelper(audioHelper);
            m_AudioManager.SetReleaeInterval(ReleaseInterval);
            m_AudioManager.SetClearInterval(ClearCacheInterval);
        }

        public void PlayBGM(string name, bool loop = true)
        {
            m_AudioManager.PlayBGM(name, loop);
        }

        public void PlaySFX(string name)
        {
            m_AudioManager.PlaySFX(name);
        }

        public void PlaySFX(string name, Transform target, bool setParent = false)
        {
            m_AudioManager.PlaySFX(name, target, setParent);
        }

        public void PlaySFX(string name, Vector3 pos)
        {
            m_AudioManager.PlayPosSFX(name, pos);
        }

        public void StopBGM(string name)
        {
            m_AudioManager.StopBGM(name);
        }

        public void MuteMaster()
        {
            m_AudioManager.MuteMaster();
        }

        public void MuteBGM()
        {
            m_AudioManager.MuteBGM();
        }

        public void MuteSFX()
        {
            m_AudioManager.MuteSFX();
        }

        public void ResumeMuteMaster()
        {
            m_AudioManager.ResumeMuteMaster();
        }

        public void ResumeMuteBGM()
        {
            m_AudioManager.ResumeMuteBGM();
        }

        public void ResumeMuteSFX()
        {
            m_AudioManager.ResumeMuteSFX();
        }

        public bool IsPlayingBGM(string name)
        {
            return m_AudioManager.IsPlayingBGM(name);
        }

        public bool IsPlayingSFX(string name)
        {
            return m_AudioManager.IsPlayingSFX(name);
        }

        public void SetMasterVolume(float value)
        {
            m_AudioManager.SetMasterVolume(value);
        }

        public void SetBGMVolume(float value)
        {
            m_AudioManager.SetBGMVolume(value);
        }

        public void SetSFXVolume(float value)
        {
            m_AudioManager.SetSFXVolume(value);
        }
    }
}
