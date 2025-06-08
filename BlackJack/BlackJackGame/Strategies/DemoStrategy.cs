namespace BlackJack.BlackJackGame.Strategies;

public class DemoStrategy : IStrategy
{
    private const double BaseBet = 1.0;

    public double DecideBet(Table table)
    {
        return BaseBet;
    }

    public List<Actions> DecideHand(Table table)
    {
        if (table.GetCurrentHand().GetValue() == 11)
        {
            return new List<Actions>() {Actions.Double, Actions.Hit };
        }
        if (table.GetCurrentHand().GetValue() < 11)
        {
            return new List<Actions>() {Actions.Split, Actions.Hit };
        }
        return new List<Actions>() {Actions.Split, Actions.Stand };
    }
}