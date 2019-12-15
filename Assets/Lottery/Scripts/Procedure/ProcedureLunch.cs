using RayFramework.Procedure;

namespace App.Runtime
{
    public class ProcedureLunch :ProcedureBase
    {
        public override void OnEnter()
        {
            InitAppSate();
        }


        private void InitAppSate()
        {
            AppEntry.Store.InitState();
        }
    }
}