using System;

namespace ConsoleApp1
{
    class Program
    {   
        static void Main(string[] args)
        {
            int playerPositionX = 5;
            int playerPositionY = 1;
            char playerSymbol = '@';

            char space = ' ';
            char exitPoint = '%';

            bool isOpen = true;

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
                {'#', '#', '#',' ','#','#','#',' ',' ',' ','#',' ',' ',' ','%'},
                {'#', '#', '#',' ','#','#','#',' ',' ',' ','#','#','#',' ','#'},
                {'#', '#', '#',' ',' ',' ',' ',' ',' ','#','#','#','#',' ','#'},
                {'#', '#', '#','#','#','#','#','#','#','#','#','#','#','#','#'}
            };

            Console.CursorVisible = false;

            while (isOpen)
            {
                ShowMap(map);

                ConsoleKeyInfo pressedKey;
                Console.SetCursorPosition(playerPositionY, playerPositionX);
                Console.Write(playerSymbol);
                pressedKey = Console.ReadKey();

                int[] direction = GetDirection(pressedKey);

                if (TryGameOver(ref playerPositionX, ref playerPositionY, map, ref direction, exitPoint))
                    isOpen = false;
                else
                    TryMovePlayer(pressedKey, ref playerPositionX, ref playerPositionY, map, ref direction, space);
            }
        }

        static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            const ConsoleKey MoveUpCommand = ConsoleKey.UpArrow;
            const ConsoleKey MoveDownCommand = ConsoleKey.DownArrow;
            const ConsoleKey MoveLeftCommand = ConsoleKey.LeftArrow;
            const ConsoleKey MoveRightCommand = ConsoleKey.RightArrow;

            int[] direction = { 0, 0 };

            switch (pressedKey.Key)
            {
                case MoveUpCommand:
                    direction[0] = -1;
                    break;

                case MoveDownCommand:
                    direction[0] = 1;
                    break;

                case MoveLeftCommand:
                    direction[1] = -1;
                    break;

                case MoveRightCommand:
                    direction[1] = 1;
                    break;
            }

            return direction;
        }

        static void TryMovePlayer(ConsoleKeyInfo pressedKey, ref int playerPositionX, ref int playerPositionY, char[,] map, ref int[] direction, char space)
        {
            int nextPlayerPositionX = playerPositionX + direction[0];
            int nextPlayerPositionY = playerPositionY + direction[1];

            if (map[nextPlayerPositionX, nextPlayerPositionY] == space)
            {
                playerPositionX = nextPlayerPositionX;
                playerPositionY = nextPlayerPositionY;
            }
        }

        static bool TryGameOver(ref int playerPositionX, ref int playerPositionY, char[,] map, ref int[] direction, char exitPoint)
        {
            int nextPlayerPositionX = playerPositionX + direction[0];
            int nextPlayerPositionY = playerPositionY + direction[1];

            if (map[nextPlayerPositionX, nextPlayerPositionY] == exitPoint)
                return true;
            else
                return false;
        }

        static void ShowMap(char[,] map)
        {
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
