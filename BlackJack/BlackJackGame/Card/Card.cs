namespace BlackJack.Card;

public class Card
{
    private string cardName;
    private char cardValue;
    private Color color;

    public Card(char cardValue, Color color)
    {
        this.cardValue = cardValue;
        this.color = color;
        setName();
    }

    private void setName()
    {
        if (cardValue == '2' || cardValue == '3' || cardValue == '4' || cardValue == '5' || cardValue == '6' || cardValue == '7' || cardValue == '8' || cardValue == '9')
        {
            cardName = cardValue.ToString();
            return;
        } 
        if (cardValue == 'T') cardName = "10";
        if (cardValue == 'J') cardName = "Jack";
        if (cardValue == 'Q') cardName = "Queen";
        if (cardValue == 'K') cardName = "King";
        if (cardValue == 'A') cardName = "Ace";
        else
        {
            throw new Exception("Unknown card value");
        }
    }
}