using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            BeensFactory beensFactory = new BeensFactory();

            List<Beens> beens = beensFactory.Create(20);

            Database database = new Database(beens);

            database.Work();
        }
    }

    class Database
    {
        private List<Beens> _beens;

        private int _todayDate = 2025;

        public Database(List<Beens> beens)
        {
            _beens = beens;
        }

        public void Work()
        {
            const string CommandShowAllOverdueBeens = "1";
            const string CommandShowAllBeens = "2";
            const string CommandClearConsole = "3";
            const string CommandExit = "4";

            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine("ГЛАВНОЕ МЕНЮ");

                StringDelimiter.DrawLine();

                Console.WriteLine($"Команда {CommandShowAllOverdueBeens} - показать все просроченные консервы");
                Console.WriteLine($"Команда {CommandShowAllBeens} - показать все консервы");
                Console.WriteLine($"Команда {CommandClearConsole} - очистить консоль");
                Console.WriteLine($"Команда {CommandExit} - выйти");

                Console.Write("Введите команду: ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandShowAllOverdueBeens:
                        ShowAllOverdueBeens();
                        break;

                    case CommandShowAllBeens:
                        ShowAllBeens();
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

        private void ShowAllOverdueBeens()
        {
            var overdueBeens = _beens.Where(beens => beens.ProductionDate + beens.BestBeforeDate < _todayDate)
                .Select(beens => beens).ToList();

            ShowAllBeens(overdueBeens);
        }

        private void ShowAllBeens()
        {
            foreach (var been in _beens)
                been.ShowInfo();
        }

        private void ShowAllBeens(List<Beens> beens)
        {
            foreach (var been in beens)
                been.ShowInfo();
        }
    }

    class Beens
    {
        public Beens(string name, int productionDate, int bestBeforeDate)
        {
            Name = name;
            ProductionDate = productionDate;
            BestBeforeDate = bestBeforeDate;
        }

        public string Name { get; }
        public int ProductionDate { get; }
        public int BestBeforeDate { get; }

        public void ShowInfo()
        {
            StringDelimiter.DrawLine();

            Console.WriteLine($"\nИмя: {Name}\nДата производства: {ProductionDate}\nСрок годности: {BestBeforeDate}");
        }
    }

    class BeensFactory
    {
        public List<Beens> Create(int count)
        {
            List<Beens> newBeens = new List<Beens>();

            NameStorage nameStorage = new NameStorage();

            int minRandomProductionDate = 2019;
            int maxRandomProductionDate = 2025;

            int minRandomBestBeforeDate = 2;
            int maxRandomBestBeforeDate = 6;

            for (int i = 0; i < count; i++)
            {
                string randomName = GetRandomText(nameStorage.Generate());
                int randomProductionDate = GetRandomValue(minRandomProductionDate, maxRandomProductionDate);
                int randomBestBeforeDate = GetRandomValue(minRandomBestBeforeDate, maxRandomBestBeforeDate);

                newBeens.Add(new Beens(randomName, randomProductionDate, randomBestBeforeDate));
            }

            return newBeens;
        }

        private string GetRandomText(List<string> text)
        {
            return text[Randomizer.GenerateRandomValue(text.Count)];
        }

        private int GetRandomValue(int minValue, int maxValue)
        {
            return Randomizer.GenerateRandomValue(minValue, maxValue + 1);
        }
    }

    class NameStorage
    {
        private List<string> _names = new List<string>
        {
            "Beenes",
            "Billy Beens",
            "Sweety Beens",
            "Red Beens"
        };

        public List<string> Generate()
        {
            return new List<string>(_names);
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
