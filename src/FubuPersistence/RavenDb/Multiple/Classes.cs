using System;
using System.Collections.Generic;
using StructureMap;
using StructureMap.Pipeline;

namespace FubuPersistence.RavenDb.Multiple
{
    public class DocumentStoreBuilder<T> : DocumentStoreBuilder where T : RavenDbSettings
    {
        public DocumentStoreBuilder(T settings, IEnumerable<IDocumentStoreConfigurationAction<T>> configurations) : base(settings, configurations)
        {
        }
    }

    public class DocumentStoreMasterInstance : Instance
    {
        protected override string getDescription()
        {
            return "Knows how to build an IDocumentStore per different Database settings";
        }

        protected override object build(Type pluginType, BuildSession session)
        {
            throw new NotSupportedException("Cannot build IDocumentStore with the open generic type");
        }
    }

    public class DocumentStoreInstance<T> : Instance where T : RavenDbSettings
    {
        protected override string getDescription()
        {
            return "Builds the IDocumentStore for settings class " + typeof (T).Name;
        }

        protected override object build(Type pluginType, BuildSession session)
        {
            return session.GetInstance<DocumentStoreBuilder<T>>().Build();
        }
    }
}