using System;

namespace RayFramework
{
    public interface IBlackboardItem
    {
        Type GetValueType();
        object GetPubValue();
    }

    public partial class BlackboardManager
    {
        private class BlackboardItem<T> : IBlackboardItem
        {
            private T m_Value { get; set; }

            public void SetValue(T value)
            {
                m_Value = value;
            }

            public T GetValue()
            {
                return m_Value;
            }

            public object GetPubValue()
            {
                return m_Value;
            }

            public Type GetValueType()
            {
                return m_Value.GetType();
            }
        }
    }
}
