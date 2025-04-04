namespace BlackJack.BlackJackGame
{
    public class Rules
    {
        // --- Dealer behavior ---
        public bool DealerHitsSoft17 { get; }
        public bool DealerPeeksForBlackjack { get; }

        // --- Blackjack payout ---
        public double BlackjackPayout { get; }

        // --- Splitting rules ---
        public int MaxSplits { get; }
        public bool AllowDoubleAfterSplit { get; }
        public bool AllowResplitAces { get; }
        public bool AllowHitSplitAces { get; }

        // --- Surrender rules ---
        public bool AllowEarlySurrender { get; }
        public bool AllowLateSurrender { get; }

        // --- Doubling rules ---
        public bool AllowDoubleOnAnyTwo { get; }
        public bool AllowDoubleOn9To11Only { get; }
        public bool AllowDoubleOn10Or11Only { get; }

        // --- Penetration ---
        public double DeckPenetration { get; }

        internal Rules(
            bool dealerHitsSoft17,
            bool dealerPeeksForBlackjack,
            double blackjackPayout,
            int maxSplits,
            bool allowDoubleAfterSplit,
            bool allowResplitAces,
            bool allowHitSplitAces,
            bool allowEarlySurrender,
            bool allowLateSurrender,
            bool allowDoubleOnAnyTwo,
            bool allowDoubleOn9To11Only,
            bool allowDoubleOn10Or11Only,
            double deckPenetration)
        {
            DealerHitsSoft17 = dealerHitsSoft17;
            DealerPeeksForBlackjack = dealerPeeksForBlackjack;
            BlackjackPayout = blackjackPayout;
            MaxSplits = maxSplits;
            AllowDoubleAfterSplit = allowDoubleAfterSplit;
            AllowResplitAces = allowResplitAces;
            AllowHitSplitAces = allowHitSplitAces;
            AllowEarlySurrender = allowEarlySurrender;
            AllowLateSurrender = allowLateSurrender;
            AllowDoubleOnAnyTwo = allowDoubleOnAnyTwo;
            AllowDoubleOn9To11Only = allowDoubleOn9To11Only;
            AllowDoubleOn10Or11Only = allowDoubleOn10Or11Only;
            DeckPenetration = deckPenetration;
        }
    }
}
