using LazyECS;
using System;
using System.Collections.Generic;
using System.Text;
using Undine.Core;

namespace Undine.LazyECS
{
    public class LazyEcsEntity : IUnifiedEntity
    {
        public LazyEcsEntity()
        { }

        public Entity Entity { get; internal set; }
        public ECSManager ECSManager { get; internal set; }

        public void AddComponent<A>(in A component) where A : struct
        {
            this.ECSManager.EntityManager.AddComponent(Entity, new LazyEcsComponent<A>()
            {
                Value = component
            });
        }

        public ref A GetComponent<A>() where A : struct
        {
            return ref this.ECSManager.EntityContainer.GetComponent<LazyEcsComponent<A>>(Entity).Value;
        }
    }
}