using System.ComponentModel.DataAnnotations;
using Knowlead.DTO.CoreModels;

namespace Knowlead.DTO.LookupModels.Core
{
    public class AchievementModel : _CoreLookupModel
    {
        [Required]
        public string Desc { get; set; }
        public int ImageId { get; set; }
        public ImageModel Image { get; set; }
    }
}