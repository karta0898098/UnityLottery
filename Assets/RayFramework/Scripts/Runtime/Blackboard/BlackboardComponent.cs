using System.Collections.Generic;
using UnityEngine;
using RayFramework;

namespace UnityRayFramework.Runtime
{
    public class BlackboardComponent : RayFrameworkComponent
    {
        private IBlackboardManager m_Blackboard = null;

        public void Start()
        {
            m_Blackboard = RayFramework.RayFrameworkEntry.GetModule<IBlackboardManager>();

            if (m_Blackboard == null)
            {
                Debug.LogError("Blackboard is null");
            }
        }

        public void SetValue<T>(string key, T value)
        {
            m_Blackboard.SetValue(key, value);
        }

        public T GetValue<T>(string key, T defaultValue)
        {
            return m_Blackboard.GetValue(key, defaultValue);
        }

        public T GetValue<T>(string key)
        {
            return m_Blackboard.GetValue<T>(key);
        }

        public Dictionary<string, object> GetDatas()
        {
            return m_Blackboard.GetDatas();
        }
    }
}
