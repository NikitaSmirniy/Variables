using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] array = new int[6, 4]
                {
                    { 1,2,3,4},
                    { 1,2,3,4},
                    { 1,2,3,4},
                    { 2,2,3,4},
                    { 3,2,3,4},
                    { 1,2,3,4}
                };

            int sum = 0;
            int productOfNumber = 1;
            int arrayString = 1;
            int arrayColumn = 0;

            for (int j = 0; j < array.GetLength(1); j++)
            {
                sum += array[arrayString, j];
            }

            for (int i = 0; i < array.GetLength(0); i++)
            {
                productOfNumber *= array[i, arrayColumn];
            }

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine($"\n{sum}");
            Console.WriteLine(productOfNumber);

            Console.ReadLine();
        }
    }
}
