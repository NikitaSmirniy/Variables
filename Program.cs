using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Enclosure[] _enclosures = new Enclosure[4];

            _enclosures[0] = new Enclosure("Вольер с львами", new Animal[]
                {
                new Animal("Лев", AnimalGender.FemaleGender, "Рррр!"),
                new Animal("Львица", AnimalGender.MaleGender, "Рррр!")
                });

            _enclosures[1] = new Enclosure("Вольер с обезьянами", new Animal[]
            {
                new Animal("Обезьяна", AnimalGender.MaleGender, "Ууу-ааа!"),
                new Animal("Обезьяна", AnimalGender.FemaleGender, "Ууу-ааа!"),
                new Animal("Обезьяна", AnimalGender.FemaleGender, "Ууу-ааа!")
            });

            _enclosures[2] = new Enclosure("Вольер с попугаями", new Animal[]
{
                new Animal("Попугай", AnimalGender.FemaleGender, "Кар-кар!"),
                new Animal("Попугай", AnimalGender.MaleGender, "Привет!")
});

            _enclosures[3] = new Enclosure("Вольер с медведями", new Animal[]
{
                new Animal("Медведь", AnimalGender.MaleGender, "Ррррр!"),
                new Animal("Медведь", AnimalGender.FemaleGender, "Ммммм!")
});

            Zoo zoo = new Zoo(_enclosures);

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
                        _zoo.ShowEnclosures();
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
        private Enclosure[] _enclosuries;

        public Zoo(Enclosure[] enclosuries)
        {
            _enclosuries = enclosuries;
        }

        public void ShowEnclosures()
        {
            Console.Write("Введите номер вольера к которому хотите подойти: ");

            if (TryGetEnclosure(out Enclosure enclosure))
                enclosure.ShowInfo();
        }

        private bool TryGetEnclosure(out Enclosure enclosure)
        {
            if (int.TryParse(Console.ReadLine(), out int userInput))
            {
                if (userInput > 0 && userInput <= _enclosuries.Length)
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

    class Enclosure
    {
        private Animal[] _animals = new Animal[0];

        public Enclosure(string name, Animal[] animals)
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
        public Animal(string name, string gender, string sound)
        {
            Name = name;
            Gender = gender;
            Sound = sound;
        }

        public string Name { get; private set; }
        public string Gender { get; private set; }
        public string Sound { get; private set; }

        public void ShowInfo()
        {
            string personalPronoun = GetGender();

            Console.WriteLine($"Это {Name} {Gender} пола и {personalPronoun} {Sound}");
        }

        private string GetGender()
        {
            string malePersonalPronoun = "он";
            string femalePersonalPronoun = "она";

            if (Gender == AnimalGender.MaleGender)
                return malePersonalPronoun;
            else if (Gender == AnimalGender.FemaleGender)
                return femalePersonalPronoun;

            return "";
        }
    }

    static class AnimalGender
    {
        public const string MaleGender = "Самец";
        public const string FemaleGender = "Самка";
    }
}
