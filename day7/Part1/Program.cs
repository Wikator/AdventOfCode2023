using Shared;
using Shared.Records;

namespace Part1;

internal class Program
{
    public static void Main()
    {
        var labelStrengths = new Dictionary<char, int>()
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
    
        var hands = InputFileReader.ParseFile("data/input.txt", labelStrengths);
        var highCards = new List<Hand>();
        var onePairs = new List<Hand>();
        var twoPairs = new List<Hand>();
        var threeOfAKinds = new List<Hand>();
        var fullHouses = new List<Hand>();
        var fourOfAKinds = new List<Hand>();
        var fiveOfAKinds = new List<Hand>();
        
        foreach (var hand in hands)
        {
            var cardList = hand.GetCardList();

            var groupedCardList = cardList
                .Select(c => (c, cardList.Count(l => l == c)))
                .Distinct()
                .Select(c => c.Item2)
                .ToArray();
            
            switch (groupedCardList.Length)
            {
                case 1:
                    fiveOfAKinds.Add(hand);
                    break;
                case 2 when groupedCardList.Any(g => g == 4):
                    fourOfAKinds.Add(hand);
                    break;
                case 2:
                    fullHouses.Add(hand);
                    break;
                case 3 when groupedCardList.Any(g => g == 3):
                    threeOfAKinds.Add(hand);
                    break;
                case 3:
                    twoPairs.Add(hand);
                    break;
                case 4:
                    onePairs.Add(hand);
                    break;
                case 5:
                    highCards.Add(hand);
                    break;
            }
        }

        highCards = OrderByCardValue(highCards);
        onePairs = OrderByCardValue(onePairs);
        twoPairs = OrderByCardValue(twoPairs);
        threeOfAKinds = OrderByCardValue(threeOfAKinds);
        fullHouses = OrderByCardValue(fullHouses);
        fourOfAKinds = OrderByCardValue(fourOfAKinds);
        fiveOfAKinds = OrderByCardValue(fiveOfAKinds);

        var combinedList = highCards
            .Concat(onePairs)
            .Concat(twoPairs)
            .Concat(threeOfAKinds)
            .Concat(fullHouses)
            .Concat(fourOfAKinds)
            .Concat(fiveOfAKinds);
            
        var nextRank = 1;
        var result = combinedList.Aggregate(0, (acc, curr) => acc + curr.Bet * nextRank++);
        Console.WriteLine(result);
    }


    private static List<Hand> OrderByCardValue(IEnumerable<Hand> hands)
    {
        return hands
            .OrderBy(h => h.Card1)
            .ThenBy(h => h.Card2)
            .ThenBy(h => h.Card3)
            .ThenBy(h => h.Card4)
            .ThenBy(h => h.Card5)
            .ToList();
    }
}
