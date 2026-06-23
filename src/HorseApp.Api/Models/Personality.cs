namespace HorseApp.Api.Models;

public record PersonalityQuestion(
    string QuestionId,
    string Text,
    PersonalityOption[] Options
);

public record PersonalityOption(
    string OptionId,
    string Text,
    Dictionary<string, int> BreedScores
);

public record PersonalityAnswerRequest(List<PersonalityAnswer> Answers);

public record PersonalityAnswer(string QuestionId, string OptionId);

public record PersonalityResult(
    string BreedId,
    string BreedName,
    string Description,
    string[] Traits,
    string ImageUrl
);
