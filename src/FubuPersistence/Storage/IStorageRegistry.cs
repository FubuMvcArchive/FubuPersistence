namespace FubuPersistence.Storage
{
    public interface IStorageRegistry
    {
        IEntityStorage<T> StorageFor<T>() where T : class, IEntity;
    }
}