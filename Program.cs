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

            for (int i = 0; i < array.Length; i++)
            {
                int repeatedCheck = 0;

                array[i] = random.Next(0, maxRandomNumber + 1);

                if (repeatedNumber == array[i])
                {
                    repeatedAmount++;
                }
                else
                {
                    if (repeatedCheck > repeatedAmount)
                    {
                        repeatedNumber = array[i];
                    }

                    repeatedCheck = 0;
                }

                repeatedCheck++;

                Console.Write(array[i] + " ");
            }

            Console.WriteLine($"\nЧисло с большим количеством повторений: {repeatedNumber}\nОно повторилось {repeatedAmount} раз");

            Console.ReadLine();
            }
        }
    }
}
