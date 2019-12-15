using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using RayFramework.Audio;
using RayFramework.Resource;
using RayFramework.Setting;

namespace UnityRayFramework.Runtime
{
    public class AudioHelper : MonoBehaviour, IAudioHelper
    {
        public AudioMixer AudioMixer;
        public AudioMixerGroup SfxGroup;

        private AudioSource[] m_BgmAudioSources;
        private AudioSource[] m_SfxAudioSources;

        private IResource m_Resource;
        private ISettingManager m_Setting;

        public readonly float MaxVolume = 1.0f;
        public readonly float MinVolume = 0.0f;

        private List<AudioSFX> m_ObjectPool;

        [Range(10, 600)]
        public float ReleaseInterval;

        public string AudioPath;

        private void Start()
        {
            m_Resource = RayFramework.RayFrameworkEntry.GetModule<IResource>();
            m_Setting = RayFramework.RayFrameworkEntry.GetModule<ISettingManager>();
            m_BgmAudioSources = GameObject.Find("BGM").GetComponents<AudioSource>();
            m_SfxAudioSources = GameObject.Find("SFX").GetComponents<AudioSource>();
            m_ObjectPool = new List<AudioSFX>();
        }

        public void ResouceAudio(string name, Action<object> OnComplete)
        {
            var path = string.IsNullOrEmpty(AudioPath) ? name : string.Format("{0}/{1}", AudioPath, name);
            m_Resource.LoadAsset<AudioClip>(path, OnComplete);
        }

        public void PlayBGM(object audioAsset, bool loop = true)
        {
            foreach (var audioSource in m_BgmAudioSources)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.volume = MaxVolume;
                    audioSource.loop = loop;
                    audioSource.clip = audioAsset as AudioClip;
                    audioSource.Play();
                }
            }
        }

        public void PlaySFX(object audioAsset)
        {
            foreach (var audioSource in m_SfxAudioSources)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.volume = MaxVolume;
                    audioSource.PlayOneShot(audioAsset as AudioClip);
                    break;
                }
            }
        }

        public void PlaySFX(object audioAsset, object target, bool setParent = false)
        {
            if (m_ObjectPool.Count > 0)
            {
                var lastIndex = m_ObjectPool.Count - 1;
                var tr = target as Transform;
                var reuse = m_ObjectPool[lastIndex];
                var parent = setParent ? tr : null;
                m_ObjectPool.RemoveAt(lastIndex);
                reuse.transform.SetParent(parent);
                reuse.transform.SetPositionAndRotation(tr.position, tr.rotation);
                reuse.gameObject.SetActive(true);
                reuse.Play(audioAsset, Recovery);
                reuse.LastUseTime = DateTime.Now;
            }
            else
            {
                var tr = target as Transform;
                var reuse = new GameObject("AudioSFX");
                var audioSourece = reuse.AddComponent<AudioSource>();
                var audioSFX = reuse.AddComponent<AudioSFX>();
                var parent = setParent ? tr : null;
                reuse.transform.SetParent(parent);
                reuse.transform.SetPositionAndRotation(tr.position, tr.rotation);
                reuse.gameObject.SetActive(true);
                audioSourece.outputAudioMixerGroup = SfxGroup;
                audioSFX.LastUseTime = DateTime.Now;
                audioSFX.Play(audioAsset, Recovery);
            }
        }

        public void PlayPosSFX(object audioAsset, object pos)
        {
            if (m_ObjectPool.Count > 0)
            {
                var lastIndex = m_ObjectPool.Count - 1;
                var newPos = (Vector3)pos;
                var reuse = m_ObjectPool[lastIndex];
                m_ObjectPool.RemoveAt(lastIndex);
                reuse.transform.SetParent(null);
                reuse.transform.SetPositionAndRotation(newPos, Quaternion.identity);
                reuse.gameObject.SetActive(true);
                reuse.Play(audioAsset, Recovery);
                reuse.LastUseTime = DateTime.Now;
            }
            else
            {
                var newPos = (Vector3)pos;
                var reuse = new GameObject("AudioSFX");
                var audioSourece = reuse.AddComponent<AudioSource>();
                var audioSFX = reuse.AddComponent<AudioSFX>();
                reuse.transform.SetPositionAndRotation(newPos, Quaternion.identity);
                reuse.gameObject.SetActive(true);
                audioSourece.outputAudioMixerGroup = SfxGroup;
                audioSFX.LastUseTime = DateTime.Now;
                audioSFX.Play(audioAsset, Recovery);
            }
        }

        public void StopBGM(string name)
        {
            foreach (var audioSource in m_BgmAudioSources)
            {
                if (audioSource.isPlaying)
                {
                    if (audioSource.clip.name == name)
                    {
                        audioSource.Stop();
                        audioSource.clip = null;
                    }
                }
            }
        }

        public void MuteMaster()
        {
            MuteBGM();
            MuteSFX();
        }

        public void MuteBGM()
        {
            foreach (var audioSource in m_BgmAudioSources)
            {
                audioSource.mute = true;
            }
        }

        public void MuteSFX()
        {
            foreach (var audioSource in m_SfxAudioSources)
            {
                audioSource.mute = true;
            }
        }

        public void ResumeMuteMaster()
        {
            ResumeMuteBGM();
            ReumeMuteSFX();
        }

        public void ResumeMuteBGM()
        {
            foreach (var audioSource in m_BgmAudioSources)
            {
                audioSource.mute = false;
            }
        }

        public void ReumeMuteSFX()
        {
            foreach (var audioSource in m_SfxAudioSources)
            {
                audioSource.mute = false;
            }
        }

        public void SetMasterVolume(float value)
        {
            SetVolume("Master", value);
        }

        public void SetBGMVolume(float value)
        {
            SetVolume("BGM", value);
        }

        public void SetSFXVolume(float value)
        {
            SetVolume("SFX", value);
        }

        private void SetVolume(string key, float value)
        {
            var percentage = 1;
            var percent = -80;
            var reverseVolume = percentage - Mathf.Clamp01(value);
            var process = percent * reverseVolume;
            AudioMixer.SetFloat(key, process);
        }

        public bool IsPlayingBGM(string name)
        {
            foreach (var item in m_BgmAudioSources)
            {
                if (item.isPlaying)
                {
                    if (item.clip != null)
                    {
                        if (item.clip.name == name)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool IsPlayingSFX(string name)
        {
            foreach (var item in m_SfxAudioSources)
            {
                if (item.isPlaying)
                {
                    if (item.clip != null)
                    {
                        if (item.clip.name == name)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void ClearCache()
        {
            for (int i = 0; i < m_ObjectPool.Count; i++)
            {
                var intervalTime = DateTime.Now - m_ObjectPool[i].LastUseTime;
                if (intervalTime.TotalSeconds >= ReleaseInterval)
                {
                    var preDestory = m_ObjectPool[i];
                    m_ObjectPool.RemoveAt(i);
                    Destroy(preDestory.gameObject);
                }
            }
        }

        public void Release(object audioAsset)
        {
            Debug.LogFormat("Clear {0}", audioAsset.ToString());
            Resources.UnloadAsset(audioAsset as AudioClip);
        }

        public void Recovery(AudioSFX go)
        {
            go.gameObject.SetActive(false);
            m_ObjectPool.Add(go);
        }
    }
}
