using System;
using System.Linq;
using System.Linq.Expressions;
using FubuCore.Dates;
using FubuCore.Util;
using FubuPersistence.Storage;

namespace FubuPersistence
{
    public class EntityRepository : IEntityRepository
    {
        private readonly IStorageFactory _storageFactory;
        private readonly Cache<Type, object> _storageProviders = new Cache<Type, object>();
        private readonly ISystemTime _systemTime;

        public EntityRepository(IStorageFactory storageFactory, ISystemTime systemTime)
        {
            _storageFactory = storageFactory;
            _systemTime = systemTime;
        }

        public IQueryable<T> All<T>() where T : class, IEntity
        {
            return storage<T>().All().Where(x => x.Deleted == null);
        }


        public T FindWhere<T>(Expression<Func<T, bool>> filter) where T : class, IEntity
        {
            var raw = storage<T>().All().FirstOrDefault(filter);
            return raw == null ? null : Find<T>(raw.Id);
        }

        public T Find<T>(Guid id) where T : class, IEntity
        {
            return storage<T>().Find(id);
        }

        // virtual for testing
        public void Update<T>(T model) where T : class, IEntity
        {
            if (model.Id == default(Guid) || model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
            }

            storage<T>().Update(model);
        }

        public void Remove<T>(T model) where T : class, IEntity
        {
            model.Deleted = new Milestone(_systemTime.UtcNow());

            Update(model);
        }

        public void DeleteAll<T>() where T : class, IEntity
        {
            storage<T>().DeleteAll();
        }

        private IEntityStorage<T> storage<T>() where T : class, IEntity
        {
            _storageProviders.Fill(typeof(T), t => _storageFactory.StorageFor<T>());

            return (IEntityStorage<T>)_storageProviders[typeof(T)];
        }

        public static EntityRepository InMemory()
        {
            return InMemory(x => { });
        }

        public static EntityRepository InMemory(Action<EntityRepositoryExpression> configure)
        {
            var expression = new EntityRepositoryExpression();
            configure(expression);

            return expression.Build();
        }
    }
}