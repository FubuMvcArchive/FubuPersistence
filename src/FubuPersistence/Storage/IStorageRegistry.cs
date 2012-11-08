namespace FubuPersistence.Storage
{
    // TODO -- rename to StorageFactory
    public interface IStorageRegistry
    {
        IEntityStorage<T> StorageFor<T>() where T : class, IEntity;
    }
}