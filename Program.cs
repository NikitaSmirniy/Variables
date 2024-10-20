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

            int degree = 1;
            int numberMultiplied = 2;
            int multipliedNumber = minDiapasonNumber;

            Console.WriteLine("Случайное число: " + randomNumber);

            while (multipliedNumber <= randomNumber)
            {
                degree++;
                multipliedNumber *= numberMultiplied;

                Console.WriteLine($"{multipliedNumber} = {numberMultiplied}^{degree}");
            }

            Console.WriteLine($"Минимальная степень двойки превосходящая число {randomNumber} = {degree}");

            Console.ReadLine();
        }
    }
}
