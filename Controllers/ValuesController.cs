using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Knowlead.DomainModel;
using Knowlead.DomainModel.LookupModels;
using Microsoft.AspNetCore.Mvc;
using Knowlead.DomainModel.LookupModels.Geo;
using Knowlead.DomainModel.LookupModels.Core;

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
        public Achievement Get()
        {
            var city = _context.Cities.Where(x => x.Name == "Vogosca").FirstOrDefault();
            return city;
        }

        // GET api/values/5
        [HttpGet("{id}")]
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
