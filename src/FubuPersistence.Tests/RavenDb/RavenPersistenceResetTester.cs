using System.Linq;
using FubuPersistence.RavenDb;
using FubuPersistence.RavenDb.Multiple;
using FubuPersistence.Reset;
using FubuPersistence.Tests.RavenDb.Integration;
using FubuTestingSupport;
using NUnit.Framework;
using StructureMap;
using FubuCore;

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
        public void can_find_other_setting_types()
        {
            container.Inject(new SecondDbSettings());
            container.Inject(new ThirdDbSettings());
            container.Inject(new FourthDbSettings());
        
            theReset.As<RavenPersistenceReset>()
                .FindOtherSettingTypes()
                .ShouldHaveTheSameElementsAs(typeof(SecondDbSettings), typeof(ThirdDbSettings), typeof(FourthDbSettings));
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

[TestFixture]
public class when_clearing_persisted_state_with_multiple_settings
{
    [Test]
    public void ejects_the_store_for_each_and_uses_in_memory_for_each_additional_type_of_setting()
    {
        var theContainer = new Container(x =>
        {
            x.IncludeRegistry<RavenDbRegistry>();
            x.ConnectToRavenDb<SecondDbSettings>();
            x.ConnectToRavenDb<ThirdDbSettings>();

            x.For<SecondDbSettings>().Use(new SecondDbSettings {RunInMemory = true});
            x.For<ThirdDbSettings>().Use(new ThirdDbSettings {RunInMemory = true});
        });

        var store2a = theContainer.GetInstance<IDocumentStore<SecondDbSettings>>();
        var store3a = theContainer.GetInstance<IDocumentStore<ThirdDbSettings>>();
    
        theContainer.GetInstance<IPersistenceReset>().ClearPersistedState();

        theContainer.GetInstance<IDocumentStore<SecondDbSettings>>()
                    .ShouldNotBeTheSameAs(store2a);


        theContainer.GetInstance<IDocumentStore<ThirdDbSettings>>()
                    .ShouldNotBeTheSameAs(store3a);

        var newSettings = theContainer.GetInstance<SecondDbSettings>();
        newSettings
            .RunInMemory.ShouldBeTrue();

        newSettings.Url.ShouldBeNull();
        newSettings.ConnectionString.ShouldBeNull();

        
    }
}