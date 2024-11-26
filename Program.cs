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
                numbers[i] = (random.Next(maxRandomNumber + 1));

            ShowArray(numbers);

            Console.WriteLine();

            Shuffle(numbers, random);

            ShowArray(numbers);

            Console.ReadLine();
        }

        static void Shuffle(int[] array, Random random)
        {
            int secondElementOfArray = 1;

            for (int i = array.Length - 1; i > secondElementOfArray; i--)
            {
                int randomNumber = random.Next(array.Length);
                int temp = array[randomNumber];

                array[randomNumber] = array[i];
                array[i] = temp;
            }
        }

        static void ShowArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (i < array.Length - 1)
                    Console.Write($"{array[i]}, ");
                else
                    Console.Write($"{array[i]}");
            }
        }
    }
}
