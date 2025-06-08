# Blackjack Simulation Tool

A high-speed, **extensible** blackjack simulator for advantage players, card-counting researchers, and anyone who wants to test custom strategies under laboratory-style conditions.
GitHub repo ▶ **[https://github.com/MrToxc/BlackJack](https://github.com/MrToxc/BlackJack)**

---

## Features

| Category                            | What you get                                                                         |
| ----------------------------------- | ------------------------------------------------------------------------------------ |
| 🛠 **Fully customizable rules**     | Deck count • penetration • S17/H17 • DAS/Split limits • blackjack payout • side bets |
| 📊 **Strategy performance testing** | Built-in popular counting systems + open interface for your own logic                |
| ⚡ **Extreme simulation speed**      | Millions of hands in seconds – variance practically evaporates                       |
| 🔌 **Plug-in architecture**         | Inject any `IStrategy` implementation or add new rule modules                        |
| 🧪 **Unit-tested core**             | xUnit project with key tests covering hand logic, rules builder and game flow        |

---

## Intended Audience

* Practising **advantage players (APs)** who want to measure EV before they sit at a table.
* **Analysts & developers** building new betting/chip-spreading approaches.
* Curious coders interested in card-game simulations.

If you are not exploring AP techniques or algorithmic decision making, this tool may be over-kill.

---

## Example Use Cases

* Compare Hi-Lo vs. KO vs. custom side-count under 6-deck S17 conditions
* Quantify EV change when penetration drops from 75 % to 50 %
* Stress-test an aggressive bet-spread versus heat (evaluate risk of ruin)

---

### Writing Your Own Strategy – Interface & Template

```csharp
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
```

```csharp
public class MyCustomStrategy : IStrategy
{
    public double DecideBet(Table table)
    {
        // Your bet-sizing logic, e.g., based on true count or bankroll
        // return a bet amount (e.g., unit size * count)
    }

    public List<Actions> DecideHand(Table table)
    {
        // Evaluate the hand and table state, then return Actions in descending preference,
        // e.g., [Actions.Stand, Actions.Double, Actions.Hit, Actions.Split]
    }
}
```

Compile, plug into the simulation, and the Dealer will execute your top-ranked action for each decision.

## Tests

The BlackJack.Tests project (xUnit) ships with ten foundational tests:

* Hand value & soft-ace logic
* Blackjack detection & bust handling
* RulesBuilder validation
* Dealer drawing on S17/H17
* Shoe penetration reshuffle trigger
* Smoke test for a full single round

Run them any time with `dotnet test` to verify that your changes haven’t broken core mechanics.
