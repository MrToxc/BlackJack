namespace BlackJack.BlackJackGame
{
    public class RulesBuilder
    {
        // --- Default values ---
        private bool _dealerHitsSoft17 = false;
        private bool _dealerPeeksForBlackjack = true;
        private double _blackjackPayout = 1.5;
        private int _maxSplits = 3;
        private bool _allowDoubleAfterSplit = true;
        private bool _allowResplitAces = true;
        private bool _allowHitSplitAces = true;
        //private bool _allowEarlySurrender = false;
        private bool _allowLateSurrender = true;
        private bool _allowInsurance = true;
        private bool _allowDoubleOnAnyTwo = true;
        private bool _allowDoubleOn9To11Only = false;
        private bool _allowDoubleOn10Or11Only = false;
        private int _deckCount = 8;
        private double _deckPenetration = 0.75;

        // --- Fluent setters ---
        public RulesBuilder SetDealerHitsSoft17(bool value) => Set(ref _dealerHitsSoft17, value);
        public RulesBuilder SetDealerPeeksForBlackjack(bool value) => Set(ref _dealerPeeksForBlackjack, value);
        public RulesBuilder SetBlackjackPayout(double value) => Set(ref _blackjackPayout, value);
        public RulesBuilder SetMaxSplits(int value) => Set(ref _maxSplits, value);
        public RulesBuilder SetAllowDoubleAfterSplit(bool value) => Set(ref _allowDoubleAfterSplit, value);
        public RulesBuilder SetAllowResplitAces(bool value) => Set(ref _allowResplitAces, value);
        public RulesBuilder SetAllowHitSplitAces(bool value) => Set(ref _allowHitSplitAces, value);
        //public RulesBuilder SetAllowEarlySurrender(bool value) => Set(ref _allowEarlySurrender, value);
        public RulesBuilder SetAllowLateSurrender(bool value) => Set(ref _allowLateSurrender, value);
        public RulesBuilder SetAllowDoubleOnAnyTwo(bool value) => Set(ref _allowDoubleOnAnyTwo, value);
        public RulesBuilder SetAllowDoubleOn9To11Only(bool value) => Set(ref _allowDoubleOn9To11Only, value);
        public RulesBuilder SetAllowDoubleOn10Or11Only(bool value) => Set(ref _allowDoubleOn10Or11Only, value);
        
        public RulesBuilder SetDeckCount(int value) => Set(ref _deckCount, value);
        public RulesBuilder SetDeckPenetration(double value) => Set(ref _deckPenetration, value);

        // --- Generic setter helper ---
        private RulesBuilder Set<T>(ref T field, T value)
        {
            field = value;
            return this;
        }

        // --- Build method with validation ---
        public Rules Build()
        {
            // --- Double rules conflict check ---
            int doubleRulesEnabled = 0;
            if (_allowDoubleOnAnyTwo) doubleRulesEnabled++;
            if (_allowDoubleOn9To11Only) doubleRulesEnabled++;
            if (_allowDoubleOn10Or11Only) doubleRulesEnabled++;

            if (doubleRulesEnabled > 1)
                throw new InvalidOperationException("Only one of the double rules can be enabled at a time.");
            
            // --- Penetration value check ---
            if (_deckPenetration <= 0.0 || _deckPenetration > 1.0)
                throw new InvalidOperationException("DeckPenetration must be greater than 0.0 and less than or equal to 1.0.");

            return new Rules(
                _dealerHitsSoft17,
                _dealerPeeksForBlackjack,
                _blackjackPayout,
                _maxSplits,
                _allowDoubleAfterSplit,
                _allowResplitAces,
                _allowHitSplitAces,
                //_allowEarlySurrender,
                _allowLateSurrender,
                _allowInsurance,
                _allowDoubleOnAnyTwo,
                _allowDoubleOn9To11Only,
                _allowDoubleOn10Or11Only,
                _deckCount,
                _deckPenetration
            );
        }
    }
}
