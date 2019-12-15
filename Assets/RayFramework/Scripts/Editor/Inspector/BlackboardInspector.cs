using System.Collections.Generic;
using RayFramework;
using UnityEditor;
using UnityRayFramework.Runtime;

namespace UnityRayFramework.Editor
{
    [CustomEditor(typeof(BlackboardComponent))]
    internal sealed class BlackboardInspector : RayFrameworkInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!EditorApplication.isPlaying)
            {
                EditorGUILayout.HelpBox("Available during runtime only.", MessageType.Info);
                return;
            }

            BlackboardComponent t = (BlackboardComponent)target;
            DrawInBlackboardValue(t, t.GetDatas());

            Repaint();
        }

        public void DrawInBlackboardValue(BlackboardComponent t, Dictionary<string, object> Datas)
        {

            if (Datas.Count == 0)
            {
                EditorGUILayout.LabelField("Blackboard:", "Null");
            }
            else
            {
                EditorGUILayout.LabelField("Blackboard", "");
                foreach (var item in Datas)
                {
                    Datas.TryGetValue(item.Key, out var value);
                    var cast = value as IBlackboardItem;
                    var type = cast.GetValueType().Name;
                    var showValue = cast.GetValueType().IsClass ? "Not Support Class" : cast.GetPubValue().ToString();
                    EditorGUIUtility.labelWidth = 100;
                    EditorGUILayout.LabelField(
                        string.Format("{0}:", item.Key),
                        string.Format("Type:<{0}>   Value :{1}", type, showValue));
                }
            }
        }
    }
}
