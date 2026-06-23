using HorseApp.Api.Data;
using HorseApp.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace HorseApp.Api.Controllers;

[ApiController]
[Route("api/breeds")]
public class BreedsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<BreedSummary>> GetAll() =>
        Ok(BreedsData.GetSummaries());

    [HttpGet("{id}")]
    public ActionResult<Breed> GetById(string id)
    {
        var breed = BreedsData.FindById(id);
        return breed is null ? NotFound() : Ok(breed);
    }

    [HttpGet("quiz/question")]
    public ActionResult<QuizQuestion> GetQuizQuestion()
    {
        var (question, _) = BreedsData.GetRandomQuizQuestion();
        return Ok(question);
    }

    [HttpPost("quiz/answer")]
    public ActionResult<QuizAnswerResponse> CheckAnswer([FromBody] QuizAnswerRequest request)
    {
        var correctBreed = BreedsData.FindById(request.QuestionId);
        if (correctBreed is null)
            return NotFound("Question not found.");

        var correct = request.AnswerId == correctBreed.Id;
        return Ok(new QuizAnswerResponse(correct, correctBreed.Id, correctBreed.Name));
    }
}
