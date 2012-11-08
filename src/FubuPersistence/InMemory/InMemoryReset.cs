using System;
using System.Collections.Generic;
using FubuCore;

namespace FubuPersistence.InMemory
{
    [MarkedForTermination, Obsolete]
    public class InMemoryReset : IReset
    {
        private readonly InMemoryPersistor _persistor;

        public InMemoryReset(IPersistor persistor)
        {
            _persistor = (InMemoryPersistor) persistor;
        }

        public void WipeAndReplace(IEnumerable<IEntity> entities)
        {
            _persistor.WipeAndReplace(entities);
        }
    }
}