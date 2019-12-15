using System;

namespace RayFramework.Procedure
{
    internal sealed class ProcedureController
    {
        public ProcedureBase CurrentState { get; private set; }

        private bool m_Started;
        private bool m_Terminated;

        public void StartProcedure<T>() where T : ProcedureBase
        {
            if (m_Terminated)
            {
                throw new RayFrameworkException("[ProcedureController] has been terminated");
            }

            if (m_Started)
            {
                throw new RayFrameworkException("[ProcedureController] has been started");
            }

            var state = Activator.CreateInstance<T>();
            m_Started = true;
            CurrentState = state;
            CurrentState.InjectController(this);
        }

        public void StartProcedure(ProcedureBase State)
        {
            if (m_Terminated)
            {
                throw new RayFrameworkException("[ProcedureController] has been terminated");
            }

            if (m_Started)
            {
                throw new RayFrameworkException("[ProcedureController] has been started");
            }

            m_Started = true;
            CurrentState = State;
            CurrentState.InjectController(this);
        }

        public void OnDestroy()
        {
            if (CurrentState != null)
            {
                CurrentState.OnLeave();
                CurrentState.OnDestroy();
                CurrentState = null;
            }
            m_Terminated = true;
        }

        public void TransTo<T>() where T : ProcedureBase
        {
            if (m_Terminated)
            {
                throw new RayFrameworkException("[ProcedureController] has been terminated");
            }

            if (!m_Started)
            {
                throw new RayFrameworkException("[ProcedureController] has been started");
            }
            if (CurrentState != null)
            {
                CurrentState.OnLeave();
            }

            var state = Activator.CreateInstance<T>();
            CurrentState = state;
            CurrentState.InjectController(this);
        }

        public void StateUpdate()
        {
            if (m_Terminated || !m_Started)
            {
                return;
            }

            if (CurrentState != null)
            {
                if (CurrentState.AtStateEnter)
                {
                    CurrentState.TouchBegin();
                    CurrentState.OnEnter();
                    return;
                }
                CurrentState.OnUpdate();
            }
        }
    }
}
