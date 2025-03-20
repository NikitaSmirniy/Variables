using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandExit = "exit";
            const string CommandSum = "sum";

            bool isOpen = true;

            List<int> numbers = new List<int>();

            while (isOpen)
            {
                Console.Clear();

                Console.WriteLine($"Что-бы получить сумму всех чисел введите: {CommandSum}");
                Console.WriteLine($"Что-бы выйти из программы введите: {CommandExit}");

                ShowNumbers(numbers);

                Console.Write("Введите команду:");

                string userInput = Console.ReadLine();

                switch (userInput.ToLower())
                {
                    case CommandSum:
                        ShowSum(numbers);
                        break;

                    case CommandExit:
                        isOpen = false;
                        break;

                    default:
                        AddNumber(userInput, numbers);
                        break;
                }
            }
        }

        static void ShowSum(List<int> numbers)
        {
            int sum = 0;

            foreach (var number in numbers)
            {
                sum += number;
            }

            Console.WriteLine(sum);
            Console.ReadLine();
        }

        static void AddNumber(string userInput, List<int> numbers)
        {
            bool isNumber = int.TryParse(userInput, out int result);

            if (isNumber)
            {
                numbers.Add(result);
                Console.WriteLine("Число успешно добавлено");
            }
            else
            {
                Console.WriteLine("Введена неверная команда");
            }

            Console.ReadLine();
        }

        static void ShowNumbers(List<int> numbers)
        {
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
        }
    }
}
