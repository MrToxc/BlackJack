using BlackJack.BlackJackGame.CardCreator;

namespace BlackJack.BlackJackGame;

public class Heap
{
    private static readonly Random Rng = new();
    private readonly List<Card> _heap = new();
    
    
    public void ToDrawPile(int deckCount)
    {
        if (deckCount < 1)
        {
            throw new ArgumentException("The deck count must be greater than 0.");
        }
        _heap.Clear();
        Innit(deckCount);
    }

    public void AddCard(Card card)
    {
        _heap.Add(card);
    }

    public void ClearDeck()
    {
        _heap.Clear();
    }

    public Card DrawCard()
    {
        if (_heap.Count == 0)
        {
            throw new InvalidOperationException("The heap is empty.");
        }
        var card = _heap[^1];
        _heap.RemoveAt(_heap.Count - 1);
        return card!;
    }
    
    
    public int Count => _heap.Count;

    private void Innit(int deckCount)
    {
        List<char> cardSet = new List<char> { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };
        for (int i = 0; i < deckCount; i++)
        {
            foreach (var card in cardSet)
            {
                foreach (Color color in Enum.GetValues<Color>())
                {
                    _heap.Add(new Card(card, color));
                }
            }
        }
        Shuffle(_heap);
    }

    private void Shuffle(List<Card> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Rng.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}