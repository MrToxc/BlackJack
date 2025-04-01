// See https://aka.ms/new-console-template for more information

using BlackJack;

Console.WriteLine("Hello, World!");
CardStorage cardStorage = new CardStorage(8);

for (int i = 0; i < 100; i++)
{
    cardStorage.Play(60);
}
