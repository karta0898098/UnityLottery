using RayFramework.Setting;
using System;
using UnityEngine;

namespace UnityRayFramework.Runtime
{
    public abstract class SettingHelperBase : MonoBehaviour, ISettingHelper
    {
        public abstract bool GetBool(string settingName);
        public abstract bool GetBool(string settingName, bool defaultValue);
        public abstract float GetFloat(string settingName);
        public abstract float GetFloat(string settingName, float defaultValue);
        public abstract int GetInt(string settingName);
        public abstract int GetInt(string settingName, int defaultValue);
        public abstract string GetString(string settingName);
        public abstract string GetString(string settingName, string defaultValue);
        public abstract bool HasSetting(string settingName);
        public abstract bool Load();
        public abstract void RemoveAllSettings();
        public abstract void RemoveSetting(string settingName);
        public abstract bool Save();
        public abstract void SetBool(string settingName, bool value);
        public abstract void SetFloat(string settingName, float value);
        public abstract void SetInt(string settingName, int value);
        public abstract void SetString(string settingName, string value);
    }
}
