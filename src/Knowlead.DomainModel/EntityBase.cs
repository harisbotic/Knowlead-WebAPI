using System;
using Knowlead.Common.DataAnnotations;

namespace Knowlead.DomainModel
{
    public abstract class EntityBase
    {
        [MyRequired]
        public DateTime CreatedAt { get; set; }

        public EntityBase()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}