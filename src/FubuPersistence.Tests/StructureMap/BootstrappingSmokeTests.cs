using System;
using FubuPersistence.StructureMap;
using FubuTestingSupport;
using NUnit.Framework;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace FubuPersistence.Tests.StructureMap
{
    [TestFixture]
    public class BootstrappingSmokeTests
    {
        private IContainer theContainer;

        [SetUp]
        public void SetUp()
        {
            theContainer = new Container(x => x.AddRegistry<TestRegistry>());
        }

        private IEntityRepository theRepository
        {
            get { return theContainer.GetInstance<IEntityRepository>(); }
        }


        [Test]
        public void can_persist_and_retrieve_an_entities()
        {
            var entity = new FakeEntity {Id = Guid.NewGuid()};

            theRepository.Update(entity);

            theRepository.Find<FakeEntity>(entity.Id).ShouldBeTheSameAs(entity);
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