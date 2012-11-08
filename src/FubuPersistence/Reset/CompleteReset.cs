using System.Collections.Generic;
using FubuCore;
using FubuCore.Logging;
using System.Linq;

namespace FubuPersistence.Reset
{
    public class CompleteReset : ICompleteReset
    {
        private readonly ILogger _logger;
        private readonly IServiceLocator _services;
        private readonly IPersistenceReset _persistence;
        private readonly IEnumerable<IServiceReset> _serviceResets;

        public CompleteReset(ILogger logger, IServiceLocator services, IPersistenceReset persistence, IEnumerable<IServiceReset> serviceResets)
        {
            _logger = logger;
            _services = services;
            _persistence = persistence;
            _serviceResets = serviceResets;
        }

        private void trace(string text, params object[] args)
        {
            if (args.Any())
            {
                _logger.DebugMessage(() => new ResetMessage{Message = text.ToFormat(args)});
            }
            else
            {
                _logger.DebugMessage(() => new ResetMessage { Message = text });
            }
        }

        public void ResetState()
        {
            _serviceResets.Each(x => {
                trace("Stopping services with {0}", x.GetType().Name);
                x.Stop();
            });

            trace("Clearing persisted state");
            _persistence.ClearPersistedState();

            trace("Loading initial data");
            _services.GetInstance<IInitialState>().Load();

            _serviceResets.Each(x => {
                
                trace("Starting services with {0}", x.GetType().Name);
                x.Start();
            });
        }

        public void CommitChanges()
        {
            trace("Committing changes");
            _persistence.CommitAllChanges();
        }
    }
}