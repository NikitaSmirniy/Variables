using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }

    class Database
    {

        public Database(List<Soldier> soldiers1, List<Soldier> soldiers2)
        {
            _soldiers1 = soldiers1;
            _soldiers2 = soldiers2;
        }

        public void Work()
        {
            const string CommandTranslateSoldiers = "1";
            const string CommandShowAllSoldiers1 = "2";
            const string CommandShowAllSoldiers2 = "3";
            const string CommandClearConsole = "4";
            const string CommandExit = "7";

            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine("ГЛАВНОЕ МЕНЮ");

                StringDelimiter.DrawLine();

                Console.WriteLine($"Команда {CommandTranslateSoldiers} - перевести солдат из 1 во 2 группу");
                Console.WriteLine($"Команда {CommandShowAllSoldiers1} - показать всех солдат из 1 команды");
                Console.WriteLine($"Команда {CommandShowAllSoldiers2} - показать всех солдат из 2 команды");
                Console.WriteLine($"Команда {CommandClearConsole} - очистить консоль");
                Console.WriteLine($"Команда {CommandExit} - выйти");

                Console.Write("Введите команду: ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandTranslateSoldiers:
                        TranslateSoldiers();
                        break;

                    case CommandShowAllSoldiers1:
                        ShowAllSoldiers(_soldiers1);
                        break;

                    case CommandShowAllSoldiers2:
                        ShowAllSoldiers(_soldiers2);
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

        private void TranslateSoldiers()
        {
            const string FilteredSymbol = "б";

            var filtered1 = _soldiers1.Where(soldier => soldier.Name.ToLower().StartsWith(FilteredSymbol));

            _soldiers1 = _soldiers1.Except(filtered1).ToList();

            _soldiers2 = _soldiers2.Union(filtered1).ToList();
        }

        private void ShowAllSoldiers(List<Soldier> soldiers)
        {
            foreach (var soldier in soldiers)
                soldier.ShowInfo();
        }
    }

    class Soldier
    {
        public Soldier(string name, string armament, int rank, int serviceLife)
        {
            Name = name;
            Armament = armament;
            Rank = rank;
            ServiceLife = serviceLife;
        }

        public string Name { get; }
        public string Armament { get; }
        public int Rank { get; }
        public int ServiceLife { get; }

        public void ShowInfo()
        {
            StringDelimiter.DrawLine();

            Console.WriteLine($"\nИмя: {Name}\nВооружение: {Armament}\nЗвание: {Rank}\nСрок службы: {ServiceLife} (месяцев)");
        }
    }

    class SoldiersFactory
    {
        public List<Soldier> Create(int count)
        {
            List<Soldier> newSoldiers = new List<Soldier>();

            NameStorage nameStorage = new NameStorage();
            ArmamentStorage armamentStorage = new ArmamentStorage();

            int minRandomRank = 1;
            int maxRandomRank = 6;

            int minRandomServiceLife = 6;
            int maxRandomServiceLife = 24;

            for (int i = 0; i < count; i++)
            {
                string randomName = GetRandomText(nameStorage.Generate());
                string randomArmament = GetRandomText(armamentStorage.Generate());
                int randomServiceLife = GetRandomValue(minRandomServiceLife, maxRandomServiceLife);
                int randomRank = GetRandomValue(minRandomRank, maxRandomRank);

                newSoldiers.Add(new Soldier(randomName, randomArmament, randomRank, randomServiceLife));
            }

            return newSoldiers;
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
            "Бабиджон",
            "Билли",
            "Xerox",
            "Бебра",
            "Vladimir",
            "Alex",
            "Freddy"
        };

        public List<string> Generate()
        {
            return new List<string>(_names);
        }
    }

    class ArmamentStorage
    {
        private List<string> _armaments = new List<string>
        {
            "m4",
            "ak74",
            "AVP"
        };

        public List<string> Generate()
        {
            return new List<string>(_armaments);
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
