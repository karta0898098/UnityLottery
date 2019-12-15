
namespace RayFramework.Procedure
{
    public abstract class ProcedureBase
    {
        internal ProcedureController StateController { get; private set; }

        public bool AtStateEnter { get; private set; } = true;

        internal void InjectController(ProcedureController controller)
        {
            StateController = controller;
        }

        public void TouchBegin()
        {
            AtStateEnter = false;
        }

        protected void TransTo<T>() where T : ProcedureBase
        {
            StateController.TransTo<T>();
        }

        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnLeave() { }
        public virtual void OnDestroy() { }
    }
}
