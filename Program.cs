using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandExit = "exit";

            int[] numbers = new int[10];
            int firstElement = 0;
            int lastElement = numbers.Length - 1;

            Random random = new Random();
            int maxRandomNumber = 100;

            bool isOpen = true;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(0, maxRandomNumber + 1);

                Console.Write($"{numbers[i]}, ");
            }

            while (isOpen)
            {
                Console.WriteLine($"\n\nКоманда {CommandExit} закрыть программу");

                Console.WriteLine("\nНа сколько элеметов хотите произвести сдвиг: ");

                string userInput = Console.ReadLine();

                int shift;
                bool isSuccess = int.TryParse(userInput, out shift);

                if (userInput == CommandExit)
                {
                    break;
                }
                else if (isSuccess == false)
                {
                    Console.WriteLine("Неверная комманда!");
                    Console.ReadLine();
                    continue;
                }

                int shiftResult = shift % numbers.Length;

                for (int i = 0; i < shiftResult; i++)
                {
                    int temp = numbers[numbers.Length - 1];

                    numbers[numbers.Length - 1] = numbers[firstElement];

                    for (int j = 0; j < lastElement - 1; j++)
                    {
                        numbers[j] = numbers[j + 1];
                    }

                    numbers[lastElement - 1] = temp;
                }

                for (int i = 0; i < numbers.Length; i++)
                {
                    Console.Write($"{numbers[i]}, ");
                }
            }

            Console.ReadLine();
        }
    }
}
