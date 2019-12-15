using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityRayFramework.Runtime
{
    public static class RayFrameworkEntry
    {
        private static readonly LinkedList<RayFrameworkComponent> m_FrameworkComponents = 
            new LinkedList<RayFrameworkComponent>();

        internal const int FrameworkSceneId = 0;

        public static T GetComponent<T>() where T : RayFrameworkComponent
        {
            return (T)GetComponent(typeof(T));
        }

        public static RayFrameworkComponent GetComponent(Type type)
        {
            LinkedListNode<RayFrameworkComponent> current = m_FrameworkComponents.First;
            while (current != null)
            {
                if (current.Value.GetType() == type)
                {
                    return current.Value;
                }

                current = current.Next;
            }

            return null;
        }

        public static RayFrameworkComponent GetComponent(string typeName)
        {
            LinkedListNode<RayFrameworkComponent> current = m_FrameworkComponents.First;
            while (current != null)
            {
                Type type = current.Value.GetType();
                if (type.FullName == typeName || type.Name == typeName)
                {
                    return current.Value;
                }

                current = current.Next;
            }

            return null;
        }

        public static void Shutdown(ShutdownType shutdownType)
        {
            var baseComponent = GetComponent<BaseComponent>();
            if (baseComponent != null)
            {
                baseComponent.Shutdown();
                baseComponent = null;
            }

            m_FrameworkComponents.Clear();

            if (shutdownType == ShutdownType.None)
            {
                return;
            }

            if (shutdownType == ShutdownType.Restart)
            {
                SceneManager.LoadScene(FrameworkSceneId);
                return;
            }

            if (shutdownType == ShutdownType.Quit)
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                return;
            }
        }

        internal static void RegisterComponent(RayFrameworkComponent rayFrameworkComponent)
        {
            if (rayFrameworkComponent == null)
            {
                Debug.Log("Game Framework component is invalid.");
                return;
            }

            var type = rayFrameworkComponent.GetType();

            var current = m_FrameworkComponents.First;
            while (current != null)
            {
                if (current.Value.GetType() == type)
                {
                    Debug.LogFormat("Game Framework component type '{0}' is already exist.", type.FullName);
                    return;
                }
                current = current.Next;
            }
            m_FrameworkComponents.AddLast(rayFrameworkComponent);
        }
    }
}
