using Knowlead.DAL;
using Microsoft.AspNetCore.Mvc;
using Knowlead.DomainModel.LookupModels.Geo;
using Knowlead.Services.Interfaces;
using System.Collections.Generic;
using System;
using Knowlead.Common.HttpRequestItems;
using System.Threading.Tasks;
using System.Linq;
using Knowlead.Common;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ApplicationDbContext _context;
        private readonly Auth _auth;
        private readonly INotificationServices _service;
        private readonly IBlobServices _blobs;
        
        
        public ValuesController(ApplicationDbContext context,
                                Auth auth,
                                IBlobServices blobs,
                                INotificationServices service)
        {
            _context = context;
            _auth = auth;
            _blobs = blobs;
            _service = service;
        }
        
        // GET api/values
        [HttpGet]
        public State Get()
        {
            var State = _context.States.Where(x => x.Name == "Vogosca").FirstOrDefault();
            return State;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            await _service.NotifyMore(new List<Guid>(){_auth.GetUserId()}, Constants.NotificationCodes.NewP2PComment, DateTime.UtcNow.AddSeconds(id));
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public void AA()
        {
            Console.WriteLine("Dobar ");
        }
    }
}
