using BlackJack.BlackJackGame;
using BlackJack.BlackJackGame.Strategies;

var rules   = new RulesBuilder().Build();
var table   = new Table(rules);
var player  = new Player(table, new DemoStrategy());
var dealer  = new Dealer(table, player);
var manager = new GameManager(dealer);

Console.WriteLine("S pocatkem 0 je vysledek testu: " + manager.Test());