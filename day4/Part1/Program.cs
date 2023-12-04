using Shared;

namespace Part1;

internal class Program
{
    private static void Main()
    {
        var lines = InputFileReader.ReadAllLines("input.txt");
        var cards = InputFileReader.GetCardsFromLines(lines);

        var answer = cards.Aggregate(0, (acc, card) =>
        {
            var count = card.RepeatingNumbersCount;
            return acc + count switch
            {
                0 => 0,
                _ => (int)Math.Pow(2, count - 1)
            };
        });

        Console.WriteLine(answer);
    }
}
