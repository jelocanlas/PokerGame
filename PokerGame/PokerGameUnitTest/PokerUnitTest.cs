using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerGameWorkTest;

namespace PokerGameUnitTest
{
    [TestClass]
    public class PokerUnitTest
    {
        readonly List<Player> players = new List<Player>();

        [TestMethod]
        public void Player1Wins()
        {
            players.Add(new Player
            {
                Name = "Player 1",
                Hand = "AS,KS,10S,QS,JS"
            });
            players.Add(new Player
            {
                Name = "Player 2",
                Hand = "AS,4D,5C,3C,2H"
            });
            players.Add(new Player
            {
                Name = "Player 3",
                Hand = "6S,2D,3C,4C,5H"
            });
            players.Add(new Player
            {
                Name = "Player 4",
                Hand = "KH,JH,QH,10H,9H"
            });

            Winner winners = Poker.StartGame(players);

            Assert.IsTrue(winners.WinningPlayers.Equals(players[0].Name), "Player 1 did not win!");

        }

        [TestMethod]
        public void Player2Wins()
        {
            players.Add(new Player
            {
                Name = "Player 1",
                Hand = "2S,4D,6C,8C,10H"
            });
            players.Add(new Player
            {
                Name = "Player 2",
                Hand = "2S,3D,4C,5C,6H"
            });
            players.Add(new Player
            {
                Name = "Player 3",
                Hand = "AS,3D,5C,7C,9H"
            });
            players.Add(new Player
            {
                Name = "Player 4",
                Hand = "2S,3D,5C,6C,7H"
            });

            Winner winners = Poker.StartGame(players);

            Assert.IsTrue(winners.WinningPlayers.Equals(players[1].Name), "Player 2 did not win!");
        }

        [TestMethod]
        public void Player3Wins()
        {
            players.Add(new Player
            {
                Name = "Player 1",
                Hand = "AS,3D,5C,7C,9H"
            });
            players.Add(new Player
            {
                Name = "Player 2",
                Hand = "2S,4D,6C,8C,10H"
            });
            players.Add(new Player
            {
                Name = "Player 3",
                Hand = "2S,3D,4C,5C,6H"
            });
            players.Add(new Player
            {
                Name = "Player 4",
                Hand = "2S,3D,5C,6C,7H"
            });

            Winner winners = Poker.StartGame(players);

            Assert.IsTrue(winners.WinningPlayers.Equals(players[2].Name), "Player 3 did not win!");
        }

        [TestMethod]
        public void Player4Wins()
        {
            players.Add(new Player
            {
                Name = "Player 1",
                Hand = "2S,3D,5C,6C,7H"
            });
            players.Add(new Player
            {
                Name = "Player 2",
                Hand = "2S,4D,6C,8C,10H"
            });
            players.Add(new Player
            {
                Name = "Player 3",
                Hand = "AS,3D,5C,7C,9H"
            });
            players.Add(new Player
            {
                Name = "Player 4",
                Hand = "2S,3D,4C,5C,6H"
            });

            Winner winners = Poker.StartGame(players);

            Assert.IsTrue(winners.WinningPlayers.Equals(players[3].Name), "Player 4 did not win!");
        }
        [TestMethod]
        public void SplitPot()
        {
            players.Add(new Player
            {
                Name = "Player 1",
                Hand = "AS,2S,3S,4S,KS"
            });
            players.Add(new Player
            {
                Name = "Player 2",
                Hand = "2S,3D,5C,6C,7H"
            });
            players.Add(new Player
            {
                Name = "Player 3",
                Hand = "AS,3D,5C,7C,9H"
            });
            players.Add(new Player
            {
                Name = "Player 4",
                Hand = "AH,2H,3H,4H,KH"
            });

            Winner winners = Poker.StartGame(players);

            Assert.IsTrue(winners.WinningPlayers.Equals("TIE"), "No tie detected!");
        }

        [TestMethod]
        public void HighCardWins()
        {
            players.Add(new Player
            {
                Name = "Player 1",
                Hand = "KS,2D,5H,4S,AC"
            });
            players.Add(new Player
            {
                Name = "Player 2",
                Hand = "AS,3D,5C,6C,7H"
            });
            players.Add(new Player
            {
                Name = "Player 3",
                Hand = "JS,3D,5C,7C,9H"
            });
            players.Add(new Player
            {
                Name = "Player 4",
                Hand = "AH,2D,3H,4H,KH"
            });

            Winner winners = Poker.StartGame(players);

            Assert.IsTrue(winners.WinningHandRank.Equals("High Card"), "High Card was not the winning hand!");
        }

        [TestMethod]
        public void PairWins()
        {
            players.Add(new Player
            {
                Name = "Player 1",
                Hand = "KS,2D,5H,4S,AC"
            });
            players.Add(new Player
            {
                Name = "Player 2",
                Hand = "AS,AD,5C,6C,7H"
            });
            players.Add(new Player
            {
                Name = "Player 3",
                Hand = "KS,KD,5C,7C,9H"
            });
            players.Add(new Player
            {
                Name = "Player 4",
                Hand = "AH,2D,3H,4H,KH"
            });

            Winner winners = Poker.StartGame(players);

            Assert.IsTrue(winners.WinningHandRank.Equals("Pair"), "Pair was not the winning hand!");
        }

        [TestMethod]
        public void TwoPairWins()
        {
            players.Add(new Player
            {
                Name = "Player 1",
                Hand = "KS,KD,5H,AS,AC"
            });
            players.Add(new Player
            {
                Name = "Player 2",
                Hand = "AS,3D,5C,6C,7H"
            });
            players.Add(new Player
            {
                Name = "Player 3",
                Hand = "AS,AD,5C,QC,QH"
            });
            players.Add(new Player
            {
                Name = "Player 4",
                Hand = "AH,2D,3H,4H,KH"
            });

            Winner winners = Poker.StartGame(players);

            Assert.IsTrue(winners.WinningHandRank.Equals("Two Pair"), "Two Pair was not the winning hand!");
        }

        [TestMethod]
        public void ThreeOfAKindWins()
        {
            players.Add(new Player
            {
                Name = "Player 1",
                Hand = "KS,KD,KH,4S,AC"
            });
            players.Add(new Player
            {
                Name = "Player 2",
                Hand = "AS,AD,AC,6C,7H"
            });
            players.Add(new Player
            {
                Name = "Player 3",
                Hand = "JS,3D,5C,7C,9H"
            });
            players.Add(new Player
            {
                Name = "Player 4",
                Hand = "QH,QD,JS,JH,KH"
            });

            Winner winners = Poker.StartGame(players);

            Assert.IsTrue(winners.WinningHandRank.Equals("Three of a Kind"), "Three of a Kind was not the winning hand!");
        }

        [TestMethod]
        public void StraightWins()
        {
            players.Add(new Player
            {
                Name = "Player 1",
                Hand = "KS,KD,KH,4S,AC"
            });
            players.Add(new Player
            {
                Name = "Player 2",
                Hand = "AS,AD,5C,6C,AH"
            });
            players.Add(new Player
            {
                Name = "Player 3",
                Hand = "3S,4D,5C,6C,7H"
            });
            players.Add(new Player
            {
                Name = "Player 4",
                Hand = "AH,QD,JH,KH,10H"
            });

            Winner winners = Poker.StartGame(players);

            Assert.IsTrue(winners.WinningHandRank.Equals("Straight"), "Straight was not the winning hand!");
        }

        [TestMethod]
        public void FlushWins()
        {
            players.Add(new Player
            {
                Name = "Player 1",
                Hand = "KS,2D,5H,4S,AC"
            });
            players.Add(new Player
            {
                Name = "Player 2",
                Hand = "AS,3D,5C,6C,7H"
            });
            players.Add(new Player
            {
                Name = "Player 3",
                Hand = "JD,AD,5D,KD,9D"
            });
            players.Add(new Player
            {
                Name = "Player 4",
                Hand = "AH,2D,3H,4H,KH"
            });

            Winner winners = Poker.StartGame(players);

            Assert.IsTrue(winners.WinningHandRank.Equals("Flush"), "Flush was not the winning hand!");
        }

        [TestMethod]
        public void FullHouseWins()
        {
            players.Add(new Player
            {
                Name = "Player 1",
                Hand = "KS,2D,5H,4S,AC"
            });
            players.Add(new Player
            {
                Name = "Player 2",
                Hand = "AS,3D,5C,6C,7H"
            });
            players.Add(new Player
            {
                Name = "Player 3",
                Hand = "AS,AD,AC,2C,2H"
            });
            players.Add(new Player
            {
                Name = "Player 4",
                Hand = "KH,KD,KS,4H,4S"
            });

            Winner winners = Poker.StartGame(players);

            Assert.IsTrue(winners.WinningHandRank.Equals("Full House"), "Full House was not the winning hand!");
        }

        [TestMethod]
        public void FourOfAKindWins()
        {
            players.Add(new Player
            {
                Name = "Player 1",
                Hand = "KS,KD,KH,4S,KC"
            });
            players.Add(new Player
            {
                Name = "Player 2",
                Hand = "AS,AD,AC,6C,AH"
            });
            players.Add(new Player
            {
                Name = "Player 3",
                Hand = "QS,QD,QC,7C,7H"
            });
            players.Add(new Player
            {
                Name = "Player 4",
                Hand = "AH,2D,3H,4H,KH"
            });

            Winner winners = Poker.StartGame(players);

            Assert.IsTrue(winners.WinningHandRank.Equals("Four of a Kind"), "Four Of A Kind was not the winning hand!");
        }

        [TestMethod]
        public void StraightFlushWins()
        {
            players.Add(new Player
            {
                Name = "Player 1",
                Hand = "KS,2D,5H,4S,AC"
            });
            players.Add(new Player
            {
                Name = "Player 2",
                Hand = "9S,QS,10S,JS,KS"
            });
            players.Add(new Player
            {
                Name = "Player 3",
                Hand = "JS,3D,5C,7C,9H"
            });
            players.Add(new Player
            {
                Name = "Player 4",
                Hand = "AH,2H,3H,4H,KH"
            });

            Winner winners = Poker.StartGame(players);

            Assert.IsTrue(winners.WinningHandRank.Equals("Straight Flush"), "Straight Flush was not the winning hand!");
        }

        [TestMethod]
        public void RoyalFlushWins()
        {
            players.Add(new Player
            {
                Name = "Player 1",
                Hand = "KS,2D,5H,4S,AC"
            });
            players.Add(new Player
            {
                Name = "Player 2",
                Hand = "AS,3D,5C,6C,7H"
            });
            players.Add(new Player
            {
                Name = "Player 3",
                Hand = "AS,2S,3S,4S,5S"
            });
            players.Add(new Player
            {
                Name = "Player 4",
                Hand = "AH,KH,QH,JH,10H"
            });

            Winner winners = Poker.StartGame(players);

            Assert.IsTrue(winners.WinningHandRank.Equals("Royal Flush"), "Impossible! Royal Flush is the highest natural hand!");
        }
    }
}
