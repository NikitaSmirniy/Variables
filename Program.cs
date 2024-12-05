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
            MovePlayer(map);

            Console.ReadLine();
        }

        static void MovePlayer(char[,] map)
        {
            int playerPositionX = 5;
            int playerPositionY = 1;
            char playerSymbol = '@';
            bool isOpen = true;

            Console.CursorVisible = false;
            ConsoleKeyInfo pressedKey;

            while (isOpen)
            {        
                ShowMap(map);

                Console.SetCursorPosition(playerPositionY, playerPositionX);
                Console.Write(playerSymbol);
                pressedKey = Console.ReadKey();

                HandlInput(pressedKey, ref playerPositionX, ref playerPositionY, map);
            }
        }

        static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            const ConsoleKey UpKey = ConsoleKey.UpArrow;
            const ConsoleKey DownKey = ConsoleKey.DownArrow;
            const ConsoleKey LeftKey = ConsoleKey.LeftArrow;
            const ConsoleKey RightKey = ConsoleKey.RightArrow;

            int[] direction = { 0, 0 };

            switch (pressedKey.Key)
            {
                case UpKey:
                    direction[0] = -1;
                    break;

                case DownKey:
                    direction[0] = 1;
                    break;

                case LeftKey:
                    direction[1] = -1;
                    break;

                case RightKey:
                    direction[1] = 1;
                    break;
            }

            return direction;
        }

        static void HandlInput(ConsoleKeyInfo pressedKey, ref int playerPositionX, ref int playerPositionY, char[,] map)
        {
            int[] direction = GetDirection(pressedKey);

            int nextPlayerPositionX = playerPositionX + direction[0];
            int nextPlayerPositionY = playerPositionY + direction[1];

            if(map[nextPlayerPositionX,nextPlayerPositionY] == ' ')
            {
                playerPositionX = nextPlayerPositionX;
                playerPositionY = nextPlayerPositionY;
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
