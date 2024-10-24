using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            int minNumber = 10;
            int maxNumber = 100;
            int[,] array = new int[10, 10];

            int lineIndex = 0;
            int lastLargeNumber = 0;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = random.Next(minNumber + 1, maxNumber);

                    if (array[i, j] > lastLargeNumber)
                    {
                        lastLargeNumber = array[i, j];
                        lineIndex = i;
                    }

                    Console.Write(array[i, j] + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (i == lineIndex)
                    {
                        array[i, j] = 0;
                    }

                    Console.Write(array[i, j] + " ");
                }

                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
