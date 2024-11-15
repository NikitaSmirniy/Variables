using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "You  will die. You good  bro  ";
            char[] separators = new char[] { ' ', '.' };

            string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                Console.WriteLine(word);
            }

            Console.ReadLine();
        }
    }
}
