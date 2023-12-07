namespace Shared;

public class HandTypes
{
    public List<Hand> HighCards { get; private set; } = [];
    public List<Hand> OnePairs { get; private set; } = [];
    public List<Hand> TwoPairs { get; private set; } = [];
    public List<Hand> ThreeOfAKinds { get; private set; } = [];
    public List<Hand> FullHouses { get; private set; } = [];
    public List<Hand> FourOfAKinds { get; private set; } = [];
    public List<Hand> FiveOfAKinds { get; private set; } = [];
    
    public void OrderAllHandTypes()
    {
        HighCards = OrderByCardValue(HighCards);
        OnePairs = OrderByCardValue(OnePairs);
        TwoPairs = OrderByCardValue(TwoPairs);
        ThreeOfAKinds = OrderByCardValue(ThreeOfAKinds);
        FullHouses = OrderByCardValue(FullHouses);
        FourOfAKinds = OrderByCardValue(FourOfAKinds);
        FiveOfAKinds = OrderByCardValue(FiveOfAKinds);
    }

    public IEnumerable<Hand> ConcatAllHandTypes()
    {
        return
        [
            ..HighCards, ..OnePairs, ..TwoPairs, ..ThreeOfAKinds,
            ..FullHouses, ..FourOfAKinds, ..FiveOfAKinds
        ];
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
