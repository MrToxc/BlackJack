namespace BlackJack.BlackJackGame;

public class GameManager
{
    private readonly Dealer _dealer;

    public GameManager(Dealer dealer)
    {
        _dealer = dealer;
    }

    public double Test()
    {
        double result = 0;
        for (int i = 0; i < 100000; i++)
        {
            Console.WriteLine(i);
            result += _dealer.PlayRound();
        }
        return result;
    }
}