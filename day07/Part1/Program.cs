namespace Part1;

internal class Program
{
    public static void Main()
    {
        var labelStrengths = new Dictionary<char, int>
        {
            { '2', 0 },
            { '3', 1 },
            { '4', 2 },
            { '5', 3 },
            { '6', 4 },
            { '7', 5 },
            { '8', 6 },
            { '9', 7 },
            { 'T', 8 },
            { 'J', 9 },
            { 'Q', 10 },
            { 'K', 11 },
            { 'A', 12 }
        };

        var result = Shared.CamelCards.TotalWinnings(GetLabelCounts, labelStrengths);
        Console.WriteLine(result);
        return;

        int[] GetLabelCounts(int[] cardList)
        {
            return cardList
                .GroupBy(c => c)
                .Select(g => g.Count())
                .OrderDescending()
                .ToArray();
        }
    }
}
