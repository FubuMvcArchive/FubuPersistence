using System.Collections.Generic;

namespace FubuPersistence
{
    public interface ICompleteReset
    {
        void ResetState(IEnumerable<IEntity> entities);
        void Shutdown();
    }
}