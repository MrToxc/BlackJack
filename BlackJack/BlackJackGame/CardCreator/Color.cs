namespace BlackJack.BlackJackGame.CardCreator;

public enum Color
{
    Spades, Hearts, Diamonds, Clubs
}

public static class ColorExtensions
{
    public static string ToShortString(this Color color)
    {
        return color switch
        {
            Color.Spades => "s",
            Color.Hearts => "h",
            Color.Diamonds => "d",
            Color.Clubs => "c",
            _ => "?"
        };
    }
}