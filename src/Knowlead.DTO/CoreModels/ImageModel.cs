using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.CoreModels
{
    public class ImageModel
    {
        [MyRequired]
        public Guid Filename { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        [MyRequired]
        public Guid UploadedById { get; set; }
        public ApplicationUserModel UploadedBy { get; set; }
    }
}

