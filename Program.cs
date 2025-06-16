using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            PrisonsFactory prisonsFactory = new PrisonsFactory();

            List<Prison> prisons = prisonsFactory.Create(20);

            Database dataBase = new Database(prisons);

            dataBase.Work();
        }
    }

    class Database
    {
        private const string AmnestiedOffense = "Антиправительственное";

        private List<Prison> _prisons;

        public Database(List<Prison> prisons)
        {
            _prisons = prisons;
        }

        public void Work()
        {
            const string CommandAmnestly = "1";
            const string CommandShowAllPrisons = "2";
            const string CommandClearConsole = "3";
            const string CommandExit = "4";

            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine("ГЛАВНОЕ МЕНЮ");

                StringDelimiter.DrawLine();

                Console.WriteLine($"Команда {CommandAmnestly} - провести амнистию");
                Console.WriteLine($"Команда {CommandShowAllPrisons} - показать данные всех преступников");
                Console.WriteLine($"Команда {CommandClearConsole} - очистить консоль");
                Console.WriteLine($"Команда {CommandExit} - выйти");

                Console.Write("Введите команду: ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAmnestly:
                        Amnesty();
                        break;

                    case CommandShowAllPrisons:
                        ShowAllPrisons();
                        break;

                    case CommandClearConsole:
                        Console.Clear();
                        break;

                    case CommandExit:
                        isOpen = false;
                        break;

                    default:
                        Console.WriteLine("Введена неверная команда");
                        break;
                }

                Console.ReadKey();
            }
        }

        private void Amnesty()
        {
            StringDelimiter.DrawLine();

            var filteretPrisons = _prisons.Where(prison => prison.Crime.ToLower() != AmnestiedOffense.ToLower()).Select(prison => prison).ToList();

            _prisons = filteretPrisons;

            Console.ReadKey();
        }

        private void ShowAllPrisons()
        {
            foreach (var prison in _prisons)
                prison.ShowInfo();
        }
    }

    class Prison
    {
        public Prison(string name, string сrime)
        {
            Name = name;
            Crime = сrime;
        }

        public string Name { get; }
        public string Crime { get; }

        public void ShowInfo()
        {
            StringDelimiter.DrawLine();

            Console.WriteLine($"\nИмя: {Name}\nПреступление: {Crime}");
        }
    }

    class PrisonsFactory
    {
        public List<Prison> Create(int count)
        {
            List<Prison> newPrisons = new List<Prison>();

            NameStorage nameStorage = new NameStorage();
            CrimeStorage crimeStorage = new CrimeStorage();

            for (int i = 0; i < count; i++)
            {
                newPrisons.Add(new Prison(GetRandomText(nameStorage.Generate()), GetRandomText(crimeStorage.Generate())));
            }

            return newPrisons;
        }

        private string GetRandomText(List<string> text)
        {
            return text[Randomizer.GenerateRandomValue(text.Count)];
        }
    }

    class NameStorage
    {
        private List<string> _names = new List<string>
        {
            "Jon",
            "Billy",
            "Xerox",
            "Constantine",
            "Vladimir",
            "Alex",
            "Freddy"
        };

        public List<string> Generate()
        {
            return new List<string>(_names);
        }
    }

    class CrimeStorage
    {
        private List<string> _сrimes = new List<string>
        {
            "Антиправительственное",
            "Продажа котиков",
            "Мошенничество",
            "Убийство",
            "Грабёж",
            "Изнас."
        };

        public List<string> Generate()
        {
            return new List<string>(_сrimes);
        }
    }

    static class Randomizer
    {
        private static Random s_random = new Random();

        public static int GenerateRandomValue(int minRandomValue, int maxRandomValue)
        {
            return s_random.Next(minRandomValue, maxRandomValue);
        }

        public static int GenerateRandomValue(int maxRandomValue)
        {
            return s_random.Next(maxRandomValue);
        }
    }

    static class StringDelimiter
    {
        public static void DrawLine(int lineRange = 20)
        {
            Console.WriteLine(new string('-', lineRange));
        }
    }
}
