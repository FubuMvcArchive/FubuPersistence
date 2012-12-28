using System;
using FubuPersistence;

namespace FubuPersistenceHarness.Domain
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}