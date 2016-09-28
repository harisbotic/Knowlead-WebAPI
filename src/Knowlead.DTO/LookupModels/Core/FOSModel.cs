using System.ComponentModel.DataAnnotations;

namespace Knowlead.DTO.LookupModels.Core
{
    public class FOSModel : _CoreLookupModel
    {
        [Required]
        public string FosDesc { get; set; }
    }
}