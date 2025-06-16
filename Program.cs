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

            DataBase dataBase = new DataBase(prisons);

            dataBase.Work();
        }
    }

    class DataBase
    {
        private List<Prison> _prisons;

        public DataBase(List<Prison> prisons)
        {
            _prisons = prisons;
        }

        public void Work()
        {
            const string CommandFindPrisons = "1";
            const string CommandShowAllPrisons = "2";
            const string CommandClearConsole = "3";
            const string CommandExit = "4";

            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine("ГЛАВНОЕ МЕНЮ");

                StringDelimiter.DrawLine();

                Console.WriteLine($"Команда {CommandFindPrisons} - найти преступников по данным");
                Console.WriteLine($"Команда {CommandShowAllPrisons} - показать данные всех преступников");
                Console.WriteLine($"Команда {CommandClearConsole} - очистить консоль");
                Console.WriteLine($"Команда {CommandExit} - выйти");

                Console.Write("Введите команду: ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandFindPrisons:
                        FindPrisons();
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

        private void FindPrisons()
        {
            StringDelimiter.DrawLine();

            Console.WriteLine("Поиск преступника будет происходить по параметрам(рост, вес, национальность)");

            Console.Write("Рост: ");

            int.TryParse(Console.ReadLine(), out int height);

            Console.Write("Вес: ");

            int.TryParse(Console.ReadLine(), out int weight);

            Console.Write("Нация: ");

            string nationality = Console.ReadLine();

            var filteretPrisons = _prisons.Where(prison => prison.Height == height && prison.Weight
            == weight && prison.Nationality.ToLower() == nationality.ToLower() && prison.IsConcluded == false).ToList();

            ShowAllPrisons(filteretPrisons);

            Console.ReadKey();
        }

        private void ShowAllPrisons()
        {
            foreach (var prison in _prisons)
                prison.ShowInfo();
        }

        private void ShowAllPrisons(List<Prison> prisons)
        {
            foreach (var prison in prisons)
                prison.ShowInfo();
        }
    }

    class Prison
    {
        public Prison(string name, bool isServingTime, int height, int weight, string nationality)
        {
            Name = name;
            IsConcluded = isServingTime;
            Height = height;
            Weight = weight;
            Nationality = nationality;
        }

        public string Name { get; }
        public bool IsConcluded { get; }
        public int Height { get; }
        public int Weight { get; }
        public string Nationality { get; }

        public void ShowInfo()
        {
            StringDelimiter.DrawLine();

            string isConcludedText = GetConcludedText();

            Console.WriteLine($"{isConcludedText}\nИмя: {Name}\nРост: {Height}\nВес: {Weight}\nНация: {Nationality}");
        }

        private string GetConcludedText()
        {
            string concludedText = "Заключен";
            string unconcludedText = "Не заключен";

            return IsConcluded == true ? concludedText : unconcludedText;
        }
    }

    class PrisonsFactory
    {
        public List<Prison> Create(int count)
        {
            List<Prison> newPrisons = new List<Prison>();

            NameStorage nameStorage = new NameStorage();
            NationalyStorage nationalyStorage = new NationalyStorage();

            int minHeight = 150;
            int maxHeight = 210;

            int minWeight = 40;
            int maxWeight = 170;

            for (int i = 0; i < count; i++)
            {
                newPrisons.Add(new Prison(GetRandomText(nameStorage.Generate()), GetRandomBoolean(),
                GetRandomValue(minHeight, maxHeight), GetRandomValue(minWeight, maxWeight), GetRandomText(nationalyStorage.Generate())));
            }

            return newPrisons;
        }

        private bool GetRandomBoolean()
        {
            bool[] booleanArray = { true, false };

            return booleanArray[Randomizer.GenerateRandomValue(0, booleanArray.Length)];
        }

        private string GetRandomText(List<string> text)
        {
            return text[Randomizer.GenerateRandomValue(0, text.Count)];
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

    class NationalyStorage
    {
        private List<string> _nationaly = new List<string>
        {
            "Russian",
            "German",
            "Japanese",
            "Chinese",
            "American",
            "Italian"
        };

        public List<string> Generate()
        {
            return new List<string>(_nationaly);
        }
    }

    static class Randomizer
    {
        private static Random s_random = new Random();

        public static int GenerateRandomValue(int minRandomValue, int maxRandomValue)
        {
            return s_random.Next(minRandomValue, maxRandomValue);
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
