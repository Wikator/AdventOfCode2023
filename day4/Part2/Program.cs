using Shared;

namespace Part2;

internal class Program
{
    private static void Main()
    {
        var lines = InputFileReader.ReadAllLines("input.txt");
        var cards = InputFileReader.GetCardsFromLines(lines);
        var dict = new Dictionary<int, int>();

        var result = cards.Aggregate(0, (acc, card) =>
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

            return acc + originalsAndCopies;
        });
        
        Console.WriteLine(result);
    }
}
