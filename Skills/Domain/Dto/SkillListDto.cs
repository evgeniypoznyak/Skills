using System.Collections.Generic;
using Newtonsoft.Json;

namespace Skills.Domain.Dto
{
    public class SkillListDto
    {
        [JsonProperty("skills")] 
        public List<SkillDto> Skills { get; set; }
    }
}