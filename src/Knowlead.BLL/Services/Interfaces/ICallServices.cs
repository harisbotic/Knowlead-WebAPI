using System;
using Knowlead.DTO.CallModels;
using Knowlead.DTO.ChatModels;

namespace Knowlead.Services.Interfaces
{
    public interface ICallServices
    {
        void StartCall(_CallModel callModel);
        _CallModel GetCall(Guid callModelId);
        PeerInfoModel GetPeer(_CallModel callModel, Guid userId);
        void CloseCall(_CallModel callModel, string reason);
        void CallModelUpdate(_CallModel callModel, bool reset);
        bool RemoveConnectionFromCall(String connectionId);
        _CallModel GetCallForUser(Guid applicationUserId);
        void AcceptCall(_CallModel callModel, PeerInfoModel peerInfoModel);
        void SendInvitations(Guid callId);
        void DisconnectFromCall(_CallModel callModel, Guid userId);
        void CallMsg(ChatMessageModel message);
    }
}
