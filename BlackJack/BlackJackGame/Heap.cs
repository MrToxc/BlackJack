using BlackJack.BlackJackGame.CardCreator;

namespace BlackJack.BlackJackGame;

public class Heap
{
    private List<Card> _heap = new List<Card>();
    private int _deckCount;
    private static Random _rng = new Random();
    

    public void ToDrawPile(int deckCount)
    {
        if (deckCount < 1)
        {
            throw new ArgumentException("The deck count must be greater than 0.");
        }
        _heap.Clear();
        _deckCount = deckCount;
        Innit();
        Shuffle(_heap);
    }

    public void AddCard(Card card)
    {
        _heap.Add(card);
    }

    public Card? DrawCard()
    {
        if (_heap.Count == 0)
        {
            return null;
        }
        var card = _heap[_heap.Count - 1];
        _heap.RemoveAt(_heap.Count - 1);
        return card;
    }

    public bool ShouldReshuffle(double penetration)
    {
        if (_deckCount == 0)
            return true; // preventivní ochrana

        double ratioPlayed = 1.0 - ((double)_heap.Count / (_deckCount*52));
        return ratioPlayed >= penetration;
    }
    
    public int GetRemainingCardCount()
    {
        return _heap.Count;
    }

    public int GetDeckCount()
    {
        return _deckCount;
    }

    private void Innit()
    {
        List<char> cardSet = new List<char> { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };
        for (int i = 0; i < _deckCount; i++)
        {
            foreach (var card in cardSet)
            {
                foreach (Color color in Enum.GetValues<Color>())
                {
                    _heap.Add(new Card(card, color));
                }
            }
        }
    }

    private void Shuffle(List<Card> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = _rng.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}