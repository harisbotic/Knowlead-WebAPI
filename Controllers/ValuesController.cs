using System.Linq;
using Knowlead.DomainModel;
using Microsoft.AspNetCore.Mvc;
using Knowlead.DomainModel.LookupModels.Geo;
using Microsoft.AspNetCore.Authorization;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ApplicationDbContext _context;
        
        public ValuesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET api/values
        [HttpGet]
        public City Get()
        {
            var city = _context.Cities.Where(x => x.Name == "Vogosca").FirstOrDefault();
            return city;
        }

        // GET api/values/5
        [HttpGet("{id}"), Authorize]
        public string Get(int id)
        {
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
    }
}
