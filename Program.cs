using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandSum = "sum";
            const string CommandExit = "exit";

            int[] numbers = new int[0];
            bool isOpen = true;

            while (isOpen)
            {
                string userInput;

                Console.Clear();

                Console.Write("Все элемнты массива: ");

                for (int i = 0; i < numbers.Length; i++)
                {
                    Console.Write($"{numbers[i]} ");
                }

                Console.WriteLine("\n");

                Console.WriteLine($"Команда: {CommandSum} - находит сумму всего массива");
                Console.WriteLine($"Команда: {CommandExit} - выходит из программы");
                Console.WriteLine($"Команда: {CommandSum} - находит сумму всего массива");

                Console.Write("Введите комманду: ");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandSum:
                        int sum = 0;

                        for (int i = 0; i < numbers.Length; i++)
                        {
                            sum += numbers[i];
                        }

                        Console.WriteLine($"Сумма чисел всех элементов массива: {sum}");
                        break;

                    case CommandExit:
                        isOpen = false;
                        Console.WriteLine("Программа завершена");
                        break;

                    default:
                        int[] tempArray;

                        tempArray = numbers;
                        numbers = new int[numbers.Length + 1];

                        for (int i = 0; i < tempArray.Length; i++)
                        {
                            numbers[i] = tempArray[i];
                        }

                        Console.Write($"Вы добавили элемент массива: {userInput}");
                        numbers[numbers.Length - 1] = Convert.ToInt32(userInput);
                        break;
                }

                Console.ReadLine();
            }
            }
        }
    }
}
