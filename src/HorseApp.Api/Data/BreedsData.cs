using HorseApp.Api.Models;

namespace HorseApp.Api.Data;

public static class BreedsData
{
    public static readonly IReadOnlyList<Breed> Breeds = new List<Breed>
    {
        new(
            Id: "arabian",
            Name: "Arabian",
            Origin: "Arabian Peninsula",
            Category: "Sport",
            Summary: "One of the oldest and most recognizable horse breeds, prized for endurance and beauty.",
            Temperament: ["Intelligent", "Spirited", "Loyal", "Sensitive"],
            Uses: ["Endurance racing", "Show", "Trail riding", "Pleasure riding"],
            FunFacts: [
                "Arabians have one fewer vertebra than most other horse breeds.",
                "They are the oldest breed in the world, with a history stretching back 4,500 years.",
                "Arabians can carry a higher percentage of their body weight than other breeds."
            ],
            ImageUrl: "/breeds/arabian.svg",
            HeightRange: "14.1–15.1 hh",
            WeightRange: "800–1,000 lb"
        ),
        new(
            Id: "thoroughbred",
            Name: "Thoroughbred",
            Origin: "England",
            Category: "Sport",
            Summary: "The world's fastest horse breed, bred primarily for racing.",
            Temperament: ["Bold", "Energetic", "Athletic", "Hot-blooded"],
            Uses: ["Racing", "Show jumping", "Eventing", "Polo"],
            FunFacts: [
                "Every Thoroughbred's birthday is January 1st, regardless of their actual birth date.",
                "The average Thoroughbred racehorse can reach speeds over 40 mph.",
                "All modern Thoroughbreds trace back to just three Arabian stallions."
            ],
            ImageUrl: "/breeds/thoroughbred.svg",
            HeightRange: "15.2–17 hh",
            WeightRange: "1,000–1,300 lb"
        ),
        new(
            Id: "friesian",
            Name: "Friesian",
            Origin: "Netherlands",
            Category: "Draft/Sport",
            Summary: "A strikingly beautiful black horse with a flowing mane and feathered legs.",
            Temperament: ["Gentle", "Willing", "Energetic", "Graceful"],
            Uses: ["Dressage", "Carriage driving", "Film & TV", "Pleasure riding"],
            FunFacts: [
                "Friesians are almost always black — other colors are extremely rare.",
                "They nearly went extinct twice in the 20th century.",
                "Their long, thick mane and tail should never be cut by tradition."
            ],
            ImageUrl: "/breeds/friesian.svg",
            HeightRange: "15–17 hh",
            WeightRange: "1,200–1,400 lb"
        ),
        new(
            Id: "andalusian",
            Name: "Andalusian",
            Origin: "Spain",
            Category: "Sport",
            Summary: "An ancient Iberian breed renowned for its elegance and high-stepping movement.",
            Temperament: ["Brave", "Docile", "Elegant", "Intelligent"],
            Uses: ["Dressage", "Bullfighting", "Show", "Classical riding"],
            FunFacts: [
                "Andalusians have been the horse of kings and warriors for centuries.",
                "They appear in cave paintings dating back 30,000 years.",
                "Most Andalusians are grey, though bay and black also occur."
            ],
            ImageUrl: "/breeds/andalusian.svg",
            HeightRange: "15.1–16.2 hh",
            WeightRange: "900–1,100 lb"
        ),
        new(
            Id: "mustang",
            Name: "Mustang",
            Origin: "North America (feral)",
            Category: "Wild",
            Summary: "Free-roaming feral horses descended from Spanish stock brought to the Americas.",
            Temperament: ["Independent", "Hardy", "Intelligent", "Wary"],
            Uses: ["Trail riding", "Ranch work", "Endurance", "Natural horsemanship"],
            FunFacts: [
                "Mustangs are not truly wild but feral — descended from domesticated Spanish horses.",
                "The US government manages about 80,000 free-roaming mustangs.",
                "A mustang can survive in environments most domestic breeds cannot."
            ],
            ImageUrl: "/breeds/mustang.svg",
            HeightRange: "13.2–15 hh",
            WeightRange: "700–900 lb"
        ),
        new(
            Id: "clydesdale",
            Name: "Clydesdale",
            Origin: "Scotland",
            Category: "Draft",
            Summary: "A powerful draft horse famous for its feathered hooves and gentle temperament.",
            Temperament: ["Gentle", "Calm", "Willing", "Sociable"],
            Uses: ["Farm work", "Logging", "Parades", "Showing"],
            FunFacts: [
                "Clydesdales are one of the largest horse breeds, standing up to 18 hh.",
                "The Budweiser Clydesdales have made this breed a cultural icon.",
                "During WWI, over one million Clydesdales were used by the British army."
            ],
            ImageUrl: "/breeds/clydesdale.svg",
            HeightRange: "16–18 hh",
            WeightRange: "1,800–2,200 lb"
        ),
        new(
            Id: "appaloosa",
            Name: "Appaloosa",
            Origin: "United States",
            Category: "Sport",
            Summary: "Known for its distinctive spotted coat, originally bred by the Nez Perce people.",
            Temperament: ["Hardy", "Versatile", "Independent", "Friendly"],
            Uses: ["Western riding", "Trail riding", "Racing", "Show"],
            FunFacts: [
                "Appaloosas have striped hooves and visible white sclera in their eyes.",
                "The breed nearly went extinct when the US Army defeated the Nez Perce in 1877.",
                "No two Appaloosas have exactly the same coat pattern."
            ],
            ImageUrl: "/breeds/appaloosa.svg",
            HeightRange: "14.2–16 hh",
            WeightRange: "950–1,200 lb"
        ),
        new(
            Id: "lipizzaner",
            Name: "Lipizzaner",
            Origin: "Slovenia/Austria",
            Category: "Sport",
            Summary: "The famous white horses of the Spanish Riding School, masters of classical dressage.",
            Temperament: ["Intelligent", "Proud", "Willing", "Athletic"],
            Uses: ["Classical dressage", "Haute école", "Carriage", "Show"],
            FunFacts: [
                "Lipizzaners are born dark and gradually turn grey-white by age 10.",
                "The 'airs above the ground' movements they perform have roots in medieval cavalry tactics.",
                "The Spanish Riding School in Vienna has been training Lipizzaners since 1572."
            ],
            ImageUrl: "/breeds/lipizzaner.svg",
            HeightRange: "14.2–15.2 hh",
            WeightRange: "1,000–1,300 lb"
        ),
        new(
            Id: "icelandic",
            Name: "Icelandic Horse",
            Origin: "Iceland",
            Category: "Gaited",
            Summary: "A small, sturdy breed unique to Iceland, famous for its extra gaits.",
            Temperament: ["Friendly", "Brave", "Hardy", "Versatile"],
            Uses: ["Trekking", "Racing", "Leisure", "Herding"],
            FunFacts: [
                "Icelandic horses can perform two extra gaits: the tölt and the flying pace.",
                "Once an Icelandic horse leaves Iceland, it can never return — strict biosecurity rules.",
                "Despite their small size, they regularly carry adult riders with ease."
            ],
            ImageUrl: "/breeds/icelandic.svg",
            HeightRange: "13–14 hh",
            WeightRange: "730–840 lb"
        ),
        new(
            Id: "quarter-horse",
            Name: "American Quarter Horse",
            Origin: "United States",
            Category: "Sport",
            Summary: "The most popular horse breed in the US, named for its speed over a quarter mile.",
            Temperament: ["Calm", "Willing", "Versatile", "Reliable"],
            Uses: ["Western riding", "Ranch work", "Racing", "Rodeo"],
            FunFacts: [
                "Quarter Horses can reach 55 mph over a quarter mile — faster than a Thoroughbred.",
                "They are the most numerous horse breed in the world.",
                "The Quarter Horse registry has over 3 million registered animals."
            ],
            ImageUrl: "/breeds/quarter-horse.svg",
            HeightRange: "14.3–16 hh",
            WeightRange: "950–1,200 lb"
        ),
        new(
            Id: "akhal-teke",
            Name: "Akhal-Teke",
            Origin: "Turkmenistan",
            Category: "Sport",
            Summary: "An ancient breed with a shimmering metallic coat, built for desert endurance.",
            Temperament: ["Loyal", "Intelligent", "Spirited", "Independent"],
            Uses: ["Endurance", "Dressage", "Show jumping", "Racing"],
            FunFacts: [
                "Akhal-Tekes have a natural metallic sheen to their coat caused by unique hair structure.",
                "They can go days without water, an adaptation to desert life.",
                "Alexander the Great's legendary horse Bucephalus may have been an Akhal-Teke."
            ],
            ImageUrl: "/breeds/akhal-teke.svg",
            HeightRange: "14.2–16 hh",
            WeightRange: "900–1,000 lb"
        ),
        new(
            Id: "morgan",
            Name: "Morgan",
            Origin: "United States",
            Category: "Sport",
            Summary: "America's first breed, compact and versatile, known for its kind temperament.",
            Temperament: ["Eager", "Kind", "Hardy", "Versatile"],
            Uses: ["Driving", "Trail riding", "Show", "Western"],
            FunFacts: [
                "All Morgans trace to a single stallion named Figure, born around 1789.",
                "Figure was known for outpulling and outrunning much larger horses.",
                "Morgans served extensively in the US Civil War on both sides."
            ],
            ImageUrl: "/breeds/morgan.svg",
            HeightRange: "14.1–15.2 hh",
            WeightRange: "900–1,100 lb"
        ),
    };

    private static readonly Random _rng = new();

    public static IReadOnlyList<BreedSummary> GetSummaries() =>
        Breeds.Select(b => new BreedSummary(b.Id, b.Name, b.Origin, b.Category, b.Summary, b.ImageUrl)).ToList();

    public static Breed? FindById(string id) =>
        Breeds.FirstOrDefault(b => b.Id == id);

    public static (QuizQuestion question, string correctBreedId) GetRandomQuizQuestion()
    {
        var correct = Breeds[_rng.Next(Breeds.Count)];
        var distractors = Breeds
            .Where(b => b.Id != correct.Id)
            .OrderBy(_ => _rng.Next())
            .Take(3)
            .ToList();

        var options = distractors
            .Append(correct)
            .OrderBy(_ => _rng.Next())
            .Select(b => new QuizOption(b.Id, b.Name))
            .ToArray();

        var question = new QuizQuestion(
            QuestionId: correct.Id,
            Description: correct.Summary,
            TraitClues: correct.Temperament.Take(2).Concat(correct.Uses.Take(2)).ToArray(),
            Options: options
        );

        return (question, correct.Id);
    }
}
