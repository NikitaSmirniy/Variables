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
            string userName = "Wick";
            string userSurname = "John";
            string temp;

            string redCup = "tea";
            string whiteCup = "capuchino";

            Console.Write($"{userName} ");
            Console.Write($"{userSurname}\n");

            temp = userName;
            userName = userSurname;
            userSurname = temp;

            Console.Write($"{userName} ");
            Console.Write($"{userSurname} ");
            Console.ReadLine();

            Console.Clear();
            Console.Write($"{redCup} ");
            Console.Write($"{whiteCup}\n");

            temp = redCup;
            redCup = whiteCup;
            whiteCup = temp;

            Console.Write($"{redCup} ");
            Console.Write($"{whiteCup} ");
            Console.ReadLine();
        }
    }
}
