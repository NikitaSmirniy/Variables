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

            int randomNumber = new Random().Next(MinimumNumber, MaximumNumber);
            int multiple = randomNumber;

            Console.WriteLine($"кратные числа {randomNumber} от {MinimumNumber} до {MaximumNumber - 1}");

            for (int i = 1; multiple < MaximumNumber; i++)
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
