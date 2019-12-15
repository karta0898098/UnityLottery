using System.Collections;
using System.Collections.Generic;

namespace RayFramework.Audio
{
    public interface IAudioManager
    {
        bool IsMasterMute { get; }
        bool IsBGMMute { get; }
        bool IsSFXMute { get; }

        void SetAudioHelper(IAudioHelper audioHelper);

        void PlayBGM(string name, bool loop = true);
        void PlaySFX(string name);
        void PlaySFX(string name, object parent, bool setParent = false);
        void PlayPosSFX(string name, object pos);
        void StopBGM(string name);

        void MuteMaster();
        void MuteBGM();
        void MuteSFX();
        void ResumeMuteMaster();
        void ResumeMuteBGM();
        void ResumeMuteSFX();

        void SetMasterVolume(float value);
        void SetBGMVolume(float value);
        void SetSFXVolume(float value);

        bool IsPlayingBGM(string name);
        bool IsPlayingSFX(string name);

        void SetReleaeInterval(float releaseInterval);
        void SetClearInterval(float clearCacheInterval);
    }
}
