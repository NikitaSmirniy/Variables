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
            string userInput = "";
            string checkExit = "exit";
            
            while(userInput != checkExit)
            {
                Console.Write("Messag\nВведите exit, что-бы выйти\n");
                userInput = Console.ReadLine();
            }
            Console.ReadLine();
        }
    }
}
