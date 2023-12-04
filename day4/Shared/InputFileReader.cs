namespace Shared;

public static class InputFileReader
{
    // Working directory must be set to day4/ for this to work
    public static IEnumerable<string> ReadAllLines(string fileName)
    {
        return File.ReadAllLines($"data/{fileName}");
    }
    
    public static IEnumerable<Card> GetCardsFromLines(IEnumerable<string> lines)
    {
        return lines.Select(l =>
        {
            var splitLines = l.Split(" | ");
            var splitFirstPart = splitLines[0].Split(": ");
            var gameNumber = int.Parse(splitFirstPart[0].Split(" ")
                .Last(n => n != string.Empty));
            
            var winningNumbers = splitFirstPart[1].Split(" ")
                .Where(n => n != string.Empty)
                .Select(int.Parse);
            
            var ownedNumber = splitLines[1].Split(" ")
                .Where(n => n != string.Empty)
                .Select(int.Parse);
            
            return new Card(gameNumber, winningNumbers, ownedNumber);
        });
    }
}

public class Card(int number, IEnumerable<int> winningNumbers, IEnumerable<int> ownedNumbers)
{
    public int Number { get; } = number;
    public int RepeatingNumbersCount { get; } = ownedNumbers.Intersect(winningNumbers).Count();
}
