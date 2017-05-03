using Knowlead.Auth.Hax;

namespace Knowlead.Auth.IdentityServer
{
    public class KnowleadClientStore
    {
        private readonly ApplicationDbContext _context;

        public KnowleadClientStore(ApplicationDbContext context)
        {
            _context = context;
        }

        // public Task<Client> FindClientByIdAsync(string clientId)
        // {
        //     if(clientId == "knowlead_web")
        //     {
        //         return GetKnowleadWebClient();
        //     }
        // }
    }
}