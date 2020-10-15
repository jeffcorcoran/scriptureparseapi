using Microsoft.AspNetCore.Mvc;
using ScriptureParseApi.Models;

namespace ScriptureParseApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParsedScriptureController : ControllerBase
    {

        [HttpGet("{scripture}")]
        public IActionResult Get(string scripture)
        {
            if (scripture.NullOrEmpty())
                return NotFound();

            return Ok(ScriptureOps.ParseScriptureFromString(scripture));
        }
    }
}
