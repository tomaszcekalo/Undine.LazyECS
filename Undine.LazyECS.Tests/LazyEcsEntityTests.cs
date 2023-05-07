using Moq;
using Undine.Core;
using Undine.LazyECS.Tests.Components;

namespace Undine.LazyECS.Tests
{
    [TestClass]
    public class LazyEcsEntityTests
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

        /*[TestMethod]
        public void ComponentCanBeRetrieved()
        {
            var container = new LazyEcsContainer();
            //var mock = new Mock<UnifiedSystem<AComponent>>();
            //container.AddSystem(mock.Object);
            container.Init();
            var entity = container.CreateNewEntity();
            entity.AddComponent(new AComponent());
            ref var component = ref entity.GetComponent<AComponent>();
            Assert.IsNotNull(component);
        }//*/
    }
}