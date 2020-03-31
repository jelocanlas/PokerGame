using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerGameWorkTest
{
    public class Game
    {
        private List<Player> Players { get; set; }

        public void Play(List<Player> players)
        {
            this.Players = players;
            Players.ForEach(p => p.HandStrength = PokerService.EvaluateHand(p.Name, p.Hand.Split(',').ToList()));
        }

        public string GetAllPlayerHands()
        {
            var sb = new StringBuilder();
            foreach (var player in Players)
            {
                sb.Append(player);
            }

            return sb.ToString();
        }

        public Winner GetWinningPlayers()
        {
            Winner winners;

            var winner = Players
                .OrderByDescending(p => p.HandStrength.Strength)
                .First();

            var tiedPlayers = Players.Where(p => p.HandStrength.Strength == winner.HandStrength.Strength).ToList();

            if (tiedPlayers.Count() > 1)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("There is a {0}-way tie between ", tiedPlayers.Count());
                for (var i = 0; i < tiedPlayers.Count(); i++)
                {
                    sb.Append(i == tiedPlayers.Count - 1 ? " and " : ", ");
                    sb.AppendFormat(tiedPlayers[i].Name);
                }

                winners = new Winner() { 
                    WinningPlayers = "TIE",
                    WinningMessage = sb.ToString(),
                    WinningHandRank = winner.HandStrength.Rank };

                return winners;
            }

            winners = new Winner() { 
                WinningPlayers = winner.Name,
                WinningMessage = string.Format("{0} wins with a {1}", winner.Name,
                winner.HandStrength.Description), WinningHandRank = winner.HandStrength.Rank };

            return winners;
        }
    }
}
