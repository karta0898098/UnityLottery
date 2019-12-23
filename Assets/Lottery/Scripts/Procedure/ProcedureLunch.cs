using RayFramework.Procedure;

using System.Collections.Generic;
using UnityEngine;

namespace App.Runtime
{
    public class ProcedureLunch : ProcedureBase
    {
        public override void OnEnter()
        {
            InitAppSate();
            Application.targetFrameRate = 60;
            AppEntry.Router.NavgationTo("Home");
        }


        private void InitAppSate()
        {
            AppEntry.Store.InitState();
        }

    }
}