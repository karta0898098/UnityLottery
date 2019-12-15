using System.Collections.Generic;

namespace RayFramework
{
    public interface IBlackboardManager
    {
        void SetValue<T>(string key, T value);

        T GetValue<T>(string key, T defaultValue);

        T GetValue<T>(string key);

        Dictionary<string, object> GetDatas();
    }
}
