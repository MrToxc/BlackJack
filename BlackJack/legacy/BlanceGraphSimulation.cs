namespace BlackJack;

public class BlanceGraphSimulation
{
    public BlanceGraphSimulation()
    {
    }

    public bool RunSimulation(double winRate, double bankroll, double goal)
    {
        if (winRate is > 100 or < 0)
        {
            throw new ArgumentOutOfRangeException("winRate must be between 0 and 100");
        }
        double loseRate = 100.0 - winRate;
        return false;
    }
}