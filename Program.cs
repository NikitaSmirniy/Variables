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
            const int MinimumNumber = 50;
            const int MaximumNumber = 151;

            int number = new Random().Next(MinimumNumber, MaximumNumber);

            Console.WriteLine($"кратные числа {number} от {MinimumNumber} до {MaximumNumber - 1}");

            for (int i = number; i < MaximumNumber; i += number)
            {
                Console.WriteLine(i);
            }

            Console.ReadLine();
        }
    }
}
