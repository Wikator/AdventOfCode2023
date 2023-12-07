namespace Shared.Records;

public record Hand(
    int Card1,
    int Card2,
    int Card3,
    int Card4,
    int Card5,
    int Bet)
{
    public int[] GetCardList()
    {
        return [Card1, Card2, Card3, Card4, Card5];
    }
}
