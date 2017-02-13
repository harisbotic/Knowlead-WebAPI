using System.Linq;
using Knowlead.DomainModel.P2PModels;
using Microsoft.EntityFrameworkCore;

namespace Knowlead.BLL.QueryExtensions
{
    public  static  class P2PQueryExtensions
    {
        public static IQueryable<P2P> IncludeEverything(this IQueryable<P2P> query)
        {
            return query.Include(x => x.P2PLanguages)
                        .Include(x => x.P2PFiles)
                            .ThenInclude(x => x.FileBlob)
                        .Include(x => x.P2PImages)
                            .ThenInclude(x => x.ImageBlob);
                        
        }
    }
}
