using HorseApp.Api.Data;
using HorseApp.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace HorseApp.Api.Controllers;

[ApiController]
[Route("api/names")]
public class NamesController : ControllerBase
{
    [HttpGet("generate")]
    public ActionResult<NameGenerateResponse> Generate(
        [FromQuery] string style = "nature",
        [FromQuery] string gender = "neutral")
    {
        var names = NamesData.Generate(style, gender);
        return Ok(new NameGenerateResponse(names, style.ToLowerInvariant(), gender.ToLowerInvariant()));
    }

    [HttpGet("options")]
    public ActionResult<object> GetOptions() =>
        Ok(new { styles = NamesData.ValidStyles, genders = NamesData.ValidGenders });
}
