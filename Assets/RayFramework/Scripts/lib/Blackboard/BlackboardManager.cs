using System.Collections.Generic;

namespace RayFramework
{
    public partial class BlackboardManager : RayCoreModule, IBlackboardManager
    {
        private Dictionary<string, object> m_Items;

        public BlackboardManager()
        {
            m_Items = new Dictionary<string, object>();
        }

        public void SetValue<T>(string key, T value)
        {
            BlackboardItem<T> item;
            if (!m_Items.ContainsKey(key))
            {
                item = new BlackboardItem<T>();
                m_Items.Add(key, item);
            }
            else
            {
                var castItem = m_Items[key] as IBlackboardItem;
                if (castItem.GetValueType() != typeof(T))
                {
                    throw new RayFrameworkException("Value type Error.");
                }
                item = (BlackboardItem<T>)castItem;
            }

            item.SetValue(value);
        }

        public T GetValue<T>(string key, T defaultValue)
        {
            if (!m_Items.ContainsKey(key))
            {
                return defaultValue;
            }
            return ((BlackboardItem<T>)m_Items[key]).GetValue();
        }

        public T GetValue<T>(string key)
        {
            if (!m_Items.ContainsKey(key))
            {
                throw new RayFrameworkException("Blackboard doesn't has value.");
            }
            return ((BlackboardItem<T>)m_Items[key]).GetValue();
        }

        public Dictionary<string, object> GetDatas()
        {
            return m_Items;
        }

        internal override void Update(float timeTick, float realTimeTick)
        {

        }

        internal override void Shoudown()
        {
            m_Items = null;
        }
    }
}
