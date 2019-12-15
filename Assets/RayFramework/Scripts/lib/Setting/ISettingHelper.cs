
namespace RayFramework.Setting
{
    public interface ISettingHelper
    {
        bool Load();

        bool Save();

        bool HasSetting(string settingName);

        void RemoveSetting(string settingName);

        void RemoveAllSettings();

        bool GetBool(string settingName);

        bool GetBool(string settingName, bool defaultValue);

        void SetBool(string settingName, bool value);

        int GetInt(string settingName);

        int GetInt(string settingName, int defaultValue);

        void SetInt(string settingName, int value);

        float GetFloat(string settingName);

        float GetFloat(string settingName, float defaultValue);

        void SetFloat(string settingName, float value);

        string GetString(string settingName);

        string GetString(string settingName, string defaultValue);

        void SetString(string settingName, string value);
    }
}
