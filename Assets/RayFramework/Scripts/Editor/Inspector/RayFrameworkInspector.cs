using UnityEditor;

namespace UnityRayFramework.Editor
{
    public class RayFrameworkInspector : UnityEditor.Editor
    {
        private bool m_IsCompiling;

        public override void OnInspectorGUI()
        {
            if (m_IsCompiling && !EditorApplication.isCompiling)
            {
                m_IsCompiling = false;
                OnCompileComplete();
            }
            else if (!m_IsCompiling && EditorApplication.isCompiling)
            {
                m_IsCompiling = true;
                OnCompileStart();
            }
        }

        protected virtual void OnCompileStart()
        {

        }

        protected virtual void OnCompileComplete()
        {

        }
    }
}
