using System.Diagnostics;

namespace BlackJack.BlackJackGame;

public class Dealer
{
    private Table _table ;
    private bool _stopPlaying = false;
    private Rules _rules;
    private double _betAmount = 0;
    private double _payoutAmount = 0;
    private Player _player;
    

    public Dealer(Rules rules, Player player)
    {
        _rules = rules;
        _table = new Table(rules);
        _player = player;
    }

    
    public double PlayHand(double betAmount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(betAmount);
        _payoutAmount = 0;
        
        _betAmount = betAmount;
        
        if (_table.ShouldShuffle()) _table.InitializeShoe();
        _table.InitializeHands();
        if (_table.GetCurrentHand().IsBlackJack())
        {
            BlackJack();
            return _payoutAmount;
        }

        PlayHands();
        DealerDrawCards();
        PayOut();
        _table.EndRound();
        
        
        
        
        
        

        
        
        
        return _payoutAmount;
    }

    private void PayOut()
    {
        int dealerValue = _table.GetDealerHandValue();
        bool dealerBusted = _table.IsDealerHandBusted();

        foreach (var playerHand in _table.GetPlayerHands())
        {
            int playerValue = playerHand.GetValue();

            if (dealerBusted)
            {
                Win(playerHand.isDoubledDown);
            }
            else if (playerValue > dealerValue)
            {
                Win(playerHand.isDoubledDown);
            }
            else if (playerValue < dealerValue)
            {
                Lose(playerHand.isDoubledDown);
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
            int value = _table.GetDealerHandValue();
            bool isSoft = _table.IsDealerHandSoft();

            if (value < 17 || (value == 17 && isSoft && _rules.DealerHitsSoft17))
            {
                _table.DealerDrawCard();
                continue;
            }
            break;
        }
    }


    private void PlayHands()
    {
        while (!_table.IsPlayingRoundOver())
        {
            if (_table.GetCurrentHand().IsBusted())
            {
                Lose(_table.GetCurrentHand().isDoubledDown);
                _table.RemoveCurrnetHand();
                continue;
            }
            foreach (var action in _player.DecideAction(_table))
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
        
        if (!_table.GetCurrentHand().HasTwoCards()) return false;

        if (action is Actions.Double)
        {
            if (_rules.AllowDoubleOnAnyTwo)
            {
                return true;
            }

            if (_rules.AllowDoubleOn9To11Only 
                && _table.GetCurrentHand().GetValue() >= 9 
                && _table.GetCurrentHand().GetValue() <= 11)
            {
                return true;
            }

            return _rules.AllowDoubleOn10Or11Only
                   && _table.GetCurrentHand().GetValue() >= 10
                   && _table.GetCurrentHand().GetValue() <= 11;
        }
        if (action is Actions.Surrender && _rules.AllowLateSurrender) return true;
        if (action is Actions.Split && _table.GetCurrentHand().CanSplit()) return true;
        throw new Exception("Invalid action");
    }
    
    //Game options
    private void Hit()
    {
        _table.DrawCardPlayer();
    }

    private void Stand()
    {
        _table.Stand();
    }

    private void DoubleDown()
    {
        _table.DoubleDown();
    }

    private void Split()
    {
        _table.Split();
    }

    private void Surrender()
    {
        _table.RemoveCurrnetHand();
        _payoutAmount += (_betAmount / 2) * -1;
    }
    
    
    
    
    
}