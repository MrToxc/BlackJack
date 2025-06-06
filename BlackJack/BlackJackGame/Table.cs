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
        if (!IsPlaying()) return;
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
        if (!IsPlaying()) return;
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
        IsPlaying();
        return PlayerHands[CurrentHandIndex];
    }

    public void Stand()
    {
        if (!IsPlaying()) return;
        CurrentHandIndex++;
    }

    public void DoubleDown()
    {
        if (!IsPlaying()) return;
        DrawCard();
        Stand();
    }

    public Card GetDealerCard()
    {
        Card card = DealerHand.Cards[0];
        return card;
    }
    

    public bool IsPlayingRoundOver()
    {
        if (PlayerHands.Count > CurrentHandIndex) return false;
        playing = false;
        return true;

    }

    private bool IsPlaying()
    {
        if (!playing)
        {
            throw new Exception("Player is playing after round is over!!!!");
        }
        return playing;
    }
    
    
    
    
}