using System;
using System.Collections.Generic;

namespace RayFramework
{
    public abstract class ObjectBase
    {
        public string Name { get; private set; }

        public object Target { get; private set; }

        public DateTime LastUseTime{ get; internal set; }

        protected internal virtual void OnSpawn()
        {

        }

        protected internal virtual void OnUnspawn()
        {

        }

        protected internal abstract void Release(bool isShutdown);
    }
}
