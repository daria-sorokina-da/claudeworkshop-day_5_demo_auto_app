namespace HorseApp.Api.Models;

public record Breed(
    string Id,
    string Name,
    string Origin,
    string Category,
    string Summary,
    string[] Temperament,
    string[] Uses,
    string[] FunFacts,
    string ImageUrl,
    string HeightRange,
    string WeightRange
);

public record BreedSummary(
    string Id,
    string Name,
    string Origin,
    string Category,
    string Summary,
    string ImageUrl
);

public record QuizQuestion(
    string QuestionId,
    string Description,
    string[] TraitClues,
    QuizOption[] Options
);

public record QuizOption(string BreedId, string BreedName);

public record QuizAnswerRequest(string QuestionId, string AnswerId);

public record QuizAnswerResponse(bool Correct, string CorrectBreedId, string CorrectBreedName);
