using System.Collections.Generic;

namespace FubuPersistence
{
    public interface IReset
    {
        void WipeAndReplace(IEnumerable<IEntity> entities);
    }
}