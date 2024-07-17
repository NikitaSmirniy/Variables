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
            string message;
            int numberOfRepiting;

            Console.Write("Какое сообщение вы хотите вывести ? ");
            message = Console.ReadLine();

            Console.Write("Сколько раз должно вывестись сообщение? ");
            numberOfRepiting = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < numberOfRepiting; i++)
            {
                Console.Write(message + "\n");
            }
            Console.ReadLine();
        }
    }
}
