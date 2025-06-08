namespace BlackJack.BlackJackGame;

public interface IStrategy
{
    /// <summary>
    /// Decide the size of the next bet based on the current table state.
    /// </summary>
    double DecideBet(Table table);


    /// <summary>
    /// Return a ranked list of possible actions from best to worst for the current hand.
    /// The dealer will choose the highest-ranked legal action.
    /// </summary>
    List<Actions> DecideHand(Table table);
}