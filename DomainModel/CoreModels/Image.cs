using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.CoreModels
{
    public class Image
    {
        public int ImageId { get; set; }
        public string Filename { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Guid UploadedById { get; set; }
        public ApplicationUser UploadedBy { get; set; }
    }
}

