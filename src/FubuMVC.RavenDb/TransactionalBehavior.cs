using System;
using System.Threading;
using FubuCore;
using FubuCore.Logging;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Http;
using FubuPersistence.RavenDb;
using Raven.Client;

namespace FubuMVC.RavenDb
{
    public class TransactionalBehavior : WrappingBehavior
    {
        private readonly ISessionBoundary _session;
        private readonly ILogger _logger;
        private readonly IHttpRequest _httpRequest;

        public TransactionalBehavior(ISessionBoundary session, ILogger logger, IHttpRequest httpRequest)
        {
            _session = session;
            _logger = logger;
            _httpRequest = httpRequest;
        }


        protected override void invoke(Action action)
        {
            action();
            _session.WithOpenSession(x => _logger.DebugMessage(() => RavenSessionLogMessage.For(x, _httpRequest)));
            _session.SaveChanges();
        }
    }

    public class RavenSessionLogMessage : LogRecord
    {
        public string Url { get; set; }
        public int Requests { get; set; }
        public string UserName { get; set; }

        public static RavenSessionLogMessage For(IDocumentSession session, IHttpRequest request)
        {
            var userName = Thread.CurrentPrincipal.IfNotNull(x => x.Identity.Name);
            var requests = session.Advanced.NumberOfRequests;
            var url = request.FullUrl();
            return new RavenSessionLogMessage
            {
                UserName = userName,
                Requests = requests,
                Url = url
            };
        }

        public override string ToString()
        {
            return string.Format("Raven Session Usage: Url: {0}, Requests: {1}, UserName: {2}", Url, Requests, UserName);
        }
    }
}