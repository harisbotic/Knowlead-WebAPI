using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.BlobModels;

namespace Knowlead.DTO.LookupModels.Core
{
    public class AchievementModel : _CoreLookupModel
    {
        [MyRequired]
        public string Desc { get; set; }
        public Guid ImageBlobId { get; set; }
        public ImageBlobModel ImageBlob { get; set; }
    }
}