using System;

namespace ConsoleApp1
{
    class Program
    {   
        static void Main(string[] args)
        {
            int[] array = new int[36];

            Random random = new Random();
            int maxRandomNumber = 9;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (random.Next(0, maxRandomNumber + 1));

                ShowArray(array, i);
            }

            Console.WriteLine();

            Shuffle(array);

            Console.ReadLine();
        }

        static void Shuffle(int[] array)
        {
            int[] tempArray = new int[array.Length];
            Random random = new Random();

            for (int i = 0; i < tempArray.Length; i++)
            {
                int randomIndex = random.Next(0, array.Length);

                tempArray[i] = array[randomIndex];
                RemoveAt(ref array, randomIndex);

                ShowArray(tempArray, i);
            }

            array = tempArray;
        }

        static void ShowArray(int[] array, int indexArray)
        {
            if (indexArray < array.Length - 1)
                Console.Write($"{array[indexArray]}, ");
            else
                Console.Write($"{array[indexArray]}");
        }

        static void RemoveAt(ref int[] array, int index)
        {
            int[] newArray = new int[array.Length - 1];

            for (int i = 0; i < index; i++)
                newArray[i] = array[i];

            for (int i = index + 1; i < array.Length; i++)
                newArray[i - 1] = array[i];

            array = newArray;
        }
    }
}
