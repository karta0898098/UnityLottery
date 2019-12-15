using UnityEngine;

namespace UnityRayFramework.Runtime
{
    public class DefaultSettingHelper : SettingHelperBase
    {
        public override bool GetBool(string settingName)
        {
            return PlayerPrefs.GetInt(settingName) != 0;
        }

        public override bool GetBool(string settingName, bool defaultValue)
        {
            return PlayerPrefs.GetInt(settingName, defaultValue ? 1 : 0) != 0;
        }

        public override float GetFloat(string settingName)
        {
            return PlayerPrefs.GetFloat(settingName);
        }

        public override float GetFloat(string settingName, float defaultValue)
        {
            return PlayerPrefs.GetFloat(settingName, defaultValue);
        }

        public override int GetInt(string settingName)
        {
            return PlayerPrefs.GetInt(settingName);
        }

        public override int GetInt(string settingName, int defaultValue)
        {
            return PlayerPrefs.GetInt(settingName, defaultValue);
        }

        public override string GetString(string settingName)
        {
            return PlayerPrefs.GetString(settingName);
        }

        public override string GetString(string settingName, string defaultValue)
        {
            return PlayerPrefs.GetString(settingName, defaultValue);
        }

        public override bool HasSetting(string settingName)
        {
            return PlayerPrefs.HasKey(settingName);
        }

        public override bool Load()
        {
            return true;
        }

        public override void RemoveAllSettings()
        {
            PlayerPrefs.DeleteAll();
        }

        public override void RemoveSetting(string settingName)
        {
            PlayerPrefs.DeleteKey(settingName);
        }

        public override bool Save()
        {
            PlayerPrefs.Save();
            return true;
        }

        public override void SetBool(string settingName, bool value)
        {
            PlayerPrefs.SetInt(settingName, value ? 1 : 0);
        }

        public override void SetFloat(string settingName, float value)
        {
            PlayerPrefs.SetFloat(settingName, value);
        }

        public override void SetInt(string settingName, int value)
        {
            PlayerPrefs.SetInt(settingName, value);
        }

        public override void SetString(string settingName, string value)
        {
            PlayerPrefs.SetString(settingName, value);
        }
    }
}
