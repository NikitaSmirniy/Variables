using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandAdd = "1";
            const string CommandDelete = "2";
            const string CommandBaned = "3";
            const string CommandExit = "4";

            List<Player> players = new List<Player>();
            DataBase dataBaze = new DataBase(ref players);

            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine($"Команда {CommandAdd} - добавить игрока");
                Console.WriteLine($"Команда {CommandDelete} - удалить игрока");
                Console.WriteLine($"Команда {CommandBaned} - бан/разбан игрока");
                Console.WriteLine($"Команда {CommandExit} - выйти");

                dataBaze.ShowAllPlayers();

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAdd:
                        dataBaze.AddPlayer();
                        break;
                    case CommandDelete:
                        dataBaze.DeletePlayer();
                        break;
                    case CommandBaned:
                        dataBaze.Baned();
                        break;
                    case CommandExit:
                        isOpen = false;
                        break;
                    default:
                        Console.WriteLine("Введена неверная команда");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class Player
    {
        public Player(int number, string name)
        {
            Number = number;
            Name = name;
        }

        public int Number { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; } = 1;
        public bool IsBaned { get; set; }
    }

    class DataBase
    {
        private List<Player> _players;

        public DataBase(ref List<Player> players)
        {
            _players = players;
        }

        public void AddPlayer()
        {
            Console.Write("Введите имя игрока:");
            string playerName = Console.ReadLine();

            _players.Add(new Player(_players.Count + 1, playerName));
        }

        public void DeletePlayer()
        {
            Console.Write("Введите номер игрока: ");
            int.TryParse(Console.ReadLine(), out int userInput);

            if (userInput > 0 && userInput <= _players.Count)
                _players.RemoveAt(userInput - 1);
        }

        public void Baned()
        {
            Console.Write("Введите номер игрока: ");
            int.TryParse(Console.ReadLine(), out int userInput);

            if (userInput <= _players.Count && userInput > 0)
            {
                if (_players[userInput - 1].IsBaned)
                    _players[userInput - 1].IsBaned = false;
                else
                    _players[userInput - 1].IsBaned = true;
            }
        }

        public void ShowAllPlayers()
        {
            for (int i = 0; i < _players.Count; i++)
            {
                Console.WriteLine(new string('_', 20));
                Console.WriteLine($"{_players[i].Number}. игрок\nИмя: {_players[i].Name}\n" +
                    $"Уровень: {_players[i].Level}\n{CheckBanedPlayer(_players[i].Number)}");
            }
        }

        public string CheckBanedPlayer(int index)
        {
            if (_players[index - 1].IsBaned)
                return "Находится в бане";
            else
                return "Не находится в бане";
        }
    }
}
