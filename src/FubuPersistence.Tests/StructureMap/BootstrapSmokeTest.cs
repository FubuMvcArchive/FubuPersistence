using System;
using FubuPersistence.StructureMap;
using FubuTestingSupport;
using NUnit.Framework;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace FubuPersistence.Tests.StructureMap
{
    [TestFixture]
    public class BootstrapSmokeTest
    {
        [Test]
        public void can_persist_and_retrieve()
        {
            var container = new Container(x => x.AddRegistry<TestRegistry>());

            var entity = new FakeEntity {Id = Guid.NewGuid()};
            container.GetInstance<IEntityRepository>().Update(entity);

            container.GetInstance<IEntityRepository>().Find<FakeEntity>(entity.Id).ShouldBeTheSameAs(entity);
        }

        public class TestRegistry : Registry
        {
            public TestRegistry()
            {
                this.FubuPersistenceInMemory();
            }
        }
    }
}