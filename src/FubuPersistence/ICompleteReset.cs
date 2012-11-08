using System;
using System.Collections.Generic;
using FubuCore;

namespace FubuPersistence
{
    [MarkedForTermination, Obsolete]
    public interface ICompleteReset
    {
        void ResetState(IEnumerable<IEntity> entities);
        void Shutdown();
    }
}