using System.Collections.Generic;
using StructureMap;

namespace FubuPersistence.StructureMap
{
    public class CompleteReset : ICompleteReset
    {
        private readonly IContainer _container;
        private readonly IReset _reset;

        public CompleteReset(IContainer container, IReset reset)
        {
            _container = container;
            _reset = reset;
        }

        public void ResetState(IEnumerable<IEntity> entities)
        {
            Shutdown();

            _reset.WipeAndReplace(entities);
        }

        public void Shutdown()
        {
            eject<IEntityRepository>();
        }

        private void eject<T>()
        {
            var instance = _container.Model.For<T>();
            if (instance != null && instance.Default != null)
            {
                instance.Default.EjectObject();
            }
        }
    }
}