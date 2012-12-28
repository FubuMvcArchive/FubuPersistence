using System;
using FubuPersistence.RavenDb;
using FubuTestingSupport;
using NUnit.Framework;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Database.Extensions;

namespace FubuPersistence.Tests.RavenDb
{
    [TestFixture]
    public class RavenDbSettingsTester
    {
        [Test]
        public void build_empty_throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RavenDbSettings().Create());
        }

        [Test]
        public void build_in_memory()
        {
            var store = createStore<EmbeddableDocumentStore>(x => x.RunInMemory = true);
            store.RunInMemory.ShouldBeTrue();
            store.UseEmbeddedHttpServer.ShouldBeFalse();
        }

        [Test]
        public void build_using_embedded_http_server_in_memory()
        {
            var store = createStore<EmbeddableDocumentStore>(x =>
            {
                x.RunInMemory = true;
                x.UseEmbeddedHttpServer = true;
            });
            store.RunInMemory.ShouldBeTrue();
            store.UseEmbeddedHttpServer.ShouldBeTrue();
        }

        [Test]
        public void build_using_embedded_http_server_with_data_directory()
        {
            var store = createStore<EmbeddableDocumentStore>(x =>
            {
                x.DataDirectory = "data".ToFullPath();
                x.UseEmbeddedHttpServer = true;
            });
            store.DataDirectory.ShouldEqual("data".ToFullPath());
            store.UseEmbeddedHttpServer.ShouldBeTrue();
        }

        [Test]
        public void build_with_data_directory()
        {
            var store = createStore<EmbeddableDocumentStore>(x => x.DataDirectory = "data".ToFullPath());
            store.DataDirectory.ShouldEqual("data".ToFullPath());
            store.UseEmbeddedHttpServer.ShouldBeFalse();
        }

        [Test]
        public void build_with_url()
        {
            var store = createStore<DocumentStore>(x => x.Url = "http://somewhere:8080");
            store.Url.ShouldEqual("http://somewhere:8080");
        }

        [Test]
        public void is_empty()
        {
            new RavenDbSettings().IsEmpty().ShouldBeTrue();

            new RavenDbSettings
            {
                RunInMemory = true
            }.IsEmpty().ShouldBeFalse();

            new RavenDbSettings
            {
                DataDirectory = "data"
            }.IsEmpty().ShouldBeFalse();

            new RavenDbSettings
            {
                Url = "http://server.com"
            }.IsEmpty().ShouldBeFalse();
        }

        private T createStore<T>(Action<RavenDbSettings> setup) where T : IDocumentStore
        {
            var settings = new RavenDbSettings();
            if (setup != null) setup(settings);
            return settings.Create().ShouldBeOfType<T>();
        }
    }
}