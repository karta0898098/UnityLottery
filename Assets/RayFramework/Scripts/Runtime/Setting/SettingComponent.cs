using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RayFramework;
using RayFramework.Setting;

namespace UnityRayFramework.Runtime
{
    public sealed class SettingComponent : RayFrameworkComponent
    {
        private ISettingManager m_SettingManager;

        protected override void Awake()
        {
            base.Awake();

            var settingHelper = GetComponent<SettingHelperBase>();
            m_SettingManager = RayFramework.RayFrameworkEntry.GetModule<ISettingManager>();
            m_SettingManager.SetSettingHelper(settingHelper);
        }

        public bool Load()
        {
            return m_SettingManager.Load();
        }

        public bool Save()
        {
            return m_SettingManager.Save();
        }

        public bool HasSetting(string settingName)
        {
            return m_SettingManager.HasSetting(settingName);
        }

        public void RemoveSetting(string settingName)
        {
            m_SettingManager.RemoveSetting(settingName);
        }

        public void RemoveAllSettings()
        {
            m_SettingManager.RemoveAllSettings();
        }

        #region Type Bool Setting
        public bool GetBool(string settingName)
        {
            return m_SettingManager.GetBool(settingName);
        }

        public bool GetBool(string settingName, bool defaultValue)
        {
            return m_SettingManager.GetBool(settingName, defaultValue);
        }

        public void SetBool(string settingName, bool value)
        {
            m_SettingManager.SetBool(settingName, value);
        }
        #endregion

        #region Type Int Setting
        public int GetInt(string settingName)
        {
            return m_SettingManager.GetInt(settingName);
        }

        public int GetInt(string settingName, int defaultValue)
        {
            return m_SettingManager.GetInt(settingName, defaultValue);
        }

        public void SetInt(string settingName, int value)
        {
            m_SettingManager.SetInt(settingName, value);
        }
        #endregion

        #region Type float Setting
        public float GetFloat(string settingName)
        {
            return m_SettingManager.GetFloat(settingName);
        }

        public float GetFloat(string settingName, float defaultValue)
        {
            return m_SettingManager.GetFloat(settingName, defaultValue);
        }

        public void SetFloat(string settingName, float value)
        {
            m_SettingManager.SetFloat(settingName, value);
        }
        #endregion

        #region Type String Setting
        public string GetString(string settingName)
        {
            return m_SettingManager.GetString(settingName);
        }

        public string GetString(string settingName, string defaultValue)
        {
            return m_SettingManager.GetString(settingName, defaultValue);
        }

        public void SetString(string settingName, string value)
        {
            m_SettingManager.SetString(settingName, value);
        }
        #endregion
    }
}
