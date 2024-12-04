using System;

namespace ConsoleApp1
{
    class Program
    {   
        static void Main(string[] args)
        {
            char[,] map =
            {
                {'#', '#', '#','#','#','#','#','#','#','#','#','#','#','#','#'},
                {'#', '#', '#','#','#','#','#','#','#','#','#','#','#','#','#'},
                {'#', '#', '#','#','#','#','#','#','#','#','#','#','#','#','#'},
                {'#', '#', '#','#','#','#','#','#','#','#','#','#','#','#','#'},
                {'#', ' ', '#','#','#','#','#','#','#','#','#','#','#','#','#'},
                {'#', ' ', ' ',' ',' ',' ',' ','#','#','#','#','#','#','#','#'},
                {'#', '#', ' ',' ','#','#','#','#','#','#','#','#','#','#','#'},
                {'#', '#', '#',' ','#','#','#','#',' ',' ',' ',' ','#','#','#'},
                {'#', '#', '#',' ','#','#','#','#',' ','#','#',' ','#','#','#'},
                {'#', '#', '#',' ','#','#','#','#',' ','#','#',' ','#',' ','#'},
                {'#', '#', '#',' ','#','#','#',' ',' ',' ','#',' ','#',' ','#'},
                {'#', '#', '#',' ','#','#','#',' ',' ',' ','#',' ',' ',' ','#'},
                {'#', '#', '#',' ','#','#','#',' ',' ',' ','#','#','#',' ','#'},
                {'#', '#', '#',' ',' ',' ',' ',' ',' ','#','#','#','#',' ','#'},
                {'#', '#', '#','#','#','#','#','#','#','#','#','#','#','#','#'}
            };

            ShowMap(map);
            TryMovePlayer(map);

            Console.ReadLine();
        }

        static void TryMovePlayer(char[,] map)
        {
            int playerX = 5;
            int playerY = 1;
            char player = '@';

            Console.CursorVisible = false;
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

            while (true)
            {        
                ShowMap(map);

                Console.SetCursorPosition(playerY, playerX);
                Console.Write(player);
                pressedKey = Console.ReadKey();

                HandlInput(pressedKey, ref playerX, ref playerY, map);
            }
        }

        static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };

            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    direction[0] = -1;
                    break;

                case ConsoleKey.DownArrow:
                    direction[0] = 1;
                    break;

                case ConsoleKey.LeftArrow:
                    direction[1] = -1;
                    break;

                case ConsoleKey.RightArrow:
                    direction[1] = 1;
                    break;
            }

            return direction;
        }

        static void HandlInput(ConsoleKeyInfo pressedKey, ref int playerX, ref int playerY, char[,] map)
        {
            int[] direction = GetDirection(pressedKey);

            int nextPlayerPositionX = playerX + direction[0];
            int nextPlayerPositionY = playerY + direction[1];

            if(map[nextPlayerPositionX,nextPlayerPositionY] == ' ')
            {
                playerX = nextPlayerPositionX;
                playerY = nextPlayerPositionY;
            }
        }

        static void ShowMap(char[,] map)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    Console.Write(map[x, y]);
                }

                Console.WriteLine();
            }
        }
    }
}
