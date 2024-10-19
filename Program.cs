using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int minRandomNumber = 10;
            int maxRandomNumber = 25;
            int minNumber = 50;
            int maxNumber = 150;

            Random random = new Random();
            int randomNumber = random.Next(minRandomNumber, maxRandomNumber + 1);
            int mumberOfMultiples = 0;

            Console.WriteLine($"кратные числа {randomNumber} от {minNumber} до {maxNumber}");

            for (int i = 0; i <= maxNumber; i += randomNumber)
            {
                if (i >= minNumber)
                {
                    mumberOfMultiples++;
                }
            }

            Console.WriteLine(mumberOfMultiples);

            Console.ReadLine();
        }
    }
}
