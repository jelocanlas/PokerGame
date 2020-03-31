using System.ComponentModel;

namespace PokerGameWorkTest
{
    public class Card
    {
        public CardSuit Suit { get; set; }
        public CardValue Value { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}{1}]", Value.Description(), Suit.Description());
        }

        public enum CardValue
        {
            [Description("2")]
            Two,
            [Description("3")]
            Three,
            [Description("4")]
            Four,
            [Description("5")]
            Five,
            [Description("6")]
            Six,
            [Description("7")]
            Seven,
            [Description("8")]
            Eight,
            [Description("9")]
            Nine,
            [Description("10")]
            Ten,
            [Description("J")]
            Jack,
            [Description("Q")]
            Queen,
            [Description("K")]
            King,
            [Description("A")]
            Ace,
            [Description("W")]
            WildCard
        }

        public enum CardSuit
        {
            [Description("S")]
            Spades,
            [Description("D")]
            Diamonds,
            [Description("H")]
            Hearts,
            [Description("C")]
            Clubs
        }
    }
}
