using System.Linq;
using FubuPersistence.RavenDb;
using FubuPersistence.Reset;
using FubuTestingSupport;
using NUnit.Framework;
using StructureMap;

namespace FubuPersistence.Tests.RavenDb
{
    [TestFixture]
    public class RavenPersistenceResetTester
    {
        private Container container;
        private RavenUnitOfWork theUnitOfWork;
        private IPersistenceReset theReset;

        [SetUp]
        public void SetUp()
        {
            container = new Container(new RavenDbRegistry());
            container.Inject(new RavenDbSettings { RunInMemory = true });

            theReset = container.GetInstance<IPersistenceReset>();
            theUnitOfWork = new RavenUnitOfWork(container);
        }

        [TearDown]
        public void TearDown()
        {
            container.Dispose();
        }

        [Test]
        public void reset_wipes_the_slate_clean()
        {
            var repo = theUnitOfWork.Start();

            repo.Update(new User());
            repo.Update(new User());
            repo.Update(new User());
            repo.Update(new OtherEntity());
            repo.Update(new OtherEntity());
            repo.Update(new ThirdEntity());

            theUnitOfWork.Commit();

            theReset.ClearPersistedState();

            repo = theUnitOfWork.Start();

            repo.All<User>().Count().ShouldEqual(0);
            repo.All<OtherEntity>().Count().ShouldEqual(0);
            repo.All<ThirdEntity>().Count().ShouldEqual(0);
        }
    }
}