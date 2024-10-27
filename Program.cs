using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandSum = "sum";
            const string CommandExit = "exit";

            int[] arrayNumber = new int[1];
            int[] array;
            bool isOpen = true;
            int sum = 0;
            string userInput;

            while (isOpen)
            {
                Console.Clear();

                for (int i = 0; i < arrayNumber.Length; i++)
                {
                    Console.Write($"{arrayNumber[i]} ");
                }

                Console.WriteLine($"Команда: {CommandSum} - находит сумму всего массива");
                Console.WriteLine($"Команда: {CommandExit} - выходит из программы");
                Console.WriteLine($"Команда: {CommandSum} - находит сумму всего массива");

                Console.Write("Введите комманду: ");

                sum = 0;
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandSum:
                        for (int i = 0; i < arrayNumber.Length; i++)
                        {
                            sum += arrayNumber[i];
                        }

                        Console.WriteLine($"Сумма чисел всех элементов массива: {sum}");
                        break;

                    case CommandExit:
                        isOpen = false;
                        Console.WriteLine("Программа завершена");
                        break;
                    default:
                        array = arrayNumber;
                        arrayNumber = new int[arrayNumber.Length + 1];

                        for (int i = 0; i < array.Length; i++)
                        {
                            arrayNumber[i] = array[i];
                        }

                        arrayNumber[arrayNumber.Length - 1] = Convert.ToInt32(userInput);
                        //Console.WriteLine("Введина неверная комманда");
                        break;
                }

                Console.ReadLine();
            
        }
    }
}
