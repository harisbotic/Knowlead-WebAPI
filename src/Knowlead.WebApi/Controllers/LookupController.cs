using System.Linq;
using Knowlead.DAL;
using Microsoft.AspNetCore.Mvc;
using Knowlead.DomainModel.LookupModels.Geo;
using System.Collections.Generic;
using Knowlead.DTO.LookupModels.Core;
using AutoMapper;
using Knowlead.DTO.ResponseModels;
using Knowlead.DTO.LookupModels.Geo;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    public class LookupController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public LookupController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET api/lookup/languages
        [HttpGet("languages")]
        public IActionResult GetLanguages()
        {
            var languages = _context.Languages.ToList();

            var languagesModel = new List<LanguageModel>();

            foreach (var lang in languages)
            {
                languagesModel.Add(Mapper.Map<LanguageModel>(lang));
            }

            return new OkObjectResult(new ResponseModel{
                Object = languagesModel
            });
        }

        // GET api/lookup/countries
        [HttpGet("countries")]
        public IActionResult GetCountries()
        {
            var countries = _context.Countries.ToList();

            var countriesModel = new List<CountryModel>();

            foreach (var country in countries)
            {
                countriesModel.Add(Mapper.Map<CountryModel>(country));
            }

            return new OkObjectResult(new ResponseModel{
                Object = countriesModel
            });
        }

        // GET api/lookup/state?countryId=5
        [HttpGet("states")]
        public IActionResult GetStatesFor(int? countryId)
        {
            var states = new List<State>();

            if(countryId.HasValue)
                states = _context.States.Where(x => x.StatesCountryId == countryId.Value).ToList();
            else
                states = _context.States.ToList();
            
            
            var statesModel = new List<StateModel>();

            foreach (var state in states)
            {
                statesModel.Add(Mapper.Map<StateModel>(state));
            }

            return new OkObjectResult(new ResponseModel{
                Object = statesModel
            });
        }

        // GET api/lookup/foses
        [HttpGet("foses")]
        public IActionResult GetAllFOS()
        {
            var foss = _context.Fos.ToList();

            var fossModel = new List<FOSModel>();

            foreach (var fos in foss)
            {
                fossModel.Add(Mapper.Map<FOSModel>(fos));
            }

            return new OkObjectResult(new ResponseModel{
                Object = fossModel
            });
        }

        // GET api/lookup/rewards
        [HttpGet("rewards")]
        public async Task<IActionResult> GetAllRewards()
        {
            var rewards = await _context.Rewards.ToListAsync();

            var rewardsModel = new List<RewardModel>();

            foreach (var reward in rewards)
            {
                rewardsModel.Add(Mapper.Map<RewardModel>(reward));
            }

            return new OkObjectResult(new ResponseModel{
                Object = rewardsModel
            });
        }
    }
}
