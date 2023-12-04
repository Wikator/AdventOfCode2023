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
            return acc + card.RepeatingNumbersCount switch
            {
                0 => 0,
                _ => (int)Math.Pow(2, card.RepeatingNumbersCount - 1)
            };
        });

        Console.WriteLine(answer);
    }
}
