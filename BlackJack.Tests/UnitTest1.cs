using BlackJack.BlackJackGame;
using BlackJack.BlackJackGame.CardCreator;
using BlackJack.BlackJackGame.Strategies;

namespace BlackJack.Tests;
static class CardHelper
{
    public static Card C(char rank) => new Card(rank, Color.Clubs);
}


public class UnitTest1
{
    [Fact]
    public void GetValue_AceNineAce_Returns21_AndIsSoft()
    {
        var hand = new Hand(false);
        hand.AddCard(CardHelper.C('A'));
        hand.AddCard(CardHelper.C('9'));
        hand.AddCard(CardHelper.C('A'));

        Assert.Equal(21, hand.GetValue());
        Assert.True(hand.IsSoft());
    }

    [Fact]
    public void IsBusted_WithNineNineFive_ReturnsTrue()
    {
        var hand = new Hand(false);
        hand.AddCard(CardHelper.C('9'));
        hand.AddCard(CardHelper.C('9'));
        hand.AddCard(CardHelper.C('5'));

        Assert.True(hand.IsBusted());
    }

    [Theory]
    [InlineData('A', 'K')]
    [InlineData('K', 'A')]
    public void IsBlackJack_TwoCard21_ReturnsTrue(char first, char second)
    {
        var hand = new Hand(false);
        hand.AddCard(CardHelper.C(first));
        hand.AddCard(CardHelper.C(second));

        Assert.True(hand.IsBlackJack());
    }

    [Fact]
    public void RemoveCard_WithMoreThanTwoCards_Throws()
    {
        var hand = new Hand(false);
        hand.AddCard(CardHelper.C('9'));
        hand.AddCard(CardHelper.C('9'));
        hand.AddCard(CardHelper.C('2'));

        Assert.Throws<InvalidOperationException>(() => hand.RemoveCard());
    }
}

// ------------------------------------------------------------
// 5‑6  ► RulesBuilder validation logic
// ------------------------------------------------------------
public class RulesBuilderTests
{
    [Fact]
    public void Build_ConflictingDoubleRules_Throws()
    {
        var builder = new RulesBuilder()
            .SetAllowDoubleOnAnyTwo(true)
            .SetAllowDoubleOn10Or11Only(true); // konflikt

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Theory]
    [InlineData(0)]    // zero penetration
    [InlineData(1.5)]  // > 1.0 penetration
    public void Build_InvalidPenetration_Throws(double penetration)
    {
        var builder = new RulesBuilder()
            .SetDeckPenetration(penetration);

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }
}

// ------------------------------------------------------------
// 7‑8  ► Heap & Table behaviour
// ------------------------------------------------------------
public class HeapAndTableTests
{
    [Fact]
    public void DrawCard_OnEmptyHeap_Throws()
    {
        var heap = new Heap();
        Assert.Throws<InvalidOperationException>(() => heap.DrawCard());
    }

    [Fact]
    public void ShouldShuffle_ReturnsTrue_AfterPenetrationReached()
    {
        var rules = new RulesBuilder().Build();
        var table = new Table(rules);

        // táhni dokud nedojde k přepnutí ShouldShuffle = true
        while (!table.ShouldShuffle())
        {
            table.InitializeHands();
            table.EndRound();
        }

        Assert.True(table.ShouldShuffle());
    }
}

// ------------------------------------------------------------
// 9‑10  ► Light integration tests with Dealer/Player/Game flow
// ------------------------------------------------------------
public class IntegrationTests
{
    [Fact]
    public void Dealer_PlayRound_ReturnsPayoutWithinBounds()
    {
        var rules = new RulesBuilder().Build();
        var table = new Table(rules);
        var player = new Player(table, new DemoStrategy());
        var dealer = new Dealer(table, player);

        double payout = dealer.PlayRound();

        // Sázka v DemoStrategy je 1 => očekávaný rozsah výsledku je přibližně -2 až +1.5
        Assert.InRange(payout, -5.0, 5.0);
    }

    [Fact]
    public void Player_DecideBet_IsPositive()
    {
        var rules = new RulesBuilder().Build();
        var table = new Table(rules);
        var strategy = new DemoStrategy();
        var player = new Player(table, strategy);

        Assert.True(player.DecideBet() > 0);
    }
}
