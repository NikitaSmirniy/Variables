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

            List<Patient> prisons = prisonsFactory.Create(20);

            Database dataBase = new Database(prisons);

            dataBase.Work();
        }
    }

    class Database
    {
        private List<Patient> _patients;

        public Database(List<Patient> patients)
        {
            _patients = patients;
        }

        public void Work()
        {
            const string CommandNameSorting = "1";
            const string CommandAgeSorting = "2";
            const string CommandFindAllPationsByDisease = "3";
            const string CommandShowAllPatients = "4";
            const string CommandClearConsole = "5";
            const string CommandExit = "6";

            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine("ГЛАВНОЕ МЕНЮ");

                StringDelimiter.DrawLine();

                Console.WriteLine($"Команда {CommandNameSorting} - отсортировать пациентов по имени");
                Console.WriteLine($"Команда {CommandAgeSorting} - отсортировать пациентов по возрасту");
                Console.WriteLine($"Команда {CommandFindAllPationsByDisease} - найти пациентов по болезни");
                Console.WriteLine($"Команда {CommandShowAllPatients} - показать данные всех пациентов");
                Console.WriteLine($"Команда {CommandClearConsole} - очистить консоль");
                Console.WriteLine($"Команда {CommandExit} - выйти");

                Console.Write("Введите команду: ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandNameSorting:
                        SortingPatientsByName();
                        break;

                    case CommandAgeSorting:
                        SortingPatientsByAge();
                        break;

                    case CommandFindAllPationsByDisease:
                        FindPatientsByDisease();
                        break;

                    case CommandShowAllPatients:
                        ShowAllPatient();
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

        private void SortingPatientsByName()
        {
            StringDelimiter.DrawLine();

            var filteretPrisons = _patients.OrderBy(patient => patient.Name).ToList();

            _patients = filteretPrisons;

            Console.ReadKey();
        }

        private void SortingPatientsByAge()
        {
            StringDelimiter.DrawLine();

            var filteretPrisons = _patients.OrderBy(patient => patient.Age).ToList();

            _patients = filteretPrisons;

            Console.ReadKey();
        }

        private void FindPatientsByDisease()
        {
            StringDelimiter.DrawLine();

            Console.Write("Введите болезнь пациента: ");

            string userInput = Console.ReadLine();

            var filteretPrisons = _patients.Where(patient => patient.Disease.ToLower() ==  userInput.ToLower())
                .Select(patient => patient).ToList();

            ShowAllPatient(filteretPrisons);

            Console.ReadKey();
        }

        private void ShowAllPatient()
        {
            foreach (var patient in _patients)
                patient.ShowInfo();
        }

        private void ShowAllPatient(List<Patient> patients)
        {
            foreach (var patient in patients)
                patient.ShowInfo();
        }
    }

    class Patient
    {
        public Patient(string name, int age,string disease)
        {
            Name = name;
            Age = age;
            Disease = disease;
        }

        public string Name { get; }
        public int Age { get; }
        public string Disease { get; }

        public void ShowInfo()
        {
            StringDelimiter.DrawLine();

            Console.WriteLine($"\nИмя: {Name}\nВозраст: {Age} лет\nБолезнь: {Disease}");
        }
    }

    class PrisonsFactory
    {
        public List<Patient> Create(int count)
        {
            List<Patient> newPrisons = new List<Patient>();

            NameStorage nameStorage = new NameStorage();
            DiseaseStorage diseaseStorage = new DiseaseStorage();

            int minRandomAge = 18;
            int maxRandomAge = 99;

            for (int i = 0; i < count; i++)
            {
                newPrisons.Add(new Patient(GetRandomText(nameStorage.Generate()), GetRandomValue(minRandomAge, maxRandomAge), GetRandomText(diseaseStorage.Generate())));
            }

            return newPrisons;
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

    class DiseaseStorage
    {
        private List<string> _diseases = new List<string>
        {
            "Лешай",
            "Ожёги",
            "Спид",
            "Вич",
            "Кровотечение",
            "Контузия"
        };

        public List<string> Generate()
        {
            return new List<string>(_diseases);
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
