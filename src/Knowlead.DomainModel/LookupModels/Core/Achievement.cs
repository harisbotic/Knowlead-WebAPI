using System.ComponentModel.DataAnnotations;
using Knowlead.DomainModel.CoreModels;

namespace Knowlead.DomainModel.LookupModels.Core
{
    public class Achievement : _CoreLookup
    {
        [Required]
        public string Desc { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}