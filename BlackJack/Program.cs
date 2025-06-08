using BlackJack.BlackJackGame;
using BlackJack.BlackJackGame.Strategies;

var rules   = new RulesBuilder().Build();
var table   = new Table(rules);
var player  = new Player(table, new BasicStrategy());
var dealer  = new Dealer(table, player);
var manager = new GameManager(dealer);

Console.WriteLine(manager.Test());