using FubuPersistence.RavenDb;
using FubuPersistence.RavenDb.Multiple;
using NUnit.Framework;
using StructureMap;
using FubuTestingSupport;

namespace FubuPersistence.Tests.RavenDb.Integration
{
    [TestFixture]
    public class MultipleDatabaseConstructionTester
    {
        private Container theContainer;

        [SetUp]
        public void SetUp()
        {
            theContainer = new Container(x => {
                x.ConnectToRavenDb<SecondDbSettings>();
                x.ConnectToRavenDb<ThirdDbSettings>();

                x.IncludeRegistry<RavenDbRegistry>();
                x.For<RavenDbSettings>().Use(RavenDbSettings.InMemory);
                x.For<SecondDbSettings>().Use(new SecondDbSettings {RunInMemory = true});
                x.For<ThirdDbSettings>().Use(new ThirdDbSettings {RunInMemory = true});
            });
        }

        [Test]
        public void can_create_database_store_per_type()
        {
            theContainer.GetInstance<IDocumentStore<SecondDbSettings>>()
                        .ShouldNotBeNull();

            theContainer.GetInstance<IDocumentStore<ThirdDbSettings>>()
                        .ShouldNotBeNull();
        }

        [Test]
        public void document_store_is_singleton()
        {
            theContainer.GetInstance<IDocumentStore<SecondDbSettings>>()
                        .ShouldBeTheSameAs(theContainer.GetInstance<IDocumentStore<SecondDbSettings>>());

            theContainer.GetInstance<IDocumentStore<ThirdDbSettings>>()
                        .ShouldBeTheSameAs(theContainer.GetInstance<IDocumentStore<ThirdDbSettings>>());
        }

        [Test]
        public void can_build_document_session_per_type()
        {
            theContainer.GetInstance<IDocumentSession<SecondDbSettings>>()
                        .ShouldBeOfType<DocumentSession<SecondDbSettings>>();

            theContainer.GetInstance<IDocumentSession<ThirdDbSettings>>()
                        .ShouldBeOfType<DocumentSession<ThirdDbSettings>>();
        }
    }

    public class SecondDbSettings : RavenDbSettings
    {
        
    }

    public class ThirdDbSettings : RavenDbSettings
    {

    }
}