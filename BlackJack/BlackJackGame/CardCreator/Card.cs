namespace BlackJack.BlackJackGame.CardCreator;

public class Card
{
    private readonly char _cardValue;
    private readonly Color _color;

    public Card(char cardValue, Color color)
    {
        _cardValue = cardValue;
        _color = color;
    }

    public char GetCardValue()
    {
        return this._cardValue;
    }

    public override string ToString()
    {
        string cardName = _cardValue.ToString();
        cardName += _color.ToShortString();
        return cardName;
    }
    
    
}