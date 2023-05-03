using LazyECS.Component;
using LazyECS.System;
using System;
using System.Collections.Generic;
using System.Text;
using Undine.Core;

namespace Undine.LazyECS
{
    public class LazyEcsSystem<A> : ISystemProcessing, Core.ISystem
        where A : struct
    {
        [InjectComponent] public LazyEcsComponent<A>[] AComponent;
        public UnifiedSystem<A> System { get; set; }

        public void Execute()
        {
            if (System == null)
            {
                System = (UnifiedSystem<A>)LazyEcsSystemMiddlewareSingleton.GetInstance().GetSystem(typeof(UnifiedSystem<A>));
            }
            for (int i = 0; i < AComponent.Length; i++)
            {
                System.ProcessSingleEntity(i, ref AComponent[i].Value);
            }
        }

        public void ProcessAll()
        {
            Execute();
        }
    }

    public class LazyEcsSystem<A, B> : ISystemProcessing, Core.ISystem
        where A : struct
        where B : struct
    {
        [InjectComponent] public LazyEcsComponent<A>[] AComponent;
        [InjectComponent] public LazyEcsComponent<B>[] BComponent;
        public UnifiedSystem<A, B> System { get; set; }

        public void Execute()
        {
            if (System == null)
            {
                System = (UnifiedSystem<A, B>)LazyEcsSystemMiddlewareSingleton.GetInstance().GetSystem(typeof(UnifiedSystem<A, B>));
            }
            for (int i = 0; i < AComponent.Length; i++)
            {
                System.ProcessSingleEntity(i, ref AComponent[i].Value, ref BComponent[i].Value);
            }
        }

        public void ProcessAll()
        {
            Execute();
        }
    }

    public class LazyEcsSystem<A, B, C> : ISystemProcessing, Core.ISystem
        where A : struct
        where B : struct
        where C : struct
    {
        [InjectComponent] public LazyEcsComponent<A>[] AComponent;
        [InjectComponent] public LazyEcsComponent<B>[] BComponent;
        [InjectComponent] public LazyEcsComponent<C>[] CComponent;

        public UnifiedSystem<A, B, C> System { get; set; }

        public void Execute()
        {
            if (System == null)
            {
                System = (UnifiedSystem<A, B, C>)LazyEcsSystemMiddlewareSingleton.GetInstance().GetSystem(typeof(UnifiedSystem<A, B, C>));
            }
            for (int i = 0; i < AComponent.Length; i++)
            {
                System.ProcessSingleEntity(i,
                    ref AComponent[i].Value,
                    ref BComponent[i].Value,
                    ref CComponent[i].Value);
            }
        }

        public void ProcessAll()
        {
            Execute();
        }
    }

    public class LazyEcsSystem<A, B, C, D> : ISystemProcessing, Core.ISystem
        where A : struct
        where B : struct
        where C : struct
        where D : struct
    {
        [InjectComponent] public LazyEcsComponent<A>[] AComponent;
        [InjectComponent] public LazyEcsComponent<B>[] BComponent;
        [InjectComponent] public LazyEcsComponent<C>[] CComponent;
        [InjectComponent] public LazyEcsComponent<D>[] DComponent;

        public UnifiedSystem<A, B, C, D> System { get; set; }

        public void Execute()
        {
            if (System == null)
            {
                System = (UnifiedSystem<A, B, C, D>)LazyEcsSystemMiddlewareSingleton.GetInstance().GetSystem(typeof(UnifiedSystem<A, B, C, D>));
            }
            for (int i = 0; i < AComponent.Length; i++)
            {
                System.ProcessSingleEntity(i,
                    ref AComponent[i].Value,
                    ref BComponent[i].Value,
                    ref CComponent[i].Value,
                    ref DComponent[i].Value);
            }
        }

        public void ProcessAll()
        {
            Execute();
        }
    }
}