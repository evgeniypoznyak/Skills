using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skills.Domain.Dto;
using Skills.Domain.Repository;
using Skills.Infrastructure.Adapter;

namespace Skills.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IRepository<SkillDto> _skillRepository;
        private readonly IAdapter<SkillDto> _adapter;
        private readonly ILogger<SkillsController> _logger;

        public SkillsController(
            IRepository<SkillDto> skillRepository,
            IAdapter<SkillDto> adapter,
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

        // POST /skills
        [HttpPost]
        public async Task<ActionResult<SkillDto>> Save([FromBody] SkillDto content)
        {
            _logger.LogInformation("SkillsController: Preceding request");
            var result = await _skillRepository.Save(content);
            _logger.LogInformation("SkillsController: Saved results");
            return new JsonResult(result);
        }
        
        // PATCH /skills
        [HttpPatch]
        public async Task<ActionResult<SkillDto>> Update([FromBody] SkillDto content)
        {
            _logger.LogInformation("SkillsController: Preceding request");
            var result = await _skillRepository.Update(content);
            _logger.LogInformation("SkillsController: Updated results");
            return new JsonResult(result);
        }
    }
}