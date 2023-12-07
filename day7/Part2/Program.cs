namespace Part2;

internal class Program
{
    public static void Main()
    {
        var labelStrengths = new Dictionary<char, int>
        {
            { 'J', 0 },
            { '2', 1 },
            { '3', 2 },
            { '4', 3 },
            { '5', 4 },
            { '6', 5 },
            { '7', 6 },
            { '8', 7 },
            { '9', 8 },
            { 'T', 9 },
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
                .Select(c => (c, cardList.Count(l => l == c || l == labelStrengths['J'])))
                .Where(c => c.Item1 != labelStrengths['J'] || c.Item2 == 5)
                .Distinct()
                .Select(c => c.Item2)
                .ToArray();
        }
    }
}
