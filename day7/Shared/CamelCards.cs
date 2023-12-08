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
            
            switch (labelCounts)
            {
                case [5]:
                    handTypes.FiveOfAKinds.Add(hand);
                    break;
                case [4, 1]:
                    handTypes.FourOfAKinds.Add(hand);
                    break;
                case [3, 2]:
                    handTypes.FullHouses.Add(hand);
                    break;
                case [3, 1, 1]:
                    handTypes.ThreeOfAKinds.Add(hand);
                    break;
                case [2, 2, 1]:
                    handTypes.TwoPairs.Add(hand);
                    break;
                case [2, 1, 1, 1]:
                    handTypes.OnePairs.Add(hand);
                    break;
                case [1, 1, 1, 1, 1]:
                    handTypes.HighCards.Add(hand);
                    break;
                default:
                    throw new ArgumentException("Invalid hand length");
            }
        }

        handTypes.OrderAllHandTypes();
        var combinedHands = handTypes.ConcatAllHandTypes();
            
        var nextRank = 1;
        return combinedHands.Aggregate(0, (acc, curr) => acc + curr.Bet * nextRank++);
    }
}
