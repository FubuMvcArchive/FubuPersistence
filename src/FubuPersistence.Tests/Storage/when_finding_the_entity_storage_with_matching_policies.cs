using FubuPersistence.Storage;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuPersistence.Tests.Storage
{
    [TestFixture]
    public class when_finding_the_entity_storage_with_matching_policies : InteractionContext<StorageRegistry>
    {
        private IEntityStoragePolicy e1;
        private IEntityStoragePolicy e2;

        protected override void beforeEach()
        {
            e1 = MockRepository.GenerateStub<IEntityStoragePolicy>();
            e2 = MockRepository.GenerateStub<IEntityStoragePolicy>();

            Services.Container.Configure(x =>
                                             {
                                                 x.For<IEntityStoragePolicy>().Add(e1);
                                                 x.For<IEntityStoragePolicy>().Add(e2);
                                             });
        }

        [Test]
        public void the_only_matching_policy_is_used()
        {
            e1.Stub(x => x.Matches<User>()).Return(true);
            e2.Stub(x => x.Matches<User>()).Return(false);

            var storage = MockRepository.GenerateStub<IEntityStorage<User>>();

            e1.Stub(x => x.BuildStorage<User>(MockFor<IPersistor>())).Return(storage);

            ClassUnderTest.StorageFor<User>().ShouldBeTheSameAs(storage);
        }

        [Test]
        public void the_last_matching_policy_is_used()
        {
            e1.Stub(x => x.Matches<User>()).Return(true);
            e2.Stub(x => x.Matches<User>()).Return(true);

            var s1 = MockRepository.GenerateStub<IEntityStorage<User>>();
            var s2 = MockRepository.GenerateStub<IEntityStorage<User>>();

            e1.Stub(x => x.BuildStorage<User>(MockFor<IPersistor>())).Return(s1);
            e2.Stub(x => x.BuildStorage<User>(MockFor<IPersistor>())).Return(s2);

            ClassUnderTest.StorageFor<User>().ShouldBeTheSameAs(s2);
        }
    }
}