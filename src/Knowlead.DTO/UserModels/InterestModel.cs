using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.LookupModels.Core;

namespace Knowlead.DTO.UserModels
{
    public class InterestModel
    {
        [MyRequired]
        public int FosId { get; set; }
        public FOSModel Fos { get; set; }

        [Range(0,5)]
        public int Stars { get; set; }
    }
}