namespace BlackJack.BlackJackGame;

public class Dealer(Table table, Player player)
{
    private readonly Rules _rules = table.Rules;
    private double _betAmount;
    private double _payoutAmount;

    public double PlayRound()
    {
        double betAmount = player.DecideBet();
        ArgumentOutOfRangeException.ThrowIfNegative(betAmount);
        _payoutAmount = 0;
        
        _betAmount = betAmount;
        
        if (table.ShouldShuffle()) table.InitializeShoe();
        table.EndRound();
        table.InitializeHands();
        if (table.GetCurrentHand().IsBlackJack())
        {
            BlackJack();
            return _payoutAmount;
        }

        PlayHands();
        DealerDrawCards();
        PayOut();
        table.EndRound();
        return _payoutAmount;
    }

    private void PayOut()
    {
        var dealerValue = table.GetDealerHandValue();
        var dealerBusted = table.IsDealerHandBusted();

        foreach (var playerHand in table.GetPlayerHands())
        {
            int playerValue = playerHand.GetValue();

            if (dealerBusted)
            {
                Win(playerHand.IsDoubledDown);
            }
            else if (playerValue > dealerValue)
            {
                Win(playerHand.IsDoubledDown);
            }
            else if (playerValue < dealerValue)
            {
                Lose(playerHand.IsDoubledDown);
            }
            else
            {
                Push();
            }
        }
    }


    private void DealerDrawCards()
    {
        while (true)
        {
            int value = table.GetDealerHandValue();
            bool isSoft = table.IsDealerHandSoft();

            if (value < 17 || (value == 17 && isSoft && _rules.DealerHitsSoft17))
            {
                table.DealerDrawCard();
                continue;
            }
            break;
        }
    }


    private void PlayHands()
    {
        while (!table.IsPlayingRoundOver())
        {
            if (table.GetCurrentHand().IsBusted())
            {
                Lose(table.GetCurrentHand().IsDoubledDown);
                table.RemoveCurrnetHand();
                continue;
            }
            foreach (var action in player.DecideAction())
            {
                if (IsPossible(action))
                {
                    switch (action)
                    {
                        case Actions.Hit:
                            Hit();
                            break;
                        case Actions.Stand:
                            Stand();
                            break;
                        case Actions.Double:
                            DoubleDown();
                            break;
                        case Actions.Split:
                            Split();
                            break;
                        case Actions.Surrender:
                            Surrender();
                            break;
                        default:
                            throw new Exception("Unknown action");
                    }

                    break;
                }
            }
        }
    }
    
    
    
    //Payouts
    private void BlackJack()
    {
        _payoutAmount += _betAmount * _rules.BlackjackPayout;
    }

    private void Push()
    {
        _payoutAmount += 0;
    }

    private void Win(bool doubled)
    {
        if (doubled) _payoutAmount += _betAmount;
        _payoutAmount += _betAmount;
    }

    private void Lose(bool doubled)
    {
        if (doubled) _payoutAmount -= _betAmount;
        _payoutAmount -= _betAmount;
    }
    //Payouts

    private bool IsPossible(Actions action)
    {
        if (action is Actions.Hit or Actions.Stand) return true;
        
        if (!table.GetCurrentHand().HasTwoCards()) return false;

        if (action is Actions.Double)
        {
            if (_rules.AllowDoubleOnAnyTwo)
            {
                return true;
            }

            if (_rules.AllowDoubleOn9To11Only 
                && table.GetCurrentHand().GetValue() >= 9 
                && table.GetCurrentHand().GetValue() <= 11)
            {
                return true;
            }

            return _rules.AllowDoubleOn10Or11Only
                   && table.GetCurrentHand().GetValue() >= 10
                   && table.GetCurrentHand().GetValue() <= 11;
        }
        if (action is Actions.Surrender && _rules.AllowLateSurrender) return true;
        if (action is Actions.Split && table.GetCurrentHand().CanSplit()) return true;
        return false;
    }
    
    //Game options
    private void Hit()
    {
        table.DrawCardPlayer();
    }

    private void Stand()
    {
        table.Stand();
    }

    private void DoubleDown()
    {
        table.DoubleDown();
    }

    private void Split()
    {
        table.Split();
    }

    private void Surrender()
    {
        table.RemoveCurrnetHand();
        _payoutAmount += (_betAmount / 2) * -1;
    }
    
    
    
    
    
}