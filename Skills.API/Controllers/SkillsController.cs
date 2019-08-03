using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountDataExtractService.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Skills.Domain.Entity;
using Skills.Domain.Repository;
using Skills.Infrastructure.Adapter;

namespace Skills.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IRepository<Skill> _skillRepository;
        private readonly IAdapter _adapter;
        private readonly ILogger<SkillsController> _logger;

        public SkillsController( 
            IRepository<Skill> skillRepository,
            IAdapter adapter,
            ILogger<SkillsController> logger
            )
        {
            _skillRepository = skillRepository;
            _adapter = adapter;
            _logger = logger;
        }
        
        // GET /skills
        [HttpGet]
        public JsonResult Find()
        {
            var result =_adapter.FindAll();
//            _logger.LogInformation("Fetched results: {@result}", result);
            return new JsonResult(JsonConvert.DeserializeObject<object>(JsonConvert.SerializeObject(result)));
        }
        // POST /skills
        [HttpPost]
        public JsonResult Save([FromBody] SkillDto content)
        {
            var result = _skillRepository.Save(content);
            return new JsonResult(result);
        }
        
    }
}