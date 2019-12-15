using System;
using System.Collections.Generic;

namespace RayFramework.Procedure
{
    public interface IProcedureManager
    {
        ProcedureBase CurrentProcedure { get; }

        void StartProcedure<T>() where T : ProcedureBase;

        void StartProcedure(ProcedureBase procedureType);
    }
}
