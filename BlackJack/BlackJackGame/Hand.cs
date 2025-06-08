using BlackJack.BlackJackGame.CardCreator;

namespace BlackJack.BlackJackGame;

public class Hand(bool isSplitHand)
{
    private readonly List<Card> _cards = new();
    public bool IsSplitHand = isSplitHand;

    public bool IsDoubledDown;
    
    public IReadOnlyList<Card> Cards => _cards;

    public void AddCard(Card card)
    {
        if (GetValue() > 21)
        {
            foreach (var card1 in _cards)
            {
                Console.Write(card1 + "///");
            }
            throw new InvalidOperationException("Hand is too large. you hit when you were already busted");
        }
        _cards.Add(card);
    }

    public Card RemoveCard()
    {
        if (_cards.Count != 2)
        {
            throw new InvalidOperationException("Cannot remove card from hand. Number of cards must be 2");
        }
        Card card = _cards[1];
        _cards.RemoveAt(1);
        return card;
    }
    
    public int GetValue()
    {
        int total = 0;
        int aceCount = 0;

        foreach (var card in _cards)
        {
            var rank = card.GetCardValue();

            if (rank == 'A')
            {
                total += 11;
                aceCount++;
            }
            else if ("TJQK".Contains(rank))
            {
                total += 10;
            }
            else
            {
                total += rank - '0'; // '2' až '9' Unicode na char 0 je 48 a na 1 je 49, proto to funguje
            }
        }

        while (total > 21 && aceCount > 0)
        {
            total -= 10;
            aceCount--;
        }

        return total;
    }

    public void ClearHand()
    {
        _cards.Clear();
    }

    
    
    public bool IsSoft()
    {
        int total = 0;
        int aceCount = 0;

        foreach (var card in _cards)
        {
            char rank = card.GetCardValue();

            if (rank == 'A')
            {
                total += 11;
                aceCount++;
            }
            else if ("TJQK".Contains(rank))
            {
                total += 10;
            }
            else
            {
                total += rank - '0';
            }
        }

        // Odečti 10 za každé eso, které by způsobilo bust
        int adjustedTotal = total;
        int adjustedAces = aceCount;

        while (adjustedTotal > 21 && adjustedAces > 0)
        {
            adjustedTotal -= 10;
            adjustedAces--;
        }

        // Pokud po korekci zůstalo alespoň jedno eso jako 11 → soft hand
        return aceCount > 0 && adjustedAces > 0;
    }
    /*
    public bool IsSoft()
    {
        int total = 0;
        int aceCount = 0;

        foreach (var card in _hand)
        {
            switch (card.GetCardValue())
            {
                case 'A':
                    total += 11;
                    aceCount++;
                    break;
                case 'K':
                case 'Q':
                case 'J':
                case 'T':
                    total += 10;
                    break;
                default:
                    total += card.GetCardValue() - '0';
                    break;
            }
        }

        // Snižujeme esa jen pokud přesahujeme 21
        int adjustedAces = aceCount;
        while (total > 21 && adjustedAces > 0)
        {
            total -= 10;
            adjustedAces--;
        }

        // Pokud nám aspoň jedno eso zůstalo jako 11, je ruka soft
        return adjustedAces > 0;
    }
*/
    
    public bool IsBlackJack()
    {
        return _cards.Count == 2 && GetValue() == 21;
    }

    public bool IsBusted()
    {
        return GetValue() > 21;
    }

    public bool CanSplit()
    {
        return HasTwoCards() && _cards[0].GetCardValue() == _cards[1].GetCardValue();
    }

    public bool HasTwoCards()
    {
        return _cards.Count == 2;
    }

    public void DoubleDown()
    {
        IsDoubledDown = true;
    }
}