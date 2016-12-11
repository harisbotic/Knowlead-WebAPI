using System;
using Knowlead.DTO.CallModels;

namespace Knowlead.Services.Interfaces
{
    public interface ICallServices
    {
        void AddCall(_CallModel callModel);
        _CallModel GetCall(Guid callModelId);
        PeerInfoModel GetPeer(Guid callModelId, Guid userId);
        void CallModelUpdate(_CallModel callModel);
    }
}
