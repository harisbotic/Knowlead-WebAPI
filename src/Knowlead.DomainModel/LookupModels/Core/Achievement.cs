using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.BlobModels;

namespace Knowlead.DomainModel.LookupModels.Core
{
    public class Achievement : _CoreLookup
    {
        [MyRequired]
        public string Desc { get; set; }
        public Guid ImageBlobId { get; set; }
        public ImageBlob ImageBlob { get; set; }
    }
}