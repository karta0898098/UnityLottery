using System.Collections.Generic;
using System.Reflection;

namespace RayFramework
{
    public static partial class Utility
    {
        public static class Json
        {
            public static Dictionary<string, object> ObjectToDictionary(object obj)
            {
                Dictionary<string, object> ret = new Dictionary<string, object>();

                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    string propName = prop.Name;
                    var val = obj.GetType().GetProperty(propName).GetValue(obj, null);
                    if (val != null)
                    {
                        ret.Add(propName, val);
                    }
                    else
                    {
                        ret.Add(propName, null);
                    }
                }
                return ret;
            }

            public static string Serialize(object obj)
            {
                var dict = ObjectToDictionary(obj);
                var jsonString = MiniJSON.Json.Serialize(dict);
                return jsonString;
            }

            public static string Serialize(object[] objs)
            {
                var array = new object[objs.Length];
                int index = 0;
                foreach (var obj in objs)
                {
                    var dict = ObjectToDictionary(obj);
                    array[index] = dict;
                    index++;
                }
                return MiniJSON.Json.Serialize(array);
            }

            public static object Deserialize(string json)
            {
                return MiniJSON.Json.Deserialize(json);
            }
        }
    }
}
