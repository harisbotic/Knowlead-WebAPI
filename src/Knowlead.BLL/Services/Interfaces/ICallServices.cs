using System;
using System.Threading.Tasks;
using Knowlead.DTO.CallModels;
using Knowlead.DTO.ChatModels;

namespace Knowlead.Services.Interfaces
{
    public interface ICallServices
    {
        Task StartCall(_CallModel callModel);
        _CallModel GetCall(Guid callModelId);
        PeerInfoModel GetPeer(_CallModel callModel, Guid userId);
        Task CloseCall(_CallModel callModel, string reason);
        Task CallModelUpdate(_CallModel callModel, bool reset);
        Task<bool> RemoveConnectionFromCall(String connectionId);
        _CallModel GetCallForUser(Guid applicationUserId);
        Task AcceptCall(_CallModel callModel, PeerInfoModel peerInfoModel);
        Task SendInvitations(Guid callId);
        Task DisconnectFromCall(_CallModel callModel, Guid userId);
        void CallMsg(ChatMessageModel message);
    }
}
