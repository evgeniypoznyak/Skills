﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skills.Domain.Aggregate;
using Skills.Domain.Dto;
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
        public async Task<ActionResult<SkillListDto>> Find()
        {
            var result = await _adapter.FindAll();
            _logger.LogInformation("Fetched results: {@result}", result);
            return Ok(result);
        }

//        // POST /skills
//        [HttpPost]
//        public async Task<ActionResult<SkillList>> Save([FromBody] SkillDto content)
//        {
//            var result = _skillRepository.Save(content);
//            _logger.LogInformation("Saved results: {@result}", result);
//            return new JsonResult(result);
//        }
    }
}