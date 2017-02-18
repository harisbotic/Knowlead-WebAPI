using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Exceptions;
using Knowlead.DAL;
using Knowlead.DomainModel.CallModels;
using static Knowlead.Common.Constants;

namespace Knowlead.BLL.Repositories
{
    public class CallRepository : ICallRepository
    {
        private readonly ApplicationDbContext _context;

        public CallRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(_Call call)
        {
            _context.Add(call);
        }

        public async Task Commit()
        {
            var success = await _context.SaveChangesAsync() > 0;
            if (!success)
                throw new ErrorModelException(ErrorCodes.DatabaseError); //No changed were made to entity
        }
    }
}