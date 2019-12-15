using System;

namespace RayFramework.Audio
{
    public interface IAudioHelper
    {
        void ResouceAudio(string name, Action<object> AudioAsset);

        void PlayBGM(object audioAsset, bool loop = true);

        void PlaySFX(object audioAsset);

        void PlaySFX(object audioAsset, object parent, bool setParent = false);

        void PlayPosSFX(object audioAsset, object pos);

        void StopBGM(string name);

        void MuteMaster();

        void MuteBGM();

        void MuteSFX();

        void ResumeMuteMaster();

        void ResumeMuteBGM();

        void ReumeMuteSFX();

        void SetMasterVolume(float value);

        void SetBGMVolume(float value);

        void SetSFXVolume(float value);

        bool IsPlayingBGM(string name);

        bool IsPlayingSFX(string name);

        void ClearCache();

        void Release(object audioAsset);
    }
}
