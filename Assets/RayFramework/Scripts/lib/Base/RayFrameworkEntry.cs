
using System;
using System.Collections.Generic;

namespace RayFramework
{
    public static class RayFrameworkEntry
    {
        private static readonly LinkedList<RayCoreModule> m_GameFrameworkModules = new LinkedList<RayCoreModule>();

        /// <summary>
        /// 輪詢所有遊戲框架模組
        /// </summary>
        /// <param name="tickTime">邏輯時間(sec)</param>
        /// <param name="realTickTime">真實時間(sec)</param>
        public static void Update(float tickTime, float realTickTime)
        {
            foreach (var module in m_GameFrameworkModules)
            {
                module.Update(tickTime, realTickTime);
            }
        }
        /// <summary>
        /// 關閉遊戲框架
        /// </summary>
        public static void Shutdown()
        {
            for (var current = m_GameFrameworkModules.Last; current != null; current = current.Previous)
            {
                current.Value.Shoudown();
            }
            m_GameFrameworkModules.Clear();
        }

        /// <summary>
        /// 獲取的遊戲框架
        /// </summary>
        /// <typeparam name="T">要獲取的遊戲框架類型</typeparam>
        /// <returns>要獲取的遊戲框架</returns>
        /// <remarks>如果框架不存在，會透過Lazy的方法創建實體</remarks>
        public static T GetModule<T>() where T : class
        {
            var interfaceType = typeof(T);
            if (!interfaceType.IsInterface)
            {
                throw new RayFrameworkException(string.Format("You must get module by interface, but '{0}' is not.", interfaceType.FullName));
            }

            var moduleName = string.Format("{0}.{1}", interfaceType.Namespace, interfaceType.Name.Substring(1));
            var moduleType = Type.GetType(moduleName);
            if (moduleType == null)
            {
                throw new RayFrameworkException(string.Format("Can not find Game Framework module type '{0}'.", moduleName));
            }

            return GetModule(moduleType) as T;
        }

        /// <summary>
        /// 獲取遊戲模組
        /// </summary>
        /// <returns>要獲取的模組</returns>
        /// <param name="moduleType">模組的類型</param>
        private static RayCoreModule GetModule(Type moduleType)
        {
            foreach (var module in m_GameFrameworkModules)
            {
                if (module.GetType() == moduleType)
                {
                    return module;
                }
            }
            return CreateModule(moduleType);
        }

        /// <summary>
        /// 創立遊戲框架
        /// </summary>
        /// <returns>模組</returns>
        /// <param name="moduleType">模組的類型</param>
        private static RayCoreModule CreateModule(Type moduleType)
        {
            var module = (RayCoreModule)Activator.CreateInstance(moduleType);

            if (module == null)
            {
                throw new RayFrameworkException(string.Format("Can not create module '{0}'.", moduleType.Name));
            }

            var current = m_GameFrameworkModules.First;
            while (current != null)
            {
                if (module.Priority > current.Value.Priority)
                {
                    break;
                }
                current = current.Next;
            }

            if (current != null)
            {
                m_GameFrameworkModules.AddBefore(current, module);
            }
            else
            {
                m_GameFrameworkModules.AddLast(module);
            }
            return module;
        }
    }
}