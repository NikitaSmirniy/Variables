using System;

namespace ConsoleApp1
{
    class Program
    {
        static int ReadInt(string userInput)
        {
            while (true)
            {
                int result;

                Console.Write("Введите число: ");
                userInput = Console.ReadLine();

                bool isSuccess = int.TryParse(userInput, out result);

                if (isSuccess)
                    return result;
                else
                {
                    Console.WriteLine("Неверный ввод");

                    continue;
                }
            }
        }
        
        static void Main(string[] args)
        {
             string userInput = "";

            ReadInt(userInput);

            Console.ReadLine();
        }
    }
}
