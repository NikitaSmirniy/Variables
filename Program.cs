using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxDiapasonNumber = 100;
            int minDiapasonNumber = 2;
            int randomNumber = new Random().Next(minDiapasonNumber, maxDiapasonNumber + 1);

            int multipliedNumber = 2;
            int degree = 2;

            Console.WriteLine("Случайоне число: " + randomNumber);

            for (int i = degree; multipliedNumber <= randomNumber; i++)
            {
                multipliedNumber *= degree;
                Console.WriteLine($"{multipliedNumber} = {degree}^{i}");

                if (multipliedNumber > randomNumber)
                {
                    Console.WriteLine($"Минимальная степень двойки превосходящая число {randomNumber} = {i}");
                    break;
                }
            }

            Console.ReadLine();
        }
    }
}
