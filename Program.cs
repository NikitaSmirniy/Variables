using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[30];

            Random random = new Random();

            int maxRandomNumber = 100;

            Console.Write("Не отсортированный массив: ");

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(0, maxRandomNumber + 1);

                Console.Write(numbers[i] + " ");
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length - 1 - i; j++)
                {
                    int temp;

                    if (numbers[j] > numbers[j + 1])
                    {
                        temp = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("\n");
            Console.Write("Отсортированный массив: ");

            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write(numbers[i] + " ");
            }

            Console.ReadLine();
        }
    }
}
