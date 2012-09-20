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

        private ICompleteReset theCompleteReset
        {
            get { return theContainer.GetInstance<ICompleteReset>(); }
        }

        [Test]
        public void can_persist_and_retrieve_an_entities()
        {
            var entity = new FakeEntity {Id = Guid.NewGuid()};

            theRepository.Update(entity);

            theRepository.Find<FakeEntity>(entity.Id).ShouldBeTheSameAs(entity);
        }

        [Test]
        public void can_wipe_and_replace()
        {
            var oldEntity = new FakeEntity { Id = Guid.NewGuid() };
            
            theRepository.Update(oldEntity);

            var newEntity = new FakeEntity { Id = Guid.NewGuid() };

            theCompleteReset.ResetState(new IEntity[] { newEntity });

            theRepository.Find<FakeEntity>(oldEntity.Id).ShouldBeNull();
            theRepository.Find<FakeEntity>(newEntity.Id).ShouldBeTheSameAs(newEntity);
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