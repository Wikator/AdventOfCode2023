namespace Part2;

internal class Program
{
    private static void Main()
    {
        var dict = new Dictionary<int, int>();

        var result = File.ReadAllLines("data/input.txt")
            .Select(ParseToCard)
            .Aggregate(0, (acc, curr) => AddScratchcards(acc, curr, dict));
        
        Console.WriteLine(result);
    }

    private static Card ParseToCard(string line)
    {
        var splitLines = line.Split(" | ");
        var splitFirstPart = splitLines[0].Split(": ");
        var gameNumber = int.Parse(splitFirstPart[0].Split(" ")
            .Last(n => n != string.Empty));
        
        var winningNumbers = splitFirstPart[1].Split(" ")
            .Where(n => n != string.Empty)
            .Select(int.Parse);
                
        var ownedNumbers = splitLines[1].Split(" ")
            .Where(n => n != string.Empty)
            .Select(int.Parse);
                
        return new Card(gameNumber, ownedNumbers.Intersect(winningNumbers).Count());
    }
    
    private static int AddScratchcards(int accumulator, Card card, Dictionary<int, int> dict)
    {
        var originalsAndCopies = 1 + dict.TryGetValue(card.Number, out var copies) switch
        {
            true => copies,
            false => 0
        };

        for (var i = card.Number + 1; i <= card.Number + card.RepeatingNumbersCount; i++)
        {
            if (!dict.TryAdd(i, originalsAndCopies))
                dict[i] += originalsAndCopies;
        }

        return accumulator + originalsAndCopies;
    }
    
    private class Card(int number, int repeatingNumbersCount)
    {
        public int Number { get; } = number;
        public int RepeatingNumbersCount { get; } = repeatingNumbersCount;
    }
}
