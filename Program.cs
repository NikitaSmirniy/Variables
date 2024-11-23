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
            bool isOpen = true;

            while (isOpen)
            {
                string userInput;
                int result;

                Console.Write("Введите число: ");
                userInput = Console.ReadLine();

                bool isSuccess = int.TryParse(userInput, out result);

                if (isSuccess)
                {
                    isOpen = false;
                    return result;
                }
                else
                    Console.WriteLine("Неверный ввод");
            }

            return 0;
        }
    }
}
