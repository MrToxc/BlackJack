namespace BlackJack.BlackJackGame;

public class Player
{
    private Table _table;
    private IStrategy _strategy;

    public Player(Table table, IStrategy strategy)
    {
        _table = table;
        _strategy = strategy;
    }

    public List<Actions> DecideAction()
    {
        return _strategy.DecideHand(_table);
    }

    public double DecideBet()
    {
        return _strategy.DecideBet(_table);
    }
}