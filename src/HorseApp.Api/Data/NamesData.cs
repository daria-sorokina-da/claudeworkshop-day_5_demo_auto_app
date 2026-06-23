namespace HorseApp.Api.Data;

public static class NamesData
{
    private static readonly Dictionary<string, Dictionary<string, string[]>> _names = new()
    {
        ["elegant"] = new()
        {
            ["male"]    = ["Aurelius", "Bartholomew", "Cavendish", "Dorian", "Evander", "Fitzgerald", "Galahad", "Hawthorne", "Isidore", "Jasper"],
            ["female"]  = ["Seraphina", "Celestine", "Arabella", "Isadora", "Lavinia", "Evangeline", "Rosalind", "Vivienne", "Genevieve", "Clarissa"],
            ["neutral"] = ["Avalon", "Solstice", "Meridian", "Equinox", "Valerian", "Saffron", "Calliope", "Elysium", "Vesper", "Zephyr"],
        },
        ["wild"] = new()
        {
            ["male"]    = ["Thunder", "Blaze", "Storm", "Ranger", "Maverick", "Rebel", "Canyon", "Bandit", "Diesel", "Gunner"],
            ["female"]  = ["Tempest", "Raven", "Ember", "Dusty", "Sage", "Blaze", "Dakota", "Scarlett", "Cinder", "Vixen"],
            ["neutral"] = ["Wildfire", "Tornado", "Eclipse", "Typhoon", "Comet", "Shadow", "Phantom", "Apex", "Titan", "Chaos"],
        },
        ["mythical"] = new()
        {
            ["male"]    = ["Pegasus", "Achilles", "Poseidon", "Orion", "Atlas", "Zephyrus", "Hyperion", "Triton", "Ares", "Helios"],
            ["female"]  = ["Athena", "Selene", "Calypso", "Nyx", "Circe", "Andromeda", "Persephone", "Hecate", "Cassandra", "Artemis"],
            ["neutral"] = ["Nimue", "Merlin", "Elara", "Aether", "Lyra", "Solaris", "Caelum", "Mythos", "Arcana", "Zenith"],
        },
        ["celtic"] = new()
        {
            ["male"]    = ["Cormac", "Fergus", "Bran", "Conor", "Diarmuid", "Fionn", "Lorcan", "Cillian", "Ruairi", "Tadhg"],
            ["female"]  = ["Saoirse", "Niamh", "Brigid", "Aoife", "Clodagh", "Deirdre", "Eithne", "Fionnuala", "Grainne", "Siobhan"],
            ["neutral"] = ["Aisling", "Rowan", "Caolán", "Caledonia", "Tara", "Finvarra", "Eriu", "Sidhe", "Morrigan", "Danu"],
        },
        ["nature"] = new()
        {
            ["male"]    = ["Birch", "Flint", "Gale", "Heath", "Jasper", "Marsh", "Oak", "Pine", "Reed", "Stone"],
            ["female"]  = ["Willow", "Clover", "Fern", "Hazel", "Ivy", "Meadow", "River", "Sky", "Dawn", "Blossom"],
            ["neutral"] = ["Cedar", "Dusk", "Frost", "Gust", "Harbor", "Mist", "Pebble", "Quill", "Tide", "Wren"],
        },
    };

    private static readonly Random _rng = new();

    public static string[] Generate(string style, string gender, int count = 5)
    {
        var normalizedStyle  = style.ToLowerInvariant();
        var normalizedGender = gender.ToLowerInvariant();

        if (!_names.TryGetValue(normalizedStyle, out var byGender))
            normalizedStyle = "nature";

        if (!_names[normalizedStyle].TryGetValue(normalizedGender, out var pool))
            normalizedGender = "neutral";

        pool = _names[normalizedStyle][normalizedGender];

        return pool.OrderBy(_ => _rng.Next()).Take(count).ToArray();
    }

    public static IReadOnlyList<string> ValidStyles  => _names.Keys.ToList();
    public static IReadOnlyList<string> ValidGenders => ["male", "female", "neutral"];
}
