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

            int reapitingNumber = 0;
            int reapitingAmount = 0;

            for (int i = 0; i < array.Length; i++)
            {
                int reapitingCheck = 0;

                array[i] = random.Next(0, maxRandomNumber + 1);

                if (reapitingNumber == array[i])
                {
                    reapitingAmount++;
                }
                else
                {
                    if (reapitingCheck > reapitingAmount)
                    {
                        reapitingNumber = array[i];
                    }

                    reapitingCheck = 0;
                }

                reapitingCheck++;

                Console.Write(array[i] + " ");
            }

            Console.WriteLine($"\nЧисло с большим количеством повторений: {reapitingNumber}\nОно повторилось {reapitingAmount} раз");

            Console.ReadLine();
            }
        }
    }
}
