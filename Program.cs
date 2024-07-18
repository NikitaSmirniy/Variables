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
            const string CommandShowText1 = "1";
            const string CommandShowText2 = "2";
            const string CommandRandomNumber = "3";
            const string CommandConsoleClear = "4";
            const string CommandExit = "5";

            Random random = new Random();
            int number;

            string userInput;
            bool isOpen = true;

            Console.WriteLine("Добро пожаловать в программу!\n");

            while(isOpen)
            {
                Console.Write($"Команда {CommandShowText1} вывести текст документа {CommandShowText1}\n");
                Console.Write($"Команда {CommandShowText2} вывести текст документа {CommandShowText2}\n");
                Console.Write($"Команда {CommandRandomNumber} вывести рандомное число\n");
                Console.Write($"Команда {CommandConsoleClear} очистить консоль!\n");
                Console.WriteLine($"Команда {CommandExit} выйти из консоли!");

                userInput = Console.ReadLine();
                
                switch(userInput)
                {
                    case CommandShowText1:
                        Console.WriteLine("Документ 1\n\n");
                            break;
                    case CommandShowText2:
                        Console.WriteLine("Докумет 2\n\n");
                        break;
                    case CommandRandomNumber:
                        number = random.Next(0, 101);
                        Console.WriteLine($"Вы вывели рандомное число и оно равно {number}\n\n");
                        break;
                    case CommandConsoleClear:
                        Console.Clear();
                        Console.Write("Консоль очищена\n\n");
                        break;
                    case CommandExit:
                        Console.Write("Вы вышли из прогараммы");
                        isOpen = false;
                        break;
                    default:
                        Console.Write("Такой команды не существует!\n\n");
                        break;
                }
            }

            Console.ReadLine();
        }
    }
}
