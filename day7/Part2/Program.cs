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
            if (cardList.All(c => c == labelStrengths['J']))
                return [cardList.Length];

            var labelCounts = cardList
                .Where(c => c != labelStrengths['J'])
                .GroupBy(c => c)
                .Select(g => g.Count())
                .OrderDescending()
                .ToArray();

            return [labelCounts[0] + cardList.Count(c => c == labelStrengths['J']), ..labelCounts.Skip(1)];
        }
    }
}
