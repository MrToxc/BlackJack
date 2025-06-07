using BlackJack.BlackJackGame.CardCreator;

namespace BlackJack.BlackJackGame;

public class Table
{
    private Hand DealerHand { get; }      = new Hand(false);
    private List<Hand>? PlayerHands { get; } = new List<Hand>();
    private Heap Shoe           { get; } = new Heap();
    private Heap Burned         { get; } = new Heap();
    public  Rules Rules         { get; }

    private int _currentHandIndex = 0;
    private bool _playing = true;
    private Card? _hiddenCard;

    public Table(Rules rules)
    {
        this.Rules = rules;
        InitializeShoe();
    }


    // START / END of shoe
    public void InitializeShoe()
    {
        EndRound();
        Shoe.ToDrawPile(Rules.DeckCount);
        Burned.ClearDeck();
        PlayerHands.Clear();
    }

    //
    public void AddPlayerHand(bool isSplitHand)
    {
        if (!IsPlaying()) return;
        PlayerHands.Add(new Hand(isSplitHand));
    }

    public void InitializeHands()
    {
        Card? faceCard = Shoe.DrawCard();
        DealerHand.AddCard(faceCard);
        Burned.AddCard(faceCard);
        _hiddenCard = Shoe.DrawCard();
        DealerHand.AddCard(_hiddenCard);
        
        AddPlayerHand(false);
        DrawCardPlayer();
        DrawCardPlayer();
        
    }
    public Hand DealerDrawCard()
    {
        _playing = false;
        Card? card = Shoe.DrawCard();
        Burned.AddCard(card);
        DealerHand.AddCard(card);
        return DealerHand;
    }
    
    public void EndRound()
    {
        if (_hiddenCard != null)
        {
            Burned.AddCard(_hiddenCard);
        }
        _hiddenCard = null;
        PlayerHands.Clear();
        DealerHand.ClearHand();
        _currentHandIndex = 0;
        _playing = true;
    }

    public void DrawCardPlayer()
    {
        if (!IsPlaying()) return;
        Card? card = Shoe.DrawCard();
        if (card != null)
        {
            Burned.AddCard(card);
            PlayerHands[_currentHandIndex].AddCard(card);
        }
        else
        {
            throw new NullReferenceException("Cannot draw card");
        }
    }

    public Hand GetCurrentHand()
    {
        IsPlaying();
        return PlayerHands[_currentHandIndex];
    }

    public void Stand()
    {
        if (!IsPlaying()) return;
        _currentHandIndex++;
    }

    public void DoubleDown()
    {
        if (!IsPlaying()) return;
        DrawCardPlayer();
        GetCurrentHand().DoubleDown();
        Stand();
    }

    public void Split()
    {
        if (!IsPlaying()) return;
        
        var cardToSplit = PlayerHands[_currentHandIndex].RemoveCard();
        PlayerHands[_currentHandIndex].IsSplitHand = true;
        AddPlayerHand(true);
        PlayerHands[PlayerHands.Count - 1].AddCard(cardToSplit);
    }

    public void RemoveCurrnetHand()
    {
        if (!IsPlaying()) return;
        PlayerHands.RemoveAt(_currentHandIndex);
    }
    
    

    public Card? GetDealerCard()
    {
        Card? card = DealerHand.Cards[0];
        return card;
    }

    public int GetDealerHandValue()
    {
        if (IsPlaying()) throw new Exception("Cannot get DealerHand when you are playing you cheater!!");
        return DealerHand.GetValue();
    }

    public bool IsDealerHandSoft()
    {
        return DealerHand.IsSoft();
    }

    public bool IsDealerHandBusted()
    {
        return DealerHand.IsBusted();
    }

    public bool IsPlayingRoundOver()
    {
        if (PlayerHands.Count > _currentHandIndex) return false;
        _playing = false;
        return true;
    }

    public bool ShouldShuffle()
    {
        if (Burned.GetDeckCount() > (Rules.DeckCount*52) * Rules.DeckPenetration)
        {
            return true;
        }
        return false;
    }

    public List<Hand>? GetPlayerHands()
    {
        return PlayerHands;
    }

    public List<Hand>? Insure()
    {
        if (!Rules.AllowInsurance)
        {
            Console.WriteLine("Insurance is not allowed");
            return null;
        }

        if (DealerHand.GetValue() == 21)
        {
            var playerHands = GetPlayerHands();
            EndRound();
            return playerHands;
        }
        return null;
    }

    private bool IsPlaying()
    {
        if (!_playing)
        {
            throw new Exception("Player is playing after round is over!!!!");
        }
        return _playing;
    }
}