namespace BlackJack.BlackJackGame.Strategies;

public class BasicStrategy : IStrategy
{
    public double DecideBet(Table table)
    {
        return 1;
    }

    public List<Actions> DecideHand(Table table)
    {
        return new List<Actions>() { Actions.Stand };
    }
}