using BlackJack.BlackJackGame.CardCreator;

namespace BlackJack.BlackJackGame;

public class Table
{
    private readonly Hand _dealerHand = new(false);

    private readonly List<Hand> _playerHands = new();
    
    private readonly Heap _shoe = new();
    private readonly Heap _burned = new();
    public readonly Rules Rules;

    private int _currentHandIndex;
    private bool _playing = true;
    private Card? _hiddenCard;

    public Table(Rules rules)
    {
        Rules = rules;
        InitializeShoe();
    }


    // START / END of shoe
    public void InitializeShoe()
    {
        Console.WriteLine("Initializing Shoe");
        EndRound();
        _shoe.ToDrawPile(Rules.DeckCount);
        _burned.ClearDeck();
        _playerHands.Clear();
    }

    //
    private void AddPlayerHand(bool isSplitHand)
    {
        if (!IsPlaying()) return;
        _playerHands.Add(new Hand(isSplitHand));
    }

    public void InitializeHands()
    {
        Card faceCard = _shoe.DrawCard();
        _dealerHand.AddCard(faceCard);
        _burned.AddCard(faceCard);
        _hiddenCard = _shoe.DrawCard();
        _dealerHand.AddCard(_hiddenCard);
        
        AddPlayerHand(false);
        DrawCardPlayer();
        DrawCardPlayer();
        
    }
    public Hand DealerDrawCard()
    {
        _playing = false;
        Card card = _shoe.DrawCard();
        _burned.AddCard(card);
        _dealerHand.AddCard(card);
        return _dealerHand;
    }
    
    public void EndRound()
    {
        if (_hiddenCard != null)
        {
            _burned.AddCard(_hiddenCard);
        }
        _hiddenCard = null;
        _playerHands.Clear();
        _dealerHand.ClearHand();
        _currentHandIndex = 0;
        _playing = true;
    }

    public void DrawCardPlayer()
    {
        if (!IsPlaying()) return;
        Card card = _shoe.DrawCard();
        _burned.AddCard(card);
        _playerHands[_currentHandIndex].AddCard(card);
    }

    public Hand GetCurrentHand()
    {
        IsPlaying();
        return _playerHands[_currentHandIndex];
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
        
        var cardToSplit = _playerHands[_currentHandIndex].RemoveCard();
        _playerHands[_currentHandIndex].IsSplitHand = true;
        AddPlayerHand(true);
        _playerHands[^1].AddCard(cardToSplit);
    }

    public void RemoveCurrnetHand()
    {
        if (!IsPlaying()) return;
        _playerHands.RemoveAt(_currentHandIndex);
    }
    
    

    public Card GetDealerCard()
    {
        Card card = _dealerHand.Cards[0];
        return card;
    }

    public int GetDealerHandValue()
    {
        if (IsPlaying()) throw new Exception("Cannot get DealerHand when you are playing you cheater!!");
        return _dealerHand.GetValue();
    }

    public bool IsDealerHandSoft()
    {
        return _dealerHand.IsSoft();
    }

    public bool IsDealerHandBusted()
    {
        return _dealerHand.IsBusted();
    }

    public bool IsPlayingRoundOver()
    {
        if (_playerHands.Count > _currentHandIndex) return false;
        _playing = false;
        return true;
    }

    public bool ShouldShuffle()
    {
        return _burned.Count > (Rules.DeckCount*52) * Rules.DeckPenetration;
    }

    public List<Hand> GetPlayerHands()
    {
        return _playerHands;
    }

    /*
    public List<Hand>? Insure()
    {
        if (!Rules.AllowInsurance)
        {
            Console.WriteLine("Insurance is not allowed");
            return null;
        }

        if (_dealerHand.GetValue() == 21)
        {
            var playerHands = GetPlayerHands();
            EndRound();
            return playerHands;
        }
        return null;
    }
    */

    private bool IsPlaying()
    {
        return _playing;
    }
}