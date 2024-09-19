using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string password = "T31Gg228";
            const string secretMessage = "Top Secret";

            int numberAttempts = 3;
            bool isOpen = true;

            while (isOpen)
            {
                string userInput;
                
                Console.Write("Введите пароль: ");
                userInput = Console.ReadLine();

                if (userInput == password)
                {
                    Console.WriteLine(secretMessage);
                    isOpen = false;
                }
                else
                {
                    Console.WriteLine("Введён неверный пароль");
                    numberAttempts--;
                    Console.WriteLine($"осталось: {numberAttempts} попыток");

                    if (numberAttempts <= 0)
                    {
                        isOpen = false;
                        Console.WriteLine("Попытки закончились");
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
