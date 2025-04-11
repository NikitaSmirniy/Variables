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
            const string CommandBan = "3";
            const string CommandUnban = "4";
            const string CommandExit = "5";

            List<Player> players = new List<Player>();
            Database databaze = new Database(players);

            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine($"Команда {CommandAdd} - добавить игрока");
                Console.WriteLine($"Команда {CommandDelete} - удалить игрока");
                Console.WriteLine($"Команда {CommandBan} - бан игрока");
                Console.WriteLine($"Команда {CommandUnban} - разбан игрока");
                Console.WriteLine($"Команда {CommandExit} - выйти");

                databaze.ShowAllPlayers();

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAdd:
                        databaze.AddPlayer();
                        break;
                    case CommandDelete:
                        databaze.DeletePlayer();
                        break;
                    case CommandBan:
                        databaze.Ban();
                        break;
                    case CommandUnban:
                        databaze.Unban();
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
        public Player(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; } = 1;
        public bool IsBanned { get; private set; }

        public void Ban()
        {
            IsBanned = true;
        }

        public void Unban()
        {
            IsBanned = false;
        }
    }

    class Database
    {
        private List<Player> _players;
        private int _lastId;

        public Database(List<Player> players)
        {
            _players = players;
        }

        public void AddPlayer()
        {
            Console.Write("Введите имя игрока: ");
            string playerName = Console.ReadLine();

            _lastId++;
            _players.Add(new Player(_lastId, playerName));
        }

        public void DeletePlayer()
        {
            if (TryGetPlayer(out Player player))
                _players.Remove(player);
        }

        public void Ban()
        {
            if (TryGetPlayer(out Player player))
            {
                if (player.IsBanned)
                    player.Unban();
            }
        }

        public void Unban()
        {
            if (TryGetPlayer(out Player player))
            {
                if (player.IsBanned == false)
                    player.Ban();
            }
        }

        private bool TryGetPlayer(out Player foundPlayer)
        {
            Console.Write("Введите номер игрока: ");
            int.TryParse(Console.ReadLine(), out int userInput);

            foreach (var player in _players)
            {
                if (player.Id == userInput)
                {
                    foundPlayer = player;

                    return true;
                }
            }

            foundPlayer = null;

            return false;
        }

        public void ShowAllPlayers()
        {
            for (int i = 0; i < _players.Count; i++)
            {
                Console.WriteLine(new string('_', 20));
                Console.WriteLine($"{i + 1}. игрок\nИмя: {_players[i].Name}\n" +
                    $"Уровень: {_players[i].Level}\n{GetBanedPlayer(i)}");
            }
        }

        private string GetBanedPlayer(int index)
        {
            if (_players[index].IsBanned)
                return "Находится в бане";
            else
                return "Не находится в бане";
        }
    }
}
