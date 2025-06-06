namespace BlackJack.BlackJackGame;

public class Dealer
{
    private Table _table;
    private bool _stopPlaying = false;
    private Rules _rules;
    private double _betAmount = 0;

    public Dealer(Table table)
    {
        _table = table;
        _rules = table.Rules;
    }

    
    public double PlayHand(double betAmount)
    {
        double payout = 0;
        _betAmount = betAmount;
        
        if (_table.ShouldShuffle())
        {
            _table.InitializeRound();
        }
        _table.InitializeHands();
        
        if (_table.GetCurrentHand().IsBlackJack())
        {
            return BlackJack();
        }
        
        

        
        

        return 0;
    }

    
    
    
    //Payouts
    private double BlackJack()
    {
        return _betAmount + _betAmount * _rules.BlackjackPayout;
    }

    private double Push()
    {
        return _betAmount;
    }

    private double Win()
    {
        return _betAmount * 2;
    }
    //Payouts
    
    
    
}