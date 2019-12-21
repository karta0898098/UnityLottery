using UnityRayFramework.Runtime;

namespace App.Runtime
{
    public partial class AppEntry
    {
        public static StoreComponent Store { get; private set; }

        public static RouterComponent Router { get; private set; }
        private void InitCustomComponent()
        {
            Store = RayFrameworkEntry.GetComponent<StoreComponent>();
            Router = RayFrameworkEntry.GetComponent<RouterComponent>();
        }
    }
}