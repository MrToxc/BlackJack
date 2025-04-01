namespace BlackJack;

public class CardStorage
{
    //types
    List<char> cardSet = new List<char> { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };

    
    //accuall cards
    List<char> deck = new List<char>();
    
    //been played
    List<char> delt = new List<char>();
    public int runningCount = 0;
    public double highestTC = -100.0;
    private int deckCount;

    
    private static Random rng = new Random(); // shared RNG
    
    public CardStorage(int deckCount)
    {
        this.deckCount = deckCount;
        foreach (var card in cardSet)
        {
            for (int i = 0; i < 4*deckCount; i++)
            {
                deck.Add(card);
            }
        } 
        Shuffle(deck);
    }

    private void Shuffle(List<char> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
    
    private void playCard()
    {
        if (deck.Count == 0)
        {
            return; // nothing to play
        }
        if (deck[0] == '2' || deck[0] == '3' || deck[0] == '4' || deck[0] == '5' || deck[0] == '6')
        {
            runningCount++;
        } else if (deck[0] == 'T' || deck[0] == 'J' || deck[0] == 'Q' || deck[0] == 'K' || deck[0] == 'A')
        {
            runningCount--;
        }
        delt.Add(deck[0]);
        deck.RemoveAt(0);
    }

    public void Play(int deckPen)
    {
        if (deckPen > 100 || deckPen < 0)
        {
            return;
        }
        double multiplier = deckPen / 100.0;
        int cardsBeforeReshuffle = (int)(deck.Count * multiplier);

        for (int i = 0; i < cardsBeforeReshuffle; i++)
        {
            playCard();
            getTrueCount();
            //Console.WriteLine(getTrueCount());
        }
        Console.WriteLine(highestTC + "");
        Reset();
    }

    private void Reset()
    {
        deck.Clear();
        delt.Clear();
        runningCount = 0;
        highestTC = -100.0;

        // Rebuild the deck from cardSet (same as in constructor)
        foreach (var card in cardSet)
        {
            for (int i = 0; i < 4 * deckCount; i++)
            {
                deck.Add(card);
            }
        }

        Shuffle(deck);
    }

    private double getTrueCount()
    {
        double trueCount = runningCount * 1.0 / (1.0 * deck.Count / 52.0);
        if (trueCount > highestTC)
        {
            highestTC = trueCount;
        }

        return trueCount;
    }
}