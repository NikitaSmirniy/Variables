using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Dispatcher dispatcher = new Dispatcher();

            dispatcher.StartWork();
        }
    }

    class Dispatcher
    {
        private const string CommandCreateTrain = "1";
        private const string CommandExit = "2";
        private const int MaxRandomDiaposon = 100;
        private const int _maxNumberOfSeats = 30;

        private List<Train> _trains = new List<Train>();
        private StringDelimiter _stringDelimiter = new StringDelimiter(40);

        public void StartWork()
        {
            bool isOpen = true;

            while (isOpen)
            {
                if (TryShowInfo())
                    ShowAllTrains();

                Console.WriteLine($"Команда создать поезд - {CommandCreateTrain}");
                Console.WriteLine($"Команда выйти - {CommandExit}");

                Console.Write("Введите команду: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandCreateTrain:
                        AddTrain();
                        break;

                    case CommandExit:
                        isOpen = false;
                        break;

                    default:
                        Console.WriteLine("Введина неверная команда");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void AddTrain()
        {
            _stringDelimiter.DrawLine();

            Console.Write("Добавьте начало маршрута поезда: ");
            string directionStart = Console.ReadLine();

            _stringDelimiter.DrawLine();

            Console.Write("Добавьте конец маршрута поезда: ");
            string directionUltimate = Console.ReadLine();

            _stringDelimiter.DrawLine();

            Console.Write($"Введите кол-во мест в вагоне(Не меньше одного и не больше {_maxNumberOfSeats}): ");

            if (TryGetMaxNumber(out int wagonCapacity))
            {
                _trains.Add(new Train(directionStart, directionUltimate, MaxRandomDiaposon, wagonCapacity));

                Console.WriteLine("Поезд успешно добавлен в список");
            }
            else
            {
                Console.WriteLine("Вы ввели неверные условия попробуйте снова!");
            }
        }

        private bool TryGetMaxNumber(out int result)
        {
            int.TryParse(Console.ReadLine(), out int userInput);

            if (userInput > 0 && userInput <= _maxNumberOfSeats)
            {
                result = userInput;
                return true;
            }

            result = 0;
            return false;
        }

        private void ShowAllTrains()
        {
            Console.WriteLine("Информация о поездах");
            _stringDelimiter.DrawLine();

            foreach (var train in _trains)
            {
                train.ShowAllInfo();
                _stringDelimiter.DrawLine();
            }
        }

        private bool TryShowInfo()
        {
            if (_trains.Count > 0)
                return true;

            return false;
        }
    }

    class Train
    {
        private Stack<Wagon> _wagons = new Stack<Wagon>();
        private StringDelimiter _stringDelimiter = new StringDelimiter(40);
        private Random _random = new Random();
        private string _directionStart;
        private string _directionUltimate;
        private int _passangers;

        public Train(string directionStart, string directionUltimate, int maxRandomDiaposon, int wagonCapacity)
        {
            _directionStart = directionStart;
            _directionUltimate = directionUltimate;
            _passangers = _random.Next(0, maxRandomDiaposon + 1);

            AddWagons(wagonCapacity, _passangers);
        }

        public int WagonsCount => _wagons.Count;

        public void ShowAllInfo()
        {
            Console.WriteLine($"Направление: {_directionStart} - {_directionUltimate}\n" +
                $"Кол-во пассажиров в поезде: {_passangers}");

            foreach (var wagon in _wagons)
            {
                _stringDelimiter.DrawLine();
                wagon.ShowAllInfo();
            }
        }

        public void AddWagons(int wagonCapacity, int passangers)
        {
            while (passangers > 0)
            {
                if (passangers - wagonCapacity > 0)
                {
                    _wagons.Push(new Wagon(wagonCapacity, wagonCapacity, WagonsCount + 1));
                    passangers -= wagonCapacity;
                }
                else
                {
                    _wagons.Push(new Wagon(passangers, wagonCapacity, WagonsCount + 1));
                    passangers = 0;
                }
            }
        }
    }

    class Wagon
    {
        private int _wagonPassangers;
        private int _id;

        public Wagon(int numberOfPassanger, int maxNumberOfPassanger, int id)
        {
            _wagonPassangers = numberOfPassanger;
            WagonCapacity = maxNumberOfPassanger;
            _id = id;
        }

        public int WagonCapacity { get; private set; }

        public void ShowAllInfo()
        {
            Console.WriteLine($"Вагон №{_id}. Пассажиры в вагоне {_wagonPassangers}/{WagonCapacity}");
        }
    }

    class StringDelimiter
    {
        private int _lineRange = 20;

        public StringDelimiter(int lineRange)
        {
            _lineRange = lineRange;
        }

        public void DrawLine()
        {
            Console.WriteLine(new string('_', _lineRange));
        }
    }
}
