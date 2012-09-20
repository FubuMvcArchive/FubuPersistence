using System.Collections.Generic;

namespace FubuPersistence.InMemory
{
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