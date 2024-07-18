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
            int startNumber = 5;
            int stepNumber = 7;
            int maximumNumber = 103;

            // Используется цикл for потому-что так код выглядит более компактным и удобным в чтении
            for (int i = startNumber; i <= maximumNumber; i += stepNumber)
            {
                Console.Write(i + "\n");
            }

            Console.ReadLine();
        }
    }
}
