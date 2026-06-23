using HorseApp.Api.Data;
using HorseApp.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace HorseApp.Api.Controllers;

[ApiController]
[Route("api/personality")]
public class PersonalityQuizController : ControllerBase
{
    [HttpGet("questions")]
    public ActionResult<IEnumerable<PersonalityQuestion>> GetQuestions() =>
        Ok(PersonalityData.Questions);

    [HttpPost("result")]
    public ActionResult<PersonalityResult> GetResult([FromBody] PersonalityAnswerRequest request)
    {
        if (request.Answers is null || request.Answers.Count == 0)
            return BadRequest("At least one answer is required.");

        var result = PersonalityData.CalculateResult(request.Answers);
        return Ok(result);
    }
}
