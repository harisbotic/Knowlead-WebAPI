using System;
using Knowlead.Common.DataAnnotations;

namespace Knowlead.DTO
{
    public abstract class EntityBaseModel
    {
        [MyRequired]
        public DateTime CreatedAt { get; set; }

        public EntityBaseModel()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}