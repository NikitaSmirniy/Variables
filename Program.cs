using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] array1 = { "1", "2", "3" };
            string[] array2 = { "2", "4", "5" };

            List<string> numbers = new List<string>();

            AddUniqueValues(array1, numbers);
            AddUniqueValues(array2, numbers);

            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            Console.ReadKey();
        }

        static void AddUniqueValues(string[] array, List<string> numbers)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (numbers.Contains(array[i]) == false)
                    numbers.Add(array[i]);
            }
        }
    }
}
