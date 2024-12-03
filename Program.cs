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

            Console.ReadLine();
        }

        static void ShowMap(char[,] map)
        {
            int userX = 5;
            int userY = 1;
            char obstacle = '#';
            char player = '@';

            Console.CursorVisible = false;

            while (true)
            {
                Console.SetCursorPosition(0, 0);

                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        Console.Write(map[i, j]);
                    }

                    Console.WriteLine();
                }

                Console.SetCursorPosition(userY, userX);
                Console.Write(player);
                ConsoleKeyInfo charKey = Console.ReadKey();

                switch (charKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (map[userX - 1, userY] != obstacle)
                            userX--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (map[userX + 1, userY] != obstacle)
                            userX++;
                        break;

                    case ConsoleKey.LeftArrow:
                        if (map[userX, userY - 1] != obstacle)
                            userY--;
                        break;

                    case ConsoleKey.RightArrow:
                        if (map[userX, userY + 1] != obstacle)
                            userY++;
                        break;
                }
            }
        }
    }
}
