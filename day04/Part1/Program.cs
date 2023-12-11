namespace Part1;

internal class Program
{
    private static void Main()
    {
        var result = File.ReadAllLines("data/input.txt")
            .Select(GetRepeatingNumbersCount)
            .Aggregate(0, AddPoints);

        Console.WriteLine(result);
    }

    private static int GetRepeatingNumbersCount(string line)
    {
        var splitLines = line.Split(" | ");
        var splitFirstPart = splitLines[0].Split(": ");
                
        var winningNumbers = splitFirstPart[1].Split(" ")
            .Where(n => n != string.Empty)
            .Select(int.Parse);
                
        var ownedNumbers = splitLines[1].Split(" ")
            .Where(n => n != string.Empty)
            .Select(int.Parse);
                
        return ownedNumbers.Intersect(winningNumbers).Count();
    }
    
    private static int AddPoints(int accumulator, int repeatingNumbersCount) =>
        accumulator + repeatingNumbersCount switch
        {
            0 => 0,
            _ => (int)Math.Pow(2, repeatingNumbersCount - 1)
        };
}
