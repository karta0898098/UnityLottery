
using System;
using System.Collections.Generic;

namespace RayFramework.Audio
{
    internal partial class AudioManager : RayCoreModule, IAudioManager
    {
        public bool IsMasterMute { get => m_IsBGMMute && m_IsSFXMute; }

        private bool m_IsBGMMute;
        public bool IsBGMMute { get => m_IsBGMMute; }

        private bool m_IsSFXMute;
        public bool IsSFXMute { get => m_IsSFXMute; }

        private IAudioHelper m_AudioHelper;

        private Dictionary<string, AudioInfo> m_ObjectPool;

        private float m_ReleaseInterval = 60;
        private float m_IntervalTimer;
        private float m_ClearInterval;

        public AudioManager()
        {
            m_ObjectPool = new Dictionary<string, AudioInfo>();
        }

        internal override void Update(float timeTick, float realTimeTick)
        {
            if (m_IntervalTimer < m_ClearInterval)
            {
                m_IntervalTimer += timeTick;
            }
            else
            {
                m_IntervalTimer = 0;
                ClearCache();
                m_AudioHelper.ClearCache();
            }
        }

        internal override void Shoudown()
        {

        }

        public void SetAudioHelper(IAudioHelper audioHelper)
        {
            m_AudioHelper = audioHelper;
        }

        public void SetReleaeInterval(float releaseInterval)
        {
            m_ReleaseInterval = releaseInterval;
        }

        public void SetClearInterval(float clearCacheInterval)
        {
            m_ClearInterval = clearCacheInterval;
        }

        public void PlayBGM(string name, bool loop = true)
        {
            if (m_ObjectPool.ContainsKey(name))
            {
                var audioInfo = m_ObjectPool[name];
                audioInfo.LastUseTime = DateTime.Now;
                m_AudioHelper.PlayBGM(audioInfo.audioAsset, loop);
            }
            else
            {
                var path = string.Format("{0}/{1}", "BGM", name);
                m_AudioHelper.ResouceAudio(path, (asset) =>
                {
                    var audioInfo = new AudioInfo
                    {
                        LastUseTime = DateTime.Now,
                        NerverRelease = false,
                        audioAsset = asset
                    };

                    m_ObjectPool.Add(name, audioInfo);
                    m_AudioHelper.PlayBGM(asset, loop);
                });
            }
        }

        public void PlaySFX(string name)
        {
            if (m_ObjectPool.ContainsKey(name))
            {
                var audioInfo = m_ObjectPool[name];
                audioInfo.LastUseTime = DateTime.Now;
                m_AudioHelper.PlaySFX(audioInfo.audioAsset);
            }
            else
            {
                var path = string.Format("{0}/{1}", "SFX", name);
                m_AudioHelper.ResouceAudio(path, (asset) =>
                {
                    var audioInfo = new AudioInfo
                    {
                        LastUseTime = DateTime.Now,
                        NerverRelease = false,
                        audioAsset = asset
                    };

                    m_ObjectPool.Add(name, audioInfo);
                    m_AudioHelper.PlaySFX(asset);
                });
            }
        }

        public void PlaySFX(string name, object parent, bool setParent = false)
        {
            if (m_ObjectPool.ContainsKey(name))
            {
                var audioInfo = m_ObjectPool[name];
                audioInfo.LastUseTime = DateTime.Now;
                m_AudioHelper.PlaySFX(audioInfo.audioAsset, parent, setParent);
            }
            else
            {
                var path = string.Format("{0}/{1}", "SFX", name);
                m_AudioHelper.ResouceAudio(path, (asset) =>
                {
                    var audioInfo = new AudioInfo
                    {
                        LastUseTime = DateTime.Now,
                        NerverRelease = false,
                        audioAsset = asset
                    };

                    m_ObjectPool.Add(name, audioInfo);
                    m_AudioHelper.PlaySFX(asset, parent, setParent);
                });
            }
        }

        public void PlayPosSFX(string name, object pos)
        {
            if (m_ObjectPool.ContainsKey(name))
            {
                var audioInfo = m_ObjectPool[name];
                audioInfo.LastUseTime = DateTime.Now;
                m_AudioHelper.PlayPosSFX(audioInfo.audioAsset, pos);
            }
            else
            {
                var path = string.Format("{0}/{1}", "SFX", name);
                m_AudioHelper.ResouceAudio(path, (asset) =>
                {
                    var audioInfo = new AudioInfo
                    {
                        LastUseTime = DateTime.Now,
                        NerverRelease = false,
                        audioAsset = asset
                    };

                    m_ObjectPool.Add(name, audioInfo);
                    m_AudioHelper.PlayPosSFX(asset, pos);
                });
            }
        }

        public void StopBGM(string name)
        {
            m_AudioHelper.StopBGM(name);
        }

        public void MuteMaster()
        {
            m_AudioHelper.MuteMaster();
        }

        public void MuteBGM()
        {
            m_IsBGMMute = true;
            m_AudioHelper.MuteBGM();
        }

        public void MuteSFX()
        {
            m_IsSFXMute = true;
            m_AudioHelper.MuteSFX();
        }

        public void ResumeMuteMaster()
        {
            m_AudioHelper.ResumeMuteMaster();
        }

        public void ResumeMuteBGM()
        {
            m_IsBGMMute = false;
            m_AudioHelper.ResumeMuteBGM();
        }

        public void ResumeMuteSFX()
        {
            m_IsSFXMute = false;
            m_AudioHelper.ReumeMuteSFX();
        }

        public void SetMasterVolume(float value)
        {
            m_AudioHelper.SetMasterVolume(value);
        }

        public void SetBGMVolume(float value)
        {
            m_AudioHelper.SetBGMVolume(value);
        }

        public void SetSFXVolume(float value)
        {
            m_AudioHelper.SetSFXVolume(value);
        }

        public bool IsPlayingBGM(string name)
        {
            return m_AudioHelper.IsPlayingBGM(name);
        }

        public bool IsPlayingSFX(string name)
        {
            return m_AudioHelper.IsPlayingSFX(name);
        }

        private void ClearCache()
        {
            var ReleaseQueue = new List<AudioInfo>();
            var ReleaseKey = new List<string>();

            foreach (var item in m_ObjectPool)
            {
                var intervalTime = DateTime.Now - item.Value.LastUseTime;
                if (intervalTime.TotalSeconds >= m_ReleaseInterval && !item.Value.NerverRelease)
                {
                    if (!IsPlayingSFX(item.Key) && !IsPlayingBGM(item.Key))
                    {
                        ReleaseKey.Add(item.Key);
                        ReleaseQueue.Add(item.Value);
                    }
                }
            }

            for (int i = 0; i < ReleaseKey.Count; i++)
            {
                m_ObjectPool.Remove(ReleaseKey[i]);
                m_AudioHelper.Release(ReleaseQueue[i].audioAsset);
            }

            ReleaseQueue.Clear();
            ReleaseKey.Clear();
        }
    }
}
