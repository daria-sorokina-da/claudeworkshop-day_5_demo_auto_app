using HorseApp.Api.Models;

namespace HorseApp.Api.Data;

public static class PersonalityData
{
    public static readonly IReadOnlyList<PersonalityQuestion> Questions = new List<PersonalityQuestion>
    {
        new(
            QuestionId: "q1",
            Text: "What's your ideal weekend?",
            Options:
            [
                new("a", "Exploring somewhere new and adventurous",
                    new() { ["arabian"] = 3, ["mustang"] = 2 }),
                new("b", "A long, steady hike with friends",
                    new() { ["clydesdale"] = 3, ["morgan"] = 2 }),
                new("c", "Performing at a showcase or event",
                    new() { ["lipizzaner"] = 3, ["andalusian"] = 2 }),
                new("d", "A relaxed trail ride with no schedule",
                    new() { ["icelandic"] = 3, ["quarter-horse"] = 2 }),
            ]
        ),
        new(
            QuestionId: "q2",
            Text: "How do you handle stress?",
            Options:
            [
                new("a", "Channel it into energy and keep moving",
                    new() { ["thoroughbred"] = 3, ["arabian"] = 2 }),
                new("b", "Stay calm and think it through",
                    new() { ["morgan"] = 3, ["quarter-horse"] = 2 }),
                new("c", "Rely on your close circle for support",
                    new() { ["friesian"] = 3, ["clydesdale"] = 2 }),
                new("d", "Adapt quickly — you're tough by nature",
                    new() { ["mustang"] = 3, ["akhal-teke"] = 2 }),
            ]
        ),
        new(
            QuestionId: "q3",
            Text: "Which environment feels most like home?",
            Options:
            [
                new("a", "Open desert or endless plains",
                    new() { ["arabian"] = 3, ["akhal-teke"] = 3 }),
                new("b", "Cool northern pastures and green hills",
                    new() { ["icelandic"] = 3, ["clydesdale"] = 2 }),
                new("c", "A royal stable or grand arena",
                    new() { ["lipizzaner"] = 3, ["andalusian"] = 3 }),
                new("d", "Rolling ranch land out west",
                    new() { ["quarter-horse"] = 3, ["appaloosa"] = 2 }),
            ]
        ),
        new(
            QuestionId: "q4",
            Text: "What best describes your social style?",
            Options:
            [
                new("a", "Intense and loyal to a chosen few",
                    new() { ["akhal-teke"] = 3, ["arabian"] = 2 }),
                new("b", "Friendly and easy to get along with",
                    new() { ["morgan"] = 3, ["appaloosa"] = 2 }),
                new("c", "Commanding — people notice when you enter a room",
                    new() { ["friesian"] = 3, ["andalusian"] = 2 }),
                new("d", "Independent, but sociable on your own terms",
                    new() { ["mustang"] = 3, ["thoroughbred"] = 2 }),
            ]
        ),
        new(
            QuestionId: "q5",
            Text: "Your friend asks for help with a huge task. You:",
            Options:
            [
                new("a", "Jump in with full energy — you love a challenge",
                    new() { ["thoroughbred"] = 3, ["arabian"] = 2 }),
                new("b", "Steady and reliable — you do your share without complaint",
                    new() { ["clydesdale"] = 3, ["quarter-horse"] = 2 }),
                new("c", "Bring elegance and precision to every step",
                    new() { ["lipizzaner"] = 3, ["andalusian"] = 2 }),
                new("d", "Offer something unique nobody else thought of",
                    new() { ["akhal-teke"] = 3, ["appaloosa"] = 2 }),
            ]
        ),
        new(
            QuestionId: "q6",
            Text: "Pick a travel style:",
            Options:
            [
                new("a", "Fast and efficient — you want to cover maximum ground",
                    new() { ["thoroughbred"] = 3, ["quarter-horse"] = 2 }),
                new("b", "Long endurance journeys, no matter the terrain",
                    new() { ["arabian"] = 3, ["akhal-teke"] = 2 }),
                new("c", "Comfortable and scenic — the journey is the point",
                    new() { ["icelandic"] = 3, ["morgan"] = 2 }),
                new("d", "You go wherever the wind takes you",
                    new() { ["mustang"] = 3, ["appaloosa"] = 2 }),
            ]
        ),
        new(
            QuestionId: "q7",
            Text: "What quality do people most admire in you?",
            Options:
            [
                new("a", "Your striking, unforgettable presence",
                    new() { ["friesian"] = 3, ["akhal-teke"] = 2 }),
                new("b", "Your strength and dependability",
                    new() { ["clydesdale"] = 3, ["morgan"] = 2 }),
                new("c", "Your grace and precision",
                    new() { ["lipizzaner"] = 3, ["andalusian"] = 2 }),
                new("d", "Your tenacity and spirit",
                    new() { ["thoroughbred"] = 3, ["mustang"] = 2 }),
            ]
        ),
        new(
            QuestionId: "q8",
            Text: "What's your relationship with rules?",
            Options:
            [
                new("a", "You follow them precisely — they exist for good reason",
                    new() { ["lipizzaner"] = 3, ["morgan"] = 2 }),
                new("b", "You respect them but adapt them to the situation",
                    new() { ["quarter-horse"] = 3, ["appaloosa"] = 2 }),
                new("c", "You push limits — you're built for speed, not constraints",
                    new() { ["thoroughbred"] = 3, ["arabian"] = 2 }),
                new("d", "Rules? You make your own path",
                    new() { ["mustang"] = 3, ["akhal-teke"] = 2 }),
            ]
        ),
    };

    private static readonly Dictionary<string, (string Description, string[] Traits)> _breedPersonalities = new()
    {
        ["arabian"]      = ("You are spirited, beautiful, and deeply loyal. You thrive on adventure and form unbreakable bonds with those you trust. Your endurance and intelligence are unmatched.", ["Spirited", "Loyal", "Intelligent", "Enduring"]),
        ["thoroughbred"] = ("You live for the rush. Bold, energetic, and fiercely competitive, you set the pace and expect others to keep up. You were born to run.", ["Bold", "Competitive", "Athletic", "Hot-blooded"]),
        ["friesian"]     = ("Strikingly beautiful and deeply kind, you command attention effortlessly. You are graceful under pressure and deeply loyal to those in your circle.", ["Elegant", "Gentle", "Graceful", "Loyal"]),
        ["andalusian"]   = ("Regal and brave, you carry yourself with quiet confidence. You have a talent for performance and a natural dignity that turns heads wherever you go.", ["Brave", "Regal", "Elegant", "Intelligent"]),
        ["mustang"]      = ("Wild and free at heart, you resist being fenced in. Resourceful and resilient, you thrive where others struggle and forge your own way in the world.", ["Independent", "Hardy", "Resilient", "Free-spirited"]),
        ["clydesdale"]   = ("Gentle giant. You are dependable, warm, and extraordinarily strong. People know they can count on you for the long haul — you never let anyone down.", ["Gentle", "Dependable", "Strong", "Sociable"]),
        ["appaloosa"]    = ("Colorful and one-of-a-kind, you stand out from the crowd. Versatile and friendly, you adapt easily and bring a unique perspective to everything you do.", ["Versatile", "Unique", "Friendly", "Hardy"]),
        ["lipizzaner"]   = ("Disciplined and precise, you are a true artist. You have spent years perfecting your craft, and your performances are nothing short of breathtaking.", ["Disciplined", "Precise", "Proud", "Athletic"]),
        ["icelandic"]    = ("Unassuming but remarkable, you have hidden depths. Brave, hardy, and unexpectedly versatile, you excel in conditions that overwhelm everyone else.", ["Brave", "Hardy", "Friendly", "Versatile"]),
        ["quarter-horse"]= ("Calm, reliable, and explosively capable when it counts. You get the job done without drama, and people know they can trust you completely.", ["Calm", "Reliable", "Versatile", "Trustworthy"]),
        ["akhal-teke"]   = ("Rare, luminous, and intensely loyal. You have an ancient soul and a bond with those you choose that few will ever fully understand.", ["Loyal", "Rare", "Spirited", "Enduring"]),
        ["morgan"]       = ("Eager and kind, you are the original American dreamer — compact, versatile, and capable of more than anyone expects. You make friends everywhere you go.", ["Eager", "Kind", "Versatile", "Hardy"]),
    };

    public static PersonalityResult CalculateResult(List<PersonalityAnswer> answers)
    {
        var scores = new Dictionary<string, int>();

        foreach (var answer in answers)
        {
            var question = Questions.FirstOrDefault(q => q.QuestionId == answer.QuestionId);
            if (question is null) continue;

            var option = question.Options.FirstOrDefault(o => o.OptionId == answer.OptionId);
            if (option is null) continue;

            foreach (var (breedId, points) in option.BreedScores)
            {
                scores.TryGetValue(breedId, out var current);
                scores[breedId] = current + points;
            }
        }

        var topBreedId = scores.OrderByDescending(kvp => kvp.Value).First().Key;
        var breed = BreedsData.FindById(topBreedId)!;
        var (description, traits) = _breedPersonalities[topBreedId];

        return new PersonalityResult(
            BreedId: breed.Id,
            BreedName: breed.Name,
            Description: description,
            Traits: traits,
            ImageUrl: breed.ImageUrl
        );
    }
}
