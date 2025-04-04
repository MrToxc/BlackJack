namespace BlackJack.Card;

public class Card
{
    private char cardValue;
    private Color color;

    public Card(char cardValue, Color color)
    {
        this.cardValue = cardValue;
        this.color = color;
    }

    private string GetName()
    {
        string cardName = cardValue.ToString();
        cardName += color.ToShortString();
        return cardName;
    }
}