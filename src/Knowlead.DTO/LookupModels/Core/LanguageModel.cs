using System.Collections.Generic;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.LookupModels.Core
{
    public class LanguageModel : _CoreLookupModel
    {
        public ICollection<ApplicationUserLanguageModel> ApplicationUserLanguages { get; set; }
    }
}