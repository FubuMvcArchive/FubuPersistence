using System;
using FubuCore.Dates;

namespace FubuPersistence
{
    // All this means is that it has an Id, nothing else
    public interface IEntity
    {
        Guid Id { get; set; }

        // TODO -- move this to a separate entity interface
        Milestone Deleted { get; set; }
    }
}