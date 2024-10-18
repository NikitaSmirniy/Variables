using System;

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

            for (int i = 0; i <= MaximumNumber; i += randomNumber)
            {
                if (i + randomNumber <= MaximumNumber)
                {
                    multiple++;
                }
            }

            Console.WriteLine(multiple);

            Console.ReadLine();
        }
    }
}
