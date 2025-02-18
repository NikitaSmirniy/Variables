using System;

namespace ConsoleApp1
{
    class Program
    {   
        static void Main(string[] args)
        {
            const string RussiaWordCommand = "Россия";
            const string CanadaWordCommand = "Канада";
            const string ChinaWordCommand = "Китай";
            const string USAWordCommand = "США";
            const string BrazilWordCommand = "Бразилия";

            const string ExitCommand = "выход";

            Dictionary<string, int> explanatoryDictionary = new Dictionary<string, int>();

            bool isOpen = true;

            explanatoryDictionary.Add(RussiaWordCommand, 1);
            explanatoryDictionary.Add(CanadaWordCommand, 2);
            explanatoryDictionary.Add(ChinaWordCommand, 3);
            explanatoryDictionary.Add(USAWordCommand, 4);
            explanatoryDictionary.Add(BrazilWordCommand, 5);

            while (isOpen)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("Введите любую из этих стран, что-бы узнать на каком месте она по площади в мире");

                foreach (var item in explanatoryDictionary.Keys)
                {
                    Console.WriteLine(item);
                }

                string userInput = Console.ReadLine();

                if (explanatoryDictionary.ContainsKey(userInput))
                    Console.WriteLine($"{userInput} находится на {explanatoryDictionary[userInput]} месте по площади в мире");
                else
                    Console.WriteLine("Такой страны нет в списке");
                ShowMessage(ConsoleColor.Red, $"Напишите комманду {ExitCommand}, что-бы выйти");
                
                userInput = Console.ReadLine();

                if (userInput == ExitCommand)
                    isOpen = false;
            }

            void ShowMessage(ConsoleColor color, string message)
            {
                Console.ForegroundColor = color;
                Console.Write(message);
            }
        }
    }
}
