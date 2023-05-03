using LazyECS;
using LazyECS.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using Undine.Core;

namespace Undine.LazyECS
{
    public class LazyEcsContainer : Undine.Core.EcsContainer
    {
        public ComponentAssignCreator ComponentAssignCreator { get; }
        public ECSManager ECSManager { get; }
        public EntitySystemsController EntitySystems { get; private set; }

        public override void Init()
        {
            base.Init();
            EntitySystems = ECSManager.Init();
        }

        public LazyEcsContainer()
        {
            ComponentAssignCreator = new ComponentAssignCreator();
            ECSManager = new ECSManager(ComponentAssignCreator);
        }

        public override void AddSystem<A>(UnifiedSystem<A> system)
        {
            LazyEcsSystemMiddlewareSingleton.GetInstance().AddSystem(system, typeof(UnifiedSystem<A>));
            ECSManager.Register<LazyEcsSystem<A>>();
        }

        public override void AddSystem<A, B>(UnifiedSystem<A, B> system)
        {
            LazyEcsSystemMiddlewareSingleton.GetInstance().AddSystem(system, typeof(UnifiedSystem<A, B>));
            ECSManager.Register<LazyEcsSystem<A, B>>();
        }

        public override void AddSystem<A, B, C>(UnifiedSystem<A, B, C> system)
        {
            LazyEcsSystemMiddlewareSingleton.GetInstance().AddSystem(system, typeof(UnifiedSystem<A, B, C>));
            ECSManager.Register<LazyEcsSystem<A, B, C>>();
        }

        public override void AddSystem<A, B, C, D>(UnifiedSystem<A, B, C, D> system)
        {
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
            throw new NotImplementedException();
        }

        public override ISystem GetSystem<A, B>(UnifiedSystem<A, B> system)
        {
            throw new NotImplementedException();
        }

        public override ISystem GetSystem<A, B, C>(UnifiedSystem<A, B, C> system)
        {
            throw new NotImplementedException();
        }

        public override ISystem GetSystem<A, B, C, D>(UnifiedSystem<A, B, C, D> system)
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            EntitySystems.OnUpdate();
        }
    }
}