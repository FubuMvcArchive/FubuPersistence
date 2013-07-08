using System;
using StructureMap;
using StructureMap.Pipeline;

namespace FubuPersistence.RavenDb.Multiple
{
    public class DocumentStoreInstance<T> : Instance where T : RavenDbSettings
    {
        protected override string getDescription()
        {
            return "Builds the IDocumentStore for settings class " + typeof (T).Name;
        }

        protected override object build(Type pluginType, BuildSession session)
        {
            var inner = session.GetInstance<DocumentStoreBuilder<T>>().Build();
            return new DocumentStore<T>(inner);
        }
    }
}