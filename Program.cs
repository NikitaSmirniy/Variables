using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium(8);

            AquariumController aquariumController = new AquariumController(aquarium);

            aquariumController.Work();
        }
    }

    class AquariumController
    {
        private const string CommandAddFish = "1";
        private const string CommandRemoveFish = "2";
        private const string CommandExit = "3";

        private Aquarium _aquarium;

        public AquariumController(Aquarium aquarium)
        {
            _aquarium = aquarium;
        }

        public void Work()
        {
            bool isOpen = true;

            while (isOpen)
            {
                _aquarium.ShowAllFishInfo();

                StringDelimiter.DrawLine();

                Console.WriteLine($"Комнада {CommandAddFish} - добавить рыбку");
                Console.WriteLine($"Комнада {CommandRemoveFish} - убрать рыбку");
                Console.WriteLine($"Комнада {CommandExit} - выйти");

                StringDelimiter.DrawLine();

                Console.Write("Введите команду: ");

                string userInput = Console.ReadLine();

                StringDelimiter.DrawLine();

                switch (userInput)
                {
                    case CommandAddFish:
                        _aquarium.AddFish();
                        break;

                    case CommandRemoveFish:
                        _aquarium.RemoveFish();
                        break;

                    case CommandExit:
                        isOpen = false;
                        break;

                    default:
                        Console.WriteLine("Команда не проходит");
                        break;
                }

                Console.ReadLine();
                Console.Clear();

                _aquarium.LifeFish();
            }
        }
    }

    class Aquarium
    {
        private List<Fish> _fish = new List<Fish>();
        private int _capacity;

        public Aquarium(int capacity)
        {
            _capacity = capacity;
        }

        public void LifeFish()
        {
            foreach (var fish in _fish)
                fish.Older();
        }

        public void AddFish() => 
            _fish.Add(new Fish(AddNameFish(), AddLifetimeFish()));

        public void RemoveFish()
        {
            if (TryGetFish(out Fish fish))
                _fish.Remove(fish);
        }

        public void ShowAllFishInfo()
        {
            foreach (var fish in _fish)
                fish.ShowInfo();

            StringDelimiter.DrawLine();

            Console.WriteLine($"Аквариум заполнен на {_fish.Count} из {_capacity} рыб");
        }

        private string AddNameFish()
        {
            string noneNameText = "Никто";

            Console.Write("Как будут звать рыбку: ");
            string nameFish = Console.ReadLine();

            if (nameFish != "")
                return nameFish;

            return noneNameText;
        }

        private int AddLifetimeFish()
        {
            int minLifeTime = 3;
            int maxLifeTime = 8;

            return Randomizer.GenerateRandomValue(minLifeTime, maxLifeTime + 1);
        }

        private bool TryGetFish(out Fish resultingFish)
        {
            List<Fish> foundFish = new List<Fish>();

            Console.Write("Введите имя рыбки из списка, которую вы хотели-бы убрать: ");

            string fishName = Console.ReadLine();

            foreach (var fish in _fish)
            {
                if (fish.Name.ToLower() == fishName.ToLower())
                    foundFish.Add(fish);
            }

            if (foundFish.Count > 1)
            {
                for (int i = 0; i < foundFish.Count; i++)
                {
                    Console.Write($"{i + 1}. ");
                    foundFish[i].ShowInfo();
                }

                StringDelimiter.DrawLine();

                Console.Write("Найдены совпадения, выберите рыбку под нужным номером, что-бы убрать её: ");

                if (int.TryParse(Console.ReadLine(), out int foundFishIndex) && foundFishIndex <= foundFish.Count)
                {
                    Console.WriteLine($"Рыбка под номером {foundFishIndex} была убрана из аквариума");
                    Console.ReadLine();

                    resultingFish = foundFish[foundFishIndex - 1];

                    return true;
                }
                else
                {
                    Console.WriteLine($"Рыбки под номером {foundFishIndex} нет в аквариуме");
                }
            }
            else if (foundFish.Count == 1)
            {
                Console.WriteLine($"Рыбка под именем {fishName} была убрана из аквариума");
                Console.ReadLine();

                resultingFish = foundFish[foundFish.Count - 1];

                return true;
            }
            else
            {
                Console.WriteLine($"Рыбки под именем {fishName} нет в аквариуме");
                Console.ReadLine();
            }

            resultingFish = null;

            return false;
        }
    }

    class Fish
    {
        private int _age;

        public Fish(string name, int lifeTime)
        {
            Name = name;
            Lifetime = lifeTime;
        }

        public string Name { get; private set; }
        public int Lifetime { get; private set; }

        private bool IsAlive => _age < Lifetime;

        public void Older()
        {
            if (IsAlive)
                _age++;
        }

        public void ShowInfo()
        {
            Console.Write($"Рыбка: {Name} ");

            ShowAgeFish();
        }

        private void ShowAgeFish()
        {
            if (IsAlive)
                Console.WriteLine($"ей {_age} лет.");
            else
                Console.WriteLine($"прожила {_age} лет. Её больше нет в живых :(");
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
