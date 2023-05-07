using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Undine.Core;
using Undine.LazyECS.Tests.Components;

namespace Undine.LazyECS.Tests
{
    [TestClass]
    public class LazyEcsContainerTests
    {
        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public void EntityCanBeCreated()
        {
            var container = new LazyEcsContainer();
            var entity = container.CreateNewEntity();
            Assert.IsNotNull(entity);
        }

        [TestMethod]
        public void OneTypeSystemCanBeAdded()
        {
            var container = new LazyEcsContainer();
            var mock = new Mock<UnifiedSystem<AComponent>>();
            container.AddSystem(mock.Object);
        }

        [TestMethod]
        public void TwoTypeSystemCanBeAdded()
        {
            var mock = new Mock<UnifiedSystem<AComponent, BComponent>>();
            var container = new LazyEcsContainer();
            container.AddSystem(mock.Object);
        }

        [TestMethod]
        public void ThreeTypeSystemCanBeAdded()
        {
            var mock = new Mock<UnifiedSystem<AComponent, BComponent, CComponent>>();
            var container = new LazyEcsContainer();
            container.AddSystem(mock.Object);
        }

        [TestMethod]
        public void FourTypeSystemCanBeAdded()
        {
            var mock = new Mock<UnifiedSystem<AComponent, BComponent, CComponent, DComponent>>();
            var container = new LazyEcsContainer();
            container.AddSystem(mock.Object);
        }

        [TestMethod]
        public void OneTypeSystemCanBeRetrieved()
        {
            var mock = new Mock<UnifiedSystem<AComponent>>();
            var container = new LazyEcsContainer();
            container.GetSystem(mock.Object);
        }

        [TestMethod]
        public void TwoTypeSystemCanBeRetrieved()
        {
            var mock = new Mock<UnifiedSystem<AComponent, BComponent>>();
            var container = new LazyEcsContainer();
            container.GetSystem(mock.Object);
        }

        [TestMethod]
        public void ThreeTypeSystemCanBeRetrieved()
        {
            var mock = new Mock<UnifiedSystem<AComponent, BComponent, CComponent>>();
            var container = new LazyEcsContainer();
            container.GetSystem(mock.Object);
        }

        [TestMethod]
        public void FourTypeSystemCanBeRetrieved()
        {
            var mock = new Mock<UnifiedSystem<AComponent, BComponent, CComponent, DComponent>>();
            var container = new LazyEcsContainer();
            container.GetSystem(mock.Object);
        }
    }
}