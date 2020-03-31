using PokerGameWorkTest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGame
{
    public static class Program
    {
        static readonly List<Player> players = new List<Player>();

        public static void Main()
        {
            players.Add(new Player
            {
                Name = "Player 1",
                Hand = "2S,3D,4C,5C,6H"
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
                Hand = "2S,3D,5C,6C,7H"
            });

            Game game = new Game();
            game.Play(players);

            Console.WriteLine(game.GetAllPlayerHands());
            Console.Write(game.GetWinningPlayers().WinningMessage);
            Console.ReadLine();
        }
    }
}
