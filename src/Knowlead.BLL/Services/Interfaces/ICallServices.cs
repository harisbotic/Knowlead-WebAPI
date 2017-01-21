using System;
using Knowlead.DTO.CallModels;

namespace Knowlead.Services.Interfaces
{
    public interface ICallServices
    {
        void StartCall(_CallModel callModel);
        _CallModel GetCall(Guid callModelId);
        PeerInfoModel GetPeer(_CallModel callModel, Guid userId);
        void CloseCall(_CallModel callModel);
        void CallModelUpdate(_CallModel callModel, bool reset);
        bool RemoveConnectionFromCall(String connectionId);
    }
}
