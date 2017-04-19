namespace Knowlead.DomainModel.BlobModels
{
    public class ImageBlob : _Blob
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public ImageBlob() : base("Image")
        {
        }
    }
}

