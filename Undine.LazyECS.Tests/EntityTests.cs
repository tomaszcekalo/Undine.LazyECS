using NSubstitute;
using Undine.Core;
using Undine.LazyECS.Tests.Components;

namespace Undine.LazyECS.Tests
{
    [TestClass]
    public class EntityTests
    {
        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public void ComponentCanBeAdded()
        {
            var container = new LazyEcsContainer();
            container.Init();
            var entity = container.CreateNewEntity();
            entity.AddComponent(new AComponent());
        }

        [TestMethod]
        public void ComponentCanBeRetrieved()
        {
            var container = new LazyEcsContainer();
            var mock = Substitute.For<UnifiedSystem<AComponent>>();
            container.AddSystem(mock);
            container.Init();
            var entity = (LazyEcsEntity)container.CreateNewEntity();
            entity.AddComponent(new AComponent());
            container.Run();//LazyECS updates components on the next frame
            Assert.IsTrue(container.ECSManager.EntityContainer.HasComponent(entity.Entity, typeof(LazyEcsComponent<AComponent>)));
            ref var component = ref entity.GetComponent<AComponent>();
            Assert.IsNotNull(component);
        }//
    }
}