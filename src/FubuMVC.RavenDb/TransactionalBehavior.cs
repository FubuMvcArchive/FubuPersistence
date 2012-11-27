using System;
using FubuMVC.Core.Behaviors;
using FubuPersistence.RavenDb;

namespace FubuMVC.RavenDb
{
    public class TransactionalBehavior : WrappingBehavior
    {
        private readonly ISessionBoundary _session;

        public TransactionalBehavior(ISessionBoundary session)
        {
            _session = session;
        }

        protected override void invoke(Action action)
        {
            action();
            _session.SaveChanges();
        }
    }
}