using System;
using System.Net;
using System.Net.Sockets;
using System.Web;
using FubuPersistence.RavenDb;
using Raven.Client;
using Raven.Client.Embedded;

namespace FubuPersistenceHarness
{
    public class SimpleEmbeddedRavenDbPortConfigurationAction : IDocumentStoreConfigurationAction
    {
        public void Configure(IDocumentStore documentStore)
        {
            var store = documentStore as EmbeddableDocumentStore;
            if (store == null) return;

            var currentPort = findInitialPort();
            var maxPort = currentPort + 5;

            while (currentPort < maxPort)
            {
                var port = currentPort++;
                if (!isPortAvailable(port)) continue;

                store.Configuration.Port = port;
                break;
            }
        }

        private static int findInitialPort()
        {
            var httpContext = HttpContext.Current;
            var url = httpContext != null ? httpContext.Request.Url : null;
            return url != null ? url.Port + 1 : 8080;
        }

        private static bool isPortAvailable(int port)
        {
            try
            {
                var endpoint = new IPEndPoint(IPAddress.Any, port);
                var listener = new TcpListener(endpoint);
                listener.Start();
                listener.Stop();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}