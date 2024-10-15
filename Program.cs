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
            const int MaximumNumber = 150;

            Random random = new Random();
            int randomNumber = random.Next(MinimumNumber, MaximumNumber + 1);
            int multiple = 0;

            Console.WriteLine($"кратные числа {randomNumber} от {MinimumNumber} до {MaximumNumber}");

            for (int i = 0; multiple < MaximumNumber; i++)
            {
                multiple += randomNumber;

                if (multiple > MaximumNumber)
                {
                    Console.WriteLine(i);
                }
            }

            Console.ReadLine();
        }
    }
}
