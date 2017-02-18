using System.Threading.Tasks;
using Knowlead.DomainModel.CallModels;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface ICallRepository
    {
        void Add (_Call call);
        Task Commit ();
    }
}