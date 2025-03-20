using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            InputUser();
        }

        static void ShowCommand(string textCommand, string textDiscription)
        {
            Console.WriteLine(textDiscription + textCommand);
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
            bool isSuccess = int.TryParse(userInput, out int shift);

            if (isSuccess)
            {
                numbers.Add(shift);
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

        static void InputUser()
        {
            const string CommandExit = "exit";
            const string CommandSum = "sum";

            bool isOpen = true;

            List<int> numbers = new List<int>();

            while (isOpen)
            {
                Console.Clear();

                ShowCommand(CommandSum, "Что-бы получить сумму всех чисел введите: ");
                ShowCommand(CommandExit, "Что-бы выйти из программы введите: ");

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
}
