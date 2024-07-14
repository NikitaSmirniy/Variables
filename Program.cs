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
            string enterName;
            string enterSurname;

            string userName = "Wick";
            string userSurname = "John";

            Console.Write($"{enterName = userName} ");
            Console.Write($"{enterSurname = userSurname}\n");

            Console.Write($"{enterName = userSurname} ");
            Console.Write($"{enterSurname = userName}");
            Console.ReadLine();
        }
    }
}
