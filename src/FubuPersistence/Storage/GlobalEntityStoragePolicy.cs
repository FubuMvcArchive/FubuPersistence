namespace FubuPersistence.Storage
{
    public class GlobalEntityStoragePolicy : IEntityStoragePolicy
    {
        public bool Matches<T>() where T : class, IEntity
        {
            return true;
        }

        public IEntityStorage<T> BuildStorage<T>(IPersistor persistor) where T : class, IEntity
        {
            return new GlobalEntityStorage<T>(persistor);
        }
    }
}