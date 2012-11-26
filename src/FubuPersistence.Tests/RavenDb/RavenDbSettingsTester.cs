using FubuPersistence.RavenDb;
using NUnit.Framework;
using FubuTestingSupport;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Database.Extensions;

namespace FubuPersistence.Tests.RavenDb
{
    [TestFixture]
    public class RavenDbSettingsTester
    {
        [Test]
        public void build_in_memory()
        {
            var settings = new RavenDbSettings
            {
                RunInMemory = true
            };

            settings.Create().ShouldBeOfType<EmbeddableDocumentStore>()
                .RunInMemory.ShouldBeTrue();
        }

        [Test]
        public void build_with_data_directory_and_no_url()
        {
            new RavenDbSettings
            {
                DataDirectory = "data"
            }.Create()
             .ShouldBeOfType<EmbeddableDocumentStore>()
             .DataDirectory.ShouldEqual("data".ToFullPath());
        }

        [Test]
        public void build_with_url()
        {
            new RavenDbSettings
            {
                Url = "http://somewhere:8080"
            }.Create()
             .ShouldBeOfType<DocumentStore>()
             .Url.ShouldEqual("http://somewhere:8080");
        }
    }
}