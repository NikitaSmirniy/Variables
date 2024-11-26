using System;

namespace ConsoleApp1
{
    class Program
    {   
        static void Main(string[] args)
        {
            int[] numbers = new int[8];

            Random random = new Random();
            int maxRandomNumber = 9;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = (random.Next(0, maxRandomNumber + 1));

                ShowArray(numbers, i);
            }

            Console.WriteLine();

            Shuffle(numbers);

            Console.ReadLine();
        }

        static void Shuffle(int[] array)
        {
            var random = new Random();

            for (int i = array.Length - 1; i > 0; i--)
            {
                int randomNumber = random.Next(i + 1);
                int temp = array[randomNumber];

                array[randomNumber] = array[i];
                array[i] = temp;
            }

            for (int i = 0; i < array.Length; i++)
                ShowArray(array, i);
        }

        static void ShowArray(int[] array, int indexArray)
        {
            if (indexArray < array.Length - 1)
                Console.Write($"{array[indexArray]}, ");
            else
                Console.Write($"{array[indexArray]}");
        }
    }
}
