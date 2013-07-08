using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Client.Linq;

namespace FubuPersistence.RavenDb.Multiple
{
    public class DocumentSession<TSettings> : IDocumentSession<TSettings> where TSettings : RavenDbSettings
    {
        private readonly IDocumentSession _inner;

        public DocumentSession(ISessionBoundary boundary)
        {
            _inner = boundary.Session<TSettings>();
        }


        public void Dispose()
        {
            _inner.Dispose();
        }

        public void Delete<T>(T entity)
        {
            _inner.Delete(entity);
        }

        public T Load<T>(string id)
        {
            return _inner.Load<T>(id);
        }

        public T[] Load<T>(params string[] ids)
        {
            return _inner.Load<T>(ids);
        }

        public T[] Load<T>(IEnumerable<string> ids)
        {
            return _inner.Load<T>(ids);
        }

        public T Load<T>(ValueType id)
        {
            return _inner.Load<T>(id);
        }

        public T[] Load<T>(params ValueType[] ids)
        {
            return _inner.Load<T>(ids);
        }

        public T[] Load<T>(IEnumerable<ValueType> ids)
        {
            return _inner.Load<T>(ids);
        }

        public IRavenQueryable<T> Query<T>(string indexName)
        {
            return _inner.Query<T>(indexName);
        }

        public IRavenQueryable<T> Query<T>()
        {
            return _inner.Query<T>();
        }

        public IRavenQueryable<T> Query<T, TIndexCreator>() where TIndexCreator : AbstractIndexCreationTask, new()
        {
            return _inner.Query<T, TIndexCreator>();
        }

        public ILoaderWithInclude<object> Include(string path)
        {
            return _inner.Include(path);
        }

        public ILoaderWithInclude<T> Include<T>(Expression<Func<T, object>> path)
        {
            return _inner.Include<T>(path);
        }

        public ILoaderWithInclude<T> Include<T, TInclude>(Expression<Func<T, object>> path)
        {
            return _inner.Include<T, TInclude>(path);
        }

        public void SaveChanges()
        {
            _inner.SaveChanges();
        }

        public void Store(object entity, Guid etag)
        {
            _inner.Store(entity, etag);
        }

        public void Store(object entity, Guid etag, string id)
        {
            _inner.Store(entity, etag, id);
        }

        public void Store(dynamic entity)
        {
            _inner.Store(entity);
        }

        public void Store(dynamic entity, string id)
        {
            _inner.Store(entity, id);
        }

        public ISyncAdvancedSessionOperation Advanced { get { return _inner.Advanced; } }
    }
}