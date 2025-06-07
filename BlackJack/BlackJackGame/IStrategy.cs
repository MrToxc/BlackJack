namespace BlackJack.BlackJackGame;

public interface IStrategy
{

    double DecideBet(Table table);


    List<Actions> DecideHand(Table table);
}