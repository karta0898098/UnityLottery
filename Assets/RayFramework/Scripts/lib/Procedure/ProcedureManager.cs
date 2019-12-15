using System;

namespace RayFramework.Procedure
{
    internal sealed class ProcedureManager : RayCoreModule, IProcedureManager
    {
        internal override int Priority => -10;

        internal ProcedureController Controller;

        public ProcedureManager()
        {
            Controller = new ProcedureController();
        }

        public ProcedureBase CurrentProcedure => Controller.CurrentState;

        public void StartProcedure<T>() where T : ProcedureBase
        {
            Controller.StartProcedure<T>();
        }

        public void StartProcedure(ProcedureBase procedureType)
        {
            Controller.StartProcedure(procedureType);
        }

        internal override void Update(float timeTick, float realTimeTick)
        {
            Controller.StateUpdate();
        }

        internal override void Shoudown()
        {
            Controller.OnDestroy();
        }
    }
}
