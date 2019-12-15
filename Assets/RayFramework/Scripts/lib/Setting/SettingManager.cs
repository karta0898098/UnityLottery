using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayFramework.Setting
{
    internal sealed class SettingManager : RayCoreModule, ISettingManager
    {
        private ISettingHelper m_SettingHelper;

        public SettingManager()
        {
            m_SettingHelper = null;
        }

        public void SetSettingHelper(ISettingHelper settingHelper)
        {
            m_SettingHelper = settingHelper;
        }

        public bool GetBool(string settingName) => m_SettingHelper.GetBool(settingName);

        public bool GetBool(string settingName, bool defaultValue) => m_SettingHelper.GetBool(settingName, defaultValue);

        public float GetFloat(string settingName) => m_SettingHelper.GetFloat(settingName);

        public float GetFloat(string settingName, float defaultValue) => m_SettingHelper.GetFloat(settingName, defaultValue);

        public int GetInt(string settingName) => m_SettingHelper.GetInt(settingName);

        public int GetInt(string settingName, int defaultValue) => m_SettingHelper.GetInt(settingName,defaultValue);

        public string GetString(string settingName) => m_SettingHelper.GetString(settingName);

        public string GetString(string settingName, string defaultValue) => m_SettingHelper.GetString(settingName,defaultValue);

        public bool HasSetting(string settingName) => m_SettingHelper.HasSetting(settingName);

        public bool Load() => m_SettingHelper.Load();

        public void RemoveAllSettings()
        {
            m_SettingHelper.RemoveAllSettings();
        }

        public void RemoveSetting(string settingName)
        {
            m_SettingHelper.RemoveSetting(settingName);
        }

        public bool Save()
        {
            return m_SettingHelper.Save();
        }

        public void SetBool(string settingName, bool value)
        {
            m_SettingHelper.SetBool(settingName, value);
        }

        public void SetFloat(string settingName, float value)
        {
            m_SettingHelper.SetFloat(settingName, value);
        }

        public void SetInt(string settingName, int value)
        {
            m_SettingHelper.SetInt(settingName, value);
        }

        public void SetString(string settingName, string value)
        {
            m_SettingHelper.SetString(settingName, value);
        }

        internal override void Shoudown()
        {

        }

        internal override void Update(float timeTick, float realTimeTick)
        {
            
        }
    }
}
