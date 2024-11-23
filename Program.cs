using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput = "(()(()))";
            int maxDepth = 0;
            int depth = 0;
            char rightStaple = '(';
            char leftStaple = ')';

            Console.WriteLine("Введите скобочное выражение");
            userInput = Console.ReadLine();

            foreach (var symbol in userInput)
            {
                if (symbol == rightStaple)
                {
                    depth++;

                    if (depth > maxDepth)
                        maxDepth = depth;
                }
                else if (symbol == leftStaple)
                {
                    depth--;

                    if(depth < 0)
                    {
                        break;
                    }
                }
            }

            if(depth != 0)
                Console.WriteLine("Выражение некорректно");
            else
                Console.WriteLine($"Корректно \nМаксимальная глубина {maxDepth}");

            Console.ReadLine();
        }
    }
}
