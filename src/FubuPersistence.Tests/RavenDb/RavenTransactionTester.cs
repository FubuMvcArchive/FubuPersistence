using System.Linq;
using FubuPersistence.RavenDb;
using FubuTestingSupport;
using NUnit.Framework;
using StructureMap;

namespace FubuPersistence.Tests.RavenDb
{
    [TestFixture]
    public class RavenTransactionTester
    {
        private Container container;
        private ITransaction transaction;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            container = new Container(new RavenDbRegistry());
            container.Inject(new RavenDbSettings
            {
                RunInMemory = true
            });
        }

        [SetUp]
        public void SetUp()
        {
            transaction = container.GetInstance<ITransaction>();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            container.Dispose();
        }

        [Test]
        public void load_all()
        {
            transaction.WithRepository(repo => {
                repo.Update(new User());
                repo.Update(new User());
                repo.Update(new User());
                repo.Update(new OtherEntity());
                repo.Update(new OtherEntity());
                repo.Update(new ThirdEntity());
            });

            bool wasCalled = false;

            transaction.WithRepository(repo => {
                repo.All<User>().Count().ShouldEqual(3);
                repo.All<OtherEntity>().Count().ShouldEqual(2);
                repo.All<ThirdEntity>().Count().ShouldEqual(1);

                wasCalled = true;
            });

            wasCalled.ShouldBeTrue();

        }

        [Test]
        public void persist()
        {
            var entity = new OtherEntity();

            transaction.WithRepository(repo => repo.Update(entity));


            bool wasCalled = false;
            transaction.WithRepository(repo => {
                repo.All<OtherEntity>().ShouldContain(entity);
                wasCalled = true;
            });

            wasCalled.ShouldBeTrue();
        }


    }
}