using System;
using System.IO;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace Knowlead.Services
{
    public class BlobServices : IBlobServices
    {
        private const string IMAGES_FOLDER = "IMAGES";
        private const string FILES_FOLDER = "FILES";
        private const string LS_FOLDER = "LOCAL_STORAGE";
        private readonly IHostingEnvironment _hostingEnvironment;

        public BlobServices(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public void SaveImageToLocalStorage(String filename, byte[] image)
        {
            var folderPath = $"{_hostingEnvironment.WebRootPath}/{LS_FOLDER}/{IMAGES_FOLDER}/";

            SaveToLocalStorage(folderPath, filename, image);
        }

        public void SaveFileToLocalStorage(String filename, byte[] file)
        {
            var folderPath = $"{_hostingEnvironment.WebRootPath}/{LS_FOLDER}/{FILES_FOLDER}/";

            SaveToLocalStorage(folderPath, filename, file);
        }

        private void SaveToLocalStorage(String folderPath, String filename, byte[] file)
        {
            Directory.CreateDirectory(folderPath);

            File.WriteAllBytes($"{folderPath}{filename}", file);
        }
    }
}