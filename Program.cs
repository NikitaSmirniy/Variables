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
            Random random = new Random();
            int number;
            int fractions1 = 3;
            int fractions2 = 5;
            int sum = 0;

            number = random.Next(3, 101);
            Console.WriteLine(number);

            for (int i = 0; i <= number; i++)
            {
                if(i % fractions1 == 0 || i % fractions2 == 0)
                {
                    sum += i;
                }
            }

            Console.WriteLine("Сумма чисел = " + sum);
            Console.ReadLine();
        }
    }
}
