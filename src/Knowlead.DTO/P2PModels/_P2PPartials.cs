using System;
using Knowlead.Common.DataAnnotations;

namespace Knowlead.DTO.P2PModels
{
    public class P2PScheduleModel
    {
        [MyRequired]
        public int P2pId { get; set; }
        public DateTime? ScheduleTime { get; set; }
        public Guid? ScheduleWithId{ get; set; }
    }
}