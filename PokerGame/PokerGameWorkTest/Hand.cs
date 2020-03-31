using System.ComponentModel;

namespace PokerGameWorkTest
{
    public class Hand
    {
        public string Description { get; set; }
        public float Strength { get; set; }
        public string Rank { get; set; }

        public enum HandCategory
        {
            [Description("Not Set")]
            NotSet,
            [Description("High Card")]
            HighCard,
            [Description("Pair")]
            Pair,
            [Description("Two Pair")]
            TwoPair,
            [Description("Three of a Kind")]
            ThreeOfAKind,
            [Description("Straight")]
            Straight,
            [Description("Flush")]
            Flush,
            [Description("Full House")]
            FullHouse,
            [Description("Four of a Kind")]
            FourOfAKind,
            [Description("Straight Flush")]
            StraightFlush,
            [Description("Royal Flush")]
            RoyalFlush
        }
    }
}
