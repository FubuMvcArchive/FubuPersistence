using System;
using FubuPersistence.Storage;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuPersistence.Tests
{
    [TestFixture]
    public class when_updating_an_entity : InteractionContext<EntityRepository>
    {
        private FakeEntity theEntity;
        private IEntityStorage<FakeEntity> theStorage;

        protected override void beforeEach()
        {
            theEntity = new FakeEntity();

            theStorage = MockFor<IEntityStorage<FakeEntity>>();
            MockFor<IStorageFactory>().Stub(x => x.StorageFor<FakeEntity>()).Return(
                theStorage);

            LocalSystemTime = DateTime.Now;
        }

        [Test]
        public void should_update()
        {
            ClassUnderTest.Update(theEntity);
            theStorage.AssertWasCalled(x => x.Update(theEntity));
        }

        [Test]
        public void removing_still_needs_to_update()
        {
            ClassUnderTest.Remove(theEntity);
            theStorage.AssertWasCalled(x => x.Update(theEntity));
        }

    }
}