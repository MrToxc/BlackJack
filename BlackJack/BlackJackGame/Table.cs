using BlackJack.BlackJackGame.CardCreator;

namespace BlackJack.BlackJackGame;

public class Table
{
    private Hand DealerHand { get; }
    List<Hand> PlayerHands { get; }
    private int CurrentHandIndex = 0; 
    private Heap Shoe;
    private Heap Burned { get; }
    public Rules rules { get; }


    private bool playing = true;


    // START / END of shoe
    public void Initialize()
    {
        Shoe.ToDrawPile(rules.DeckCount);
        Burned.ClearDeck();
        PlayerHands.Clear();
    }

    //
    void AddPlayerHand()
    {
        if (!playing) return;
        PlayerHands.Add(new Hand());
    }

    public Hand DealerDrawCard()
    {
        playing = false;
        Card card = Shoe.DrawCard();
        Burned.AddCard(card);
        DealerHand.AddCard(card);
        return DealerHand;
    }
    
    public void EndRound()
    {
        PlayerHands.Clear();
        CurrentHandIndex = 0;
        playing = true;
    }

    public void DrawCard()
    {
        if (!playing) return;
        Card card = Shoe.DrawCard();
        if (card != null)
        {
            Burned.AddCard(card);
            PlayerHands[CurrentHandIndex].AddCard(card);
        }
        else
        {
            throw new NullReferenceException("Cannot draw card");
        }
    }

    public Hand GetCurrentHand()
    {
        
        return PlayerHands[CurrentHandIndex];
    }

    public void Stand()
    {
        CurrentHandIndex++;
    }

    public Card GetDealerCard()
    {
        Card card = DealerHand.Cards[0];
        return card;
    }
    
    
    
    
    
}