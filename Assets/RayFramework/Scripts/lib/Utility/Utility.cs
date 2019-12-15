using System;

using System.Collections.Generic;

namespace RayFramework
{
    public static partial class Utility
    {
        public static class Assembly
        {
            private static readonly System.Reflection.Assembly[] m_Assemblies = null;
            private static readonly Dictionary<string, Type> m_CachedTypes = new Dictionary<string, Type>();

            static Assembly()
            {
                m_Assemblies = AppDomain.CurrentDomain.GetAssemblies();
            }

            public static System.Reflection.Assembly[] GetAssemblies()
            {
                return m_Assemblies;
            }

            public static Type[] GetTypes()
            {
                var results = new List<Type>();
                foreach (var assembly in m_Assemblies)
                {
                    results.AddRange(assembly.GetTypes());
                }
                return results.ToArray();
            }

            public static void GetTypes(List<Type> results)
            {
                if (results == null)
                {
                    throw new RayFrameworkException("Results is invalid");
                }

                results.Clear();
                foreach (var assembly in m_Assemblies)
                {
                    results.AddRange(assembly.GetTypes());
                }
            }

            public static Type GetType(string typeName)
            {
                Type type = null;
                if (m_CachedTypes.TryGetValue(typeName, out type))
                {
                    return type;
                }


                type = Type.GetType(typeName);
                if (type != null)
                {
                    m_CachedTypes.Add(typeName, type);
                    return type;
                }


                foreach (var assembly in m_Assemblies)
                {
                    type = Type.GetType(string.Format("{0}, {1}", typeName, assembly.FullName));
                    if (type != null)
                    {
                        m_CachedTypes.Add(typeName, type);
                        return type;
                    }
                }

                return null;
            }
        }
    }
}
