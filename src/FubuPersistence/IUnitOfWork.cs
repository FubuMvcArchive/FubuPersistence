namespace FubuPersistence
{
    public interface IUnitOfWork
    {
        void StartTransaction();
        void Commit();
        void RejectChanges();
    }
}