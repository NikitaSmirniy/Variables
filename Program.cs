using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[30];

            Random random = new Random();

            int maxRandomNumber = 9;

            int repeatedNumber = 0;
            int repeatedAmount = 0;
            int repeatedCheck = 0;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(0, maxRandomNumber + 1);

                Console.Write(array[i] + " ");
            }

            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] == array[i + 1])
                {
                    repeatedCheck++;

                    if (repeatedCheck > repeatedAmount)
                    {
                        repeatedAmount++;
                        repeatedNumber = array[i];
                    }
                }
                else
                {
                    repeatedCheck = 0;
                }
            }

            Console.WriteLine($"\nЦифра с большим повторением подряд: {repeatedNumber}\nОно повторилось {repeatedAmount + 1} раз");

            Console.ReadLine();
        }
    }
}
