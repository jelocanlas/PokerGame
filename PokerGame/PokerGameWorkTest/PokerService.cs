using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static PokerGameWorkTest.Card;
using static PokerGameWorkTest.Hand;

namespace PokerGameWorkTest
{
    public static class PokerService
    {
        public static Hand EvaluateHand(string playerName, List<string> cards)
        {
            var hand = new Hand();

            List<int> cardValueList = new List<int>();
            List<CardValue> cardValues = new List<CardValue>();
            List<string> suits = new List<string>();

            foreach (var card in cards)
            {
                cardValues.Add(!String.IsNullOrWhiteSpace(card) && card.Length > 2
                ? GetCardValue(card.Substring(0, card.Length - 1))
                : GetCardValue(card[0].ToString()));
                suits.Add(card.Last().ToString());
            }

            cardValueList = cardValues.Select(c => (int)c).ToList();

            var isInvalidHand = cardValueList.Contains((int)CardValue.WildCard);

            if (isInvalidHand)
            {
                throw new Exception(playerName + " has an invalid hand");
            }

            var handCategory = HandCategory.NotSet;
            string handDescription = string.Empty;

            int primaryAddition = 0;
            int secondaryAddition = 0;
            int terciaryAddition = 0;
            int terciary2Addition = 0;
            int terciary3Addition = 0;

            int highCard = cardValueList.Max();
            bool isStraight = ((cardValueList.Max() - cardValueList.Min() == 4) || 
                (cardValueList.Max() == (int)CardValue.Ace &&
                cardValueList.Contains((int)CardValue.Two) &&
                cardValueList.Contains((int)CardValue.Three) &&
                cardValueList.Contains((int)CardValue.Four) &&
                cardValueList.Contains((int)CardValue.Five))
                );
            bool isFlush = suits.All(x => x == suits.First());

            if (isStraight && isFlush)
            {
                // Straight Flush & Royal Flush
                if ((CardValue)highCard == CardValue.Ace && !cardValueList.Contains((int)CardValue.King))
                {
                    //Possibility of low straight
                    highCard = cardValueList.OrderByDescending(r => r).Skip(1).FirstOrDefault();
                    primaryAddition = highCard;
                    handCategory = HandCategory.StraightFlush;
                    handDescription = "Straight Flush to " + (CardValue)highCard;
                }
                else if(!((CardValue)highCard == CardValue.Ace))
                {
                    primaryAddition = highCard;
                    handCategory = HandCategory.StraightFlush;
                    handDescription = "Straight Flush to " + (CardValue)highCard;
                }
                else
                {
                    primaryAddition = highCard;
                    handCategory = HandCategory.RoyalFlush;
                    handDescription = "Royal Flush";
                }
            }

            else if (isFlush)
            {
                // Flush
                handCategory = HandCategory.Flush;
                primaryAddition = highCard;
                handDescription = (CardValue)highCard + " high flush.";
            }
            else if (isStraight)
            {
                // Straight
                if ((CardValue)highCard == CardValue.Ace && !cardValueList.Contains((int)CardValue.King))
                {
                    highCard = cardValueList.OrderByDescending(r => r).Skip(1).FirstOrDefault();
                }

                handCategory = HandCategory.Straight;
                primaryAddition = highCard;
                handDescription = "Straight to " + (CardValue)highCard;
            }

            // Pair
            var groups = cardValueList.GroupBy(x => x).ToList();
            bool hasPairing = groups.Any(g => g.Count() > 1);
            if (hasPairing)
            {
                var fourOfAKind = groups.FirstOrDefault(g => g.Count() == 4);
                var threeOfAKind = groups.FirstOrDefault(g => g.Count() == 3);
                var pairs = groups.Where(g => g.Count() == 2).ToList();

                if (fourOfAKind != null)
                {
                    // 4 of a kind
                    handCategory = HandCategory.FourOfAKind;
                    // value of 4 of a kind card
                    primaryAddition = fourOfAKind.Key;
                    // 5th card
                    secondaryAddition = cardValueList.Where(x => x != fourOfAKind.Key).Max();
                    handDescription = string.Format("Four of a Kind, {0}s", (CardValue)fourOfAKind.Key);
                }
                else if (threeOfAKind != null && pairs.Any())
                {
                    handCategory = HandCategory.FullHouse;
                    // value of trips
                    primaryAddition = threeOfAKind.Key;
                    // value of pair
                    secondaryAddition = pairs.First().Key;
                    handDescription = string.Format("Full House, {0}s over {1}s", (CardValue)threeOfAKind.Key, (CardValue)pairs.First().Key);
                }
                else if (threeOfAKind != null)
                {
                    // Three of a kind
                    handCategory = HandCategory.ThreeOfAKind;
                    // three of a kind card value
                    primaryAddition = threeOfAKind.Key;
                    // high kicker
                    secondaryAddition = cardValueList.Where(x => x != threeOfAKind.Key).Max();
                    // other kicker
                    terciaryAddition = cardValueList.Where(x => x != threeOfAKind.Key).Min();
                    handDescription = string.Format("Three of a kind, {0}s", (CardValue)threeOfAKind.Key);
                }
                else if (pairs.Count() == 2)
                {
                    // Two Pair
                    handCategory = HandCategory.TwoPair;
                    // high pair
                    primaryAddition = pairs.Select(p => p.Key).Max();
                    // low pair
                    secondaryAddition = pairs.Select(p => p.Key).Min();
                    // 5th card
                    terciaryAddition = groups.First(g => g.Count() == 1).Key;
                    handDescription = string.Format("Two Pair, {0}s and {1}s", (CardValue)primaryAddition, (CardValue)secondaryAddition);
                }
                else if (pairs.Count == 1)
                {
                    // Pair
                    handCategory = HandCategory.Pair;
                    // pair value
                    primaryAddition = pairs.First().Key;
                    var otherCards = groups.Where(g => g.Count() == 1).Select(g => g.Key).OrderBy(x => x).ToList();
                    // high kicker
                    secondaryAddition = otherCards[0];
                    // 2nd kicker
                    terciaryAddition = otherCards[1];
                    // 3rd kicker
                    terciary2Addition = otherCards[2];
                    handDescription = string.Format("Pair of {0}s", (CardValue)primaryAddition);
                }
            }

            if (handCategory == HandCategory.NotSet)
            {
                // High Card
                handCategory = HandCategory.HighCard;
                var orderedCards = cardValueList.OrderByDescending(x => x).ToList();
                // highest to lowest cards
                primaryAddition = orderedCards[0];
                secondaryAddition = orderedCards[1];
                terciaryAddition = orderedCards[2];
                terciary2Addition = orderedCards[3];
                terciary3Addition = orderedCards[4];
                handDescription = "High Card: " + (CardValue)primaryAddition;
            }

            var score = ((int)handCategory * 100000) +
                (primaryAddition * 10000) +
                (secondaryAddition * 1000) +
                (terciaryAddition * 100) +
                (terciary2Addition * 10) +
                terciary3Addition;

            hand.Description = handDescription;
            hand.Strength = score;
            hand.Rank = handCategory.Description();

            return hand;
        }

        public static CardValue GetCardValue(string value)
        {
            CardValue cardValue = CardValue.WildCard;

            switch (value)
            {
                case "A":
                    cardValue = CardValue.Ace;
                    break;
                case "2":
                    cardValue = CardValue.Two;
                    break;
                case "3":
                    cardValue = CardValue.Three;
                    break;
                case "4":
                    cardValue = CardValue.Four;
                    break;
                case "5":
                    cardValue = CardValue.Five;
                    break;
                case "6":
                    cardValue = CardValue.Six;
                    break;
                case "7":
                    cardValue = CardValue.Seven;
                    break;
                case "8":
                    cardValue = CardValue.Eight;
                    break;
                case "9":
                    cardValue = CardValue.Nine;
                    break;
                case "10":
                    cardValue = CardValue.Ten;
                    break;
                case "J":
                    cardValue = CardValue.Jack;
                    break;
                case "Q":
                    cardValue = CardValue.Queen;
                    break;
                case "K":
                    cardValue = CardValue.King;
                    break;
                default:
                    break;
            }

            return cardValue;
        }

        public static string Description(this Enum enumVal)
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return ((DescriptionAttribute)attributes[0]).Description;
        }
    }
}
