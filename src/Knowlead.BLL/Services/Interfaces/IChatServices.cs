using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace Knowlead.Services.Interfaces
{
    public interface IChatServices
    {
        Task<TableResult> SendChatMessage(Guid senderId, Guid sendToId, String message);
    }
}
