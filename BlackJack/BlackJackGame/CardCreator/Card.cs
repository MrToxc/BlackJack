namespace BlackJack.BlackJackGame.CardCreator;

public class Card
{
    private char _cardValue;
    private Color _color;

    public Card(char cardValue, Color color)
    {
        this._cardValue = cardValue;
        this._color = color;
    }

    private string GetName()
    {
        string cardName = _cardValue.ToString();
        cardName += _color.ToShortString();
        return cardName;
    }
}