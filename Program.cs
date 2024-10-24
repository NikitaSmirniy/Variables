using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int minRandomNumber = 0;
            int maxRandomNumber = 30;

            int[] array = new int[30];
            int firstElementIndex = 0;
            int lastElementIndex = array.Length - 1;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(minRandomNumber, maxRandomNumber);
                Console.Write($"{array[i]} ");
            }

            Console.WriteLine();

            if (array[firstElementIndex] > array[firstElementIndex + 1])
            {
                Console.WriteLine(array[firstElementIndex]);
            }

            for (int i = 1; i < lastElementIndex; i++)
            {
                if (array[i] > array[i - 1] && array[i] > array[i + 1])
                {
                    Console.WriteLine(array[i]);
                }
            }

            if (array[lastElementIndex] > array[lastElementIndex - 1])
            {
                Console.WriteLine(array[lastElementIndex]);
            }

            Console.ReadLine();
        }
    }
}
