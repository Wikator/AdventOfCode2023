namespace Shared;

public static class CamelCards
{
    public static int TotalWinnings(Func<int[], int[]> getLabelCounts,
        Dictionary<char, int> labelStrengths)
    {
        var handsFromFile = InputFileReader.ParseFile("data/input.txt", labelStrengths);
        var handTypes = new HandTypes();
        
        foreach (var hand in handsFromFile)
        {
            int[] cardList =
            [
                hand.Card1, hand.Card2, hand.Card3,
                hand.Card4, hand.Card5
            ];

            var labelCounts = getLabelCounts(cardList);
            
            switch (labelCounts.Length)
            {
                case 1:
                    handTypes.FiveOfAKinds.Add(hand);
                    break;
                case 2 when labelCounts.Any(g => g == 4):
                    handTypes.FourOfAKinds.Add(hand);
                    break;
                case 2:
                    handTypes.FullHouses.Add(hand);
                    break;
                case 3 when labelCounts.Any(g => g == 3):
                    handTypes.ThreeOfAKinds.Add(hand);
                    break;
                case 3:
                    handTypes.TwoPairs.Add(hand);
                    break;
                case 4:
                    handTypes.OnePairs.Add(hand);
                    break;
                case 5:
                    handTypes.HighCards.Add(hand);
                    break;
            }
        }

        handTypes.OrderAllHandTypes();
        var combinedHands = handTypes.ConcatAllHandTypes();
            
        var nextRank = 1;
        return combinedHands.Aggregate(0, (acc, curr) => acc + curr.Bet * nextRank++);
    }
}
