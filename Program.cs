using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Render render = new Render();
            Player player = new Player(20, 20);

            render.ShowPlayerPosition(player.X, player.Y, player.PlayerSymbol);

            Console.ReadKey();
        }
    }

    class Render
    {
        public void ShowPlayerPosition(int x, int y, char playerSymbol)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(x, y);
            Console.Write(playerSymbol);
        }
    }

    class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public char PlayerSymbol { get; private set; }

        public Player(int x, int y, char playerSymbol = '#')
        {
            X = x;
            Y = y;
            PlayerSymbol = playerSymbol;
        }
    }
}
