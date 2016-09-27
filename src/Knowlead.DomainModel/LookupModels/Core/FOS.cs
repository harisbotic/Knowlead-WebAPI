using System.ComponentModel.DataAnnotations;

namespace Knowlead.DomainModel.LookupModels.Core
{
    public class FOS : _CoreLookup
    {
        [Required]
        public string FosDesc { get; set; }
    }
}