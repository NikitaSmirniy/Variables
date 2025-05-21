using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ZooFactory zooFactory = new ZooFactory();

            Zoo zoo = zooFactory.Create(new string[] { "Лев", "Обезьяна", "Попугай", "Медведь" }, new string[] { "Рррр!", "Ууу-ааа!", "Привет!", "Ммммм!" });

            Zookeeper zookeeper = new Zookeeper(zoo);

            zookeeper.Work();
        }
    }

    class Zookeeper
    {
        private const string CommandShow = "1";
        private const string CommandExit = "2";

        private Zoo _zoo;

        public Zookeeper(Zoo zoo)
        {
            _zoo = zoo;
        }

        public void Work()
        {
            bool isOpen = true;

            while (isOpen)
            {
                Console.Clear();

                Console.WriteLine($"Команда {CommandShow} - подойти к вольерам");
                Console.WriteLine($"Команда {CommandExit} - выйти");

                Console.Write("Введите команду: ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandShow:
                        _zoo.ShowEnclosure();
                        break;

                    case CommandExit:
                        isOpen = false;
                        break;

                    default:
                        Console.WriteLine("Введина неверная команда");
                        break;
                }

                Console.ReadKey();
            }
        }
    }

    class Zoo
    {
        private List<Enclosure> _enclosuries;

        public Zoo(List<Enclosure> enclosuries)
        {
            _enclosuries = enclosuries;
        }

        public void ShowEnclosure()
        {
            ShowEnclosuresName();

            Console.Write("Введите номер вольера к которому хотите подойти: ");

            if (TryGetEnclosure(out Enclosure enclosure))
                enclosure.ShowInfo();
        }

        private void ShowEnclosuresName()
        {
            for (int i = 0; i < _enclosuries.Count; i++)
                Console.WriteLine($"{i + 1} {_enclosuries[i].Name}");
        }

        private bool TryGetEnclosure(out Enclosure enclosure)
        {
            if (int.TryParse(Console.ReadLine(), out int userInput))
            {
                if (userInput > 0 && userInput <= _enclosuries.Count)
                {
                    Enclosure chooseEncloser = _enclosuries[userInput - 1];

                    enclosure = chooseEncloser;

                    return true;
                }

                Console.WriteLine($"Вольера под номером {userInput} не существует в нашем зоопарке");

                enclosure = null;

                return false;
            }

            Console.WriteLine("Введено неверное значение");

            enclosure = null;

            return false;
        }
    }

    class EnclosureFactory
    {
        public Enclosure Create(string animalName, string animalSound)
        {
            string name = $"Вольер в котором содержатся животное {animalName}";


            string[] genders = { "Самец", "Самка" };
            string[] personalPronouns = { "он", "она" };

            int minValue = 2;
            int maxValue = 10;
            int animalCount = Randomizer.GenerateRandomValue(minValue, maxValue + 1);

            AnimalFactory animalFactory = new AnimalFactory();

            List<Animal> animals = new List<Animal>();

            for (int i = 0; i < animalCount; i++)
            {
                int randomGender = Randomizer.GenerateRandomValue(0, genders.Length);

                string gender = genders[randomGender];

                string personalPronoun = personalPronouns[randomGender];

                animals.Add(animalFactory.Create(animalName, gender, animalSound, personalPronoun));
            }

            return new Enclosure(name, animals);
        }
    }

    class ZooFactory
    {
        public Zoo Create(string[] animalNames, string[] animalSounds)
        {
            int minValue = 2;
            int maxValue = 4;
            int enclosuresCount = Randomizer.GenerateRandomValue(minValue, maxValue + 1);

            EnclosureFactory enclosureFactory = new EnclosureFactory();

            List<Enclosure> enclosures = new List<Enclosure>();

            for (int i = 0; i < enclosuresCount; i++)
            {
                enclosures.Add(enclosureFactory.Create(animalNames[i], animalSounds[i]));
            }

            return new Zoo(enclosures);
        }
    }

    class AnimalFactory
    {
        public Animal Create(string name, string gender, string sound, string personalPronoun)
        {
            return new Animal(name, gender, sound, personalPronoun);
        }
    }

    class Enclosure
    {
        private List<Animal> _animals;

        public Enclosure(string name, List<Animal> animals)
        {
            _animals = animals;
            Name = name;
        }

        public string Name { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"В этом вольере живут: {Name}");

            foreach (var animal in _animals)
                animal.ShowInfo();
        }
    }

    class Animal
    {
        public Animal(string name, string gender, string sound, string personalPronoun)
        {
            Name = name;
            Gender = gender;
            Sound = sound;
            PersonalPronoun = personalPronoun;
        }

        public string Name { get; private set; }
        public string Gender { get; private set; }
        public string Sound { get; private set; }
        public string PersonalPronoun { get; private set; }

        public void ShowInfo() =>
            Console.WriteLine($"Это {Name} {Gender} пола и {PersonalPronoun} {Sound}");
    }

    static class Randomizer
    {
        private static Random s_random = new Random();

        public static int GenerateRandomValue(int minRandomValue, int maxRandomValue)
        {
            return s_random.Next(minRandomValue, maxRandomValue);
        }
    }
}
