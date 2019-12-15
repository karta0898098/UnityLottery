using RayFramework;
using RayFramework.Procedure;
using System;
using System.Collections;
using UnityEngine;

namespace UnityRayFramework.Runtime
{
    public sealed class ProcedureComponent : RayFrameworkComponent
    {
        private IProcedureManager m_ProcedureManager;
        private ProcedureBase m_EntranceProcedure = null;

        [SerializeField]
        private string[] m_AvailableProcedureTypeNames;

        [SerializeField]
        private string m_EntranceProcedureTypeName;

        public ProcedureBase CurrentPricedure => m_ProcedureManager.CurrentProcedure;

        protected override void Awake()
        {
            base.Awake();

            m_ProcedureManager = RayFramework.RayFrameworkEntry.GetModule<IProcedureManager>();
        }

        private IEnumerator Start()
        {
            var procedures = new ProcedureBase[m_AvailableProcedureTypeNames.Length];
            for (int i = 0; i < m_AvailableProcedureTypeNames.Length; i++)
            {
                var procedureType = Utility.Assembly.GetType(m_AvailableProcedureTypeNames[i]);
                if (procedureType == null)
                {
                    Debug.LogErrorFormat("Can not find procedure type '{0}'.", m_AvailableProcedureTypeNames[i]);
                    yield break;
                }

                procedures[i] = (ProcedureBase)Activator.CreateInstance(procedureType);
                if (procedures[i] == null)
                {
                    Debug.LogErrorFormat("Can not create procedure instance '{0}'.", m_AvailableProcedureTypeNames[i]);
                    yield break;
                }
                if (m_EntranceProcedureTypeName == m_AvailableProcedureTypeNames[i])
                {
                    m_EntranceProcedure = procedures[i];
                }
            }

            if (m_EntranceProcedure == null)
            {
                Debug.LogError("EntranceProcedure procedure is invalid.");
                yield break;
            }

            yield return new WaitForEndOfFrame();

            m_ProcedureManager.StartProcedure(m_EntranceProcedure);
        }

    }
}
