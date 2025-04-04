namespace BlackJack.Card;

public enum Color
{
    SPADES, HEARTS, DIAMONDS, CLUBS
}

public static class ColorExtensions
{
    public static string ToShortString(this Color color)
    {
        return color switch
        {
            Color.SPADES => "s",
            Color.HEARTS => "h",
            Color.DIAMONDS => "d",
            Color.CLUBS => "c",
            _ => "?"
        };
    }
}