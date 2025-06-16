using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            PlaersFactory prisonsFactory = new PlaersFactory();

            List<Player> prisons = prisonsFactory.Create(20);

            Database dataBase = new Database(prisons);

            dataBase.Work();
        }
    }

    class Database
    {
        private List<Player> _players;

        public Database(List<Player> players)
        {
            _players = players;
        }

        public void Work()
        {
            const string CommandTopByLevel = "1";
            const string CommandTopByForce = "2";
            const string CommandShowAllPlayers = "3";
            const string CommandClearConsole = "4";
            const string CommandExit = "5";

            int topVelocity = 3;

            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine("ГЛАВНОЕ МЕНЮ");

                StringDelimiter.DrawLine();

                Console.WriteLine($"Команда {CommandTopByLevel} - показать топ 3 лучших игроков по уровню");
                Console.WriteLine($"Команда {CommandTopByForce} - показать топ 3 лучших игроков по силе");
                Console.WriteLine($"Команда {CommandShowAllPlayers} - показать данные всех игроков");
                Console.WriteLine($"Команда {CommandClearConsole} - очистить консоль");
                Console.WriteLine($"Команда {CommandExit} - выйти");

                Console.Write("Введите команду: ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandTopByLevel:
                        FindTopPlayersByLevel(topVelocity);
                        break;

                    case CommandTopByForce:
                        FindTopPlayersByForce(topVelocity);
                        break;

                    case CommandShowAllPlayers:
                        ShowAllPlayers();
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

        private void FindTopPlayersByLevel(int topPlayersCount)
        {
            StringDelimiter.DrawLine();

            var filteretPlayers = _players.OrderByDescending(player => player.Level).ToList();

            int typedTop = 0;

            var topPlayers = filteretPlayers.TakeWhile(player => typedTop++ < topPlayersCount).ToList();

            ShowTop(topPlayers);

            Console.ReadKey();
        }

        private void FindTopPlayersByForce(int topPlayersCount)
        {
            StringDelimiter.DrawLine();

            var filteretPlayers = _players.OrderByDescending(player => player.Force).ToList();

            int topCount = 0;

            var topPlayers = filteretPlayers.TakeWhile(player => topCount++ < topPlayersCount).ToList();

            ShowTop(topPlayers);

            Console.ReadKey();
        }

        private void ShowAllPlayers()
        {
            foreach (var player in _players)
                player.ShowInfo();
        }

        private void ShowTop(List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Место");

                players[i].ShowInfo();
            }
        }
    }

    class Player
    {
        public Player(string name, int level, int force)
        {
            Name = name;
            Level = level;
            Force = force;
        }

        public string Name { get; }
        public int Level { get; }
        public int Force { get; }

        public void ShowInfo()
        {
            StringDelimiter.DrawLine();

            Console.WriteLine($"\nИмя: {Name}\nУровень: {Level} lvl\nСила: {Force}");
        }
    }

    class PlaersFactory
    {
        public List<Player> Create(int count)
        {
            List<Player> newPlayers = new List<Player>();

            NameStorage nameStorage = new NameStorage();

            int minRandomLevel = 1;
            int maxRandomLevel = 99;

            int minRandomForce = 1;
            int maxRandomForce = 49;

            for (int i = 0; i < count; i++)
            {
                string randomName = GetRandomText(nameStorage.Generate());
                int randomLevel = GetRandomValue(minRandomLevel, maxRandomLevel);
                int randomForce = GetRandomValue(minRandomForce, maxRandomForce);

                newPlayers.Add(new Player(randomName, randomLevel, randomForce * randomLevel));
            }

            return newPlayers;
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
