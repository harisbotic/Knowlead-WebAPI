using Knowlead.BLL.Interfaces;
using Knowlead.DAL;

namespace Knowlead.BLL
{
    public class P2PRepository : IP2PRepository
    {
        private ApplicationDbContext _context;

        public P2PRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}