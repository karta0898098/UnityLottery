using RayFramework.Procedure;

namespace App.Runtime
{
    public class ProcedureLunch : ProcedureBase
    {
        public override void OnEnter()
        {
            InitAppSate();
            UnityEngine.Application.targetFrameRate = 60;
            AppEntry.Router.NavgationTo("Home");
        }


        private void InitAppSate()
        {
            AppEntry.Store.InitState();
        }

    }
}