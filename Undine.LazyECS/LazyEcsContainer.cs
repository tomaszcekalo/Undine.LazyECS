using LazyECS;
using LazyECS.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using Undine.Core;

namespace Undine.LazyECS
{
    public class LazyEcsContainer : EcsContainer
    {
        public ComponentAssignCreator ComponentAssignCreator { get; }
        public UndineEcsManager ECSManager { get; }
        public UndineEntitySystemsController EntitySystems { get; private set; }

        public override void Init()
        {
            base.Init();
            EntitySystems = ECSManager.Init();
        }

        public LazyEcsContainer()
        {
            ComponentAssignCreator = new ComponentAssignCreator();
            ECSManager = new UndineEcsManager(ComponentAssignCreator);
        }

        public override void AddSystem<A>(UnifiedSystem<A> system)
        {
            RegisterComponentType<A>();
            LazyEcsSystemMiddlewareSingleton.GetInstance().AddSystem(system, typeof(UnifiedSystem<A>));
            ECSManager.Register<LazyEcsSystem<A>>();
        }

        public override void AddSystem<A, B>(UnifiedSystem<A, B> system)
        {
            RegisterComponentType<A>();
            RegisterComponentType<B>();
            LazyEcsSystemMiddlewareSingleton.GetInstance().AddSystem(system, typeof(UnifiedSystem<A, B>));
            ECSManager.Register<LazyEcsSystem<A, B>>();
        }

        public override void AddSystem<A, B, C>(UnifiedSystem<A, B, C> system)
        {
            RegisterComponentType<A>();
            RegisterComponentType<B>();
            RegisterComponentType<C>();
            LazyEcsSystemMiddlewareSingleton.GetInstance().AddSystem(system, typeof(UnifiedSystem<A, B, C>));
            ECSManager.Register<LazyEcsSystem<A, B, C>>();
        }

        public override void AddSystem<A, B, C, D>(UnifiedSystem<A, B, C, D> system)
        {
            RegisterComponentType<A>();
            RegisterComponentType<B>();
            RegisterComponentType<C>();
            RegisterComponentType<D>();
            LazyEcsSystemMiddlewareSingleton.GetInstance().AddSystem(system, typeof(UnifiedSystem<A, B, C, D>));
            ECSManager.Register<LazyEcsSystem<A, B, C, D>>();
        }

        public override IUnifiedEntity CreateNewEntity()
        {
            var entity = ECSManager.CreateEntity();

            return new LazyEcsEntity()
            {
                Entity = entity,
                ECSManager = ECSManager
            };
        }

        public override ISystem GetSystem<A>(UnifiedSystem<A> system)
        {
            RegisterComponentType<A>();
            var result = new LazyEcsSystem<A>()
            {
                System = system
            };
            ECSManager.Register(result);
            return result;
        }

        public override ISystem GetSystem<A, B>(UnifiedSystem<A, B> system)
        {
            RegisterComponentType<A>();
            RegisterComponentType<B>();
            var result = new LazyEcsSystem<A, B>()
            {
                System = system
            };
            ECSManager.Register(result);
            return result;
        }

        public override ISystem GetSystem<A, B, C>(UnifiedSystem<A, B, C> system)
        {
            RegisterComponentType<A>();
            RegisterComponentType<B>();
            RegisterComponentType<C>();
            var result = new LazyEcsSystem<A, B, C>()
            {
                System = system
            };
            ECSManager.Register(result);
            return result;
        }

        public override ISystem GetSystem<A, B, C, D>(UnifiedSystem<A, B, C, D> system)
        {
            RegisterComponentType<A>();
            RegisterComponentType<B>();
            RegisterComponentType<C>();
            RegisterComponentType<D>();
            var result = new LazyEcsSystem<A, B, C, D>()
            {
                System = system
            };
            ECSManager.Register(result);
            return result;
        }

        public override void Run()
        {
            EntitySystems.OnUpdate();
        }
    }
}