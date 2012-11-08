using System;
using System.Collections.Generic;
using FubuCore;

namespace FubuPersistence
{
    [MarkedForTermination, Obsolete]
    public interface IReset
    {
        void WipeAndReplace(IEnumerable<IEntity> entities);
    }
}