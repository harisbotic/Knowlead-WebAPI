using System.Collections.Generic;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.LookupModels.Core
{
    public class Language : _CoreLookup
    {
        public ICollection<ApplicationUserLanguage> ApplicationUserLanguages { get; set; }
    }
}