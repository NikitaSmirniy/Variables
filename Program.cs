using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int maximumDiapasonNumber = 100;
            int randomNumber = new Random().Next(0, maximumDiapasonNumber + 1);

            int multipliedNumber = 2;
            int degree = 2;

            Console.WriteLine(randomNumber);

            for (int i = degree; multipliedNumber <= randomNumber; i++)
            {
                multipliedNumber *= degree;

                if (multipliedNumber > randomNumber)
                {
                    Console.WriteLine(i);
                }
            }

            Console.ReadLine();
        }
    }
}
