using StructureMap.Configuration.DSL;

namespace FubuPersistence.StructureMap
{
    public static class FubuPersistenceRegistryExtensions
    {
         public static void FubuPersistenceInMemory(this Registry registry)
         {
             registry.IncludeRegistry<InMemoryRegistry>();
         }
    }
}