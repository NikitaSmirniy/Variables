using System;

namespace ConsoleApp1
{
    class Program
    {   
        static void Main(string[] args)
        {
             Console.WriteLine(ReadInt());

            Console.ReadLine();
        }

        static int ReadInt()
        {
            int result = 0;
            bool isSuccess = false;
            string userInput;

            while (isSuccess == false)
            {
                Console.Write("Введите число: ");
                userInput = Console.ReadLine();
                isSuccess = int.TryParse(userInput, out result);

                if (isSuccess == false)
                {
                    Console.WriteLine("Неверный ввод");
                }
            }

            return result;
        }
    }
}
