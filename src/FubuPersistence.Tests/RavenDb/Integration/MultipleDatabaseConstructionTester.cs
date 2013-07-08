using FubuPersistence.RavenDb;
using FubuPersistence.RavenDb.Multiple;
using NUnit.Framework;
using Raven.Client;
using Raven.Client.Document;
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
                x.ConnectToRavenDb<SecondDbSettings>(store => {
                    store.Conventions.DefaultQueryingConsistency = ConsistencyOptions.MonotonicRead;
                });
                x.ConnectToRavenDb<ThirdDbSettings>(store => {
                    store.Conventions.DefaultQueryingConsistency = ConsistencyOptions.QueryYourWrites;
                });

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
        public void respects_the_configuration_per_store_setting_type()
        {
            theContainer.GetInstance<IDocumentStore<SecondDbSettings>>()
                        .Conventions.DefaultQueryingConsistency
                        .ShouldEqual(ConsistencyOptions.MonotonicRead);

            theContainer.GetInstance<IDocumentStore<ThirdDbSettings>>()
                        .Conventions.DefaultQueryingConsistency
                        .ShouldEqual(ConsistencyOptions.QueryYourWrites);
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

        [Test]
        public void default_raven_store_is_identified_as_Default()
        {
            theContainer.GetInstance<IDocumentStore>()
                        .Identifier.ShouldEqual("Default");
        }

        [Test]
        public void other_raven_stores_are_identified_as_the_type()
        {
            theContainer.GetInstance<IDocumentStore<SecondDbSettings>>()
                        .Identifier.ShouldEqual("SecondDbSettings");

            theContainer.GetInstance<IDocumentStore<ThirdDbSettings>>()
                        .Identifier.ShouldEqual("ThirdDbSettings");
        }

    }

    public class SecondDbSettings : RavenDbSettings
    {
        
    }

    public class ThirdDbSettings : RavenDbSettings
    {

    }
}