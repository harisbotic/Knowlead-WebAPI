using System;

namespace Knowlead.Services.Interfaces
{
    public interface IBlobServices
    {
        void SaveImageToLocalStorage(String filename, byte[] image);
        void SaveFileToLocalStorage(String filename, byte[] file);
    }
}
