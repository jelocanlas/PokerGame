using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PokerGameWorkTest
{
    public class Poker
    {
        public static Winner StartGame(List<Player> players)
        {
            Game game = new Game();
            game.Play(players);

            Console.WriteLine(game.GetAllPlayerHands());
            Console.Write(game.GetWinningPlayers().WinningMessage);

            return game.GetWinningPlayers();
            
        }
    }
}
