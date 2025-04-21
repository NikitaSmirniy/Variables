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
        private const int MaxRandomRange = 100;
        private const int MaxNumberOfSeats = 30;

        private List<Train> _trains = new List<Train>();
        private StringDelimiter _stringDelimiter = new StringDelimiter(40);
        private Random _random = new Random();

        public void StartWork()
        {
            bool isOpen = true;

            while (isOpen)
            {
                if (_trains.Count > 0)
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

            Console.WriteLine("Создание маршрута");

            _stringDelimiter.DrawLine();

            Console.Write("Добавьте начало маршрута поезда: ");
            string directionStart = Console.ReadLine();

            _stringDelimiter.DrawLine();

            Console.Write("Добавьте конец маршрута поезда: ");
            string directionUltimate = Console.ReadLine();

            if (directionStart.ToLower() == directionUltimate.ToLower())
            {
                Console.WriteLine("Пункт отправления не должен быть равен пункту прибытия!!!");
                Console.ReadKey();
                return;
            }

            _stringDelimiter.DrawLine();

            Console.Write($"Введите кол-во мест в вагоне(Не меньше одного и не больше {MaxNumberOfSeats}): ");

            if (TryGetMaxNumber(out int wagonCapacity))
            {
                int passangers = SetRandomCapacityTrain();
                _trains.Add(new Train(directionStart, directionUltimate, passangers, AddWagons(wagonCapacity, passangers)));

                Console.WriteLine("Поезд успешно добавлен в список");
            }
            else
            {
                Console.WriteLine("Вы ввели неверное кол-во мест в вагоне!");
            }
        }

        private bool TryGetMaxNumber(out int result)
        {
            int.TryParse(Console.ReadLine(), out int userInput);

            if (userInput > 0 && userInput <= MaxNumberOfSeats)
            {
                result = userInput;
                return true;
            }

            result = 0;
            return false;
        }

        private int SetRandomCapacityTrain()
        {
            return _random.Next(0, MaxRandomRange + 1);
        }

        private Stack<Wagon> AddWagons(int wagonCapacity, int passangers)
        {
            Stack<Wagon> wagons = new Stack<Wagon>();

            while (passangers > 0)
            {
                if (passangers - wagonCapacity > 0)
                {
                    wagons.Push(SetWagonValue(wagonCapacity, wagonCapacity, wagons.Count));
                    passangers -= wagonCapacity;
                }
                else
                {
                    wagons.Push(SetWagonValue(passangers, wagonCapacity, wagons.Count));
                    passangers = 0;
                }
            }

            return wagons;
        }

        private Wagon SetWagonValue(int passangers, int wagonCapacity, int id)
        {
            return new Wagon(passangers, wagonCapacity, id + 1);
        }

        private void ShowAllTrains()
        {
            Console.WriteLine("Информация о поездах");
            _stringDelimiter.DrawLine();

            for (int i = 0; i < _trains.Count; i++)
            {
                Console.WriteLine($"Поезд №{i + 1}");
                _trains[i].ShowAllInfo();
                _stringDelimiter.DrawLine();
            }
        }
    }

    class Train
    {
        private Stack<Wagon> _wagons = new Stack<Wagon>();
        private StringDelimiter _stringDelimiter = new StringDelimiter(40);
        private Direction _direction;
        private int _passangers;

        public Train(string departurePoint, string arrivalPoint, int passangers, Stack<Wagon> wagons)
        {
            _direction = new Direction(departurePoint, arrivalPoint);
            _passangers = passangers;
            _wagons = wagons;
        }

        public void ShowAllInfo()
        {
            Console.WriteLine($"Кол-во пассажиров в поезде: {_passangers}");
            _direction.ShowDirection();

            foreach (var wagon in _wagons)
            {
                _stringDelimiter.DrawLine();
                wagon.ShowAllInfo();
            }
        }
    }

    class Direction
    {
        public Direction(string departurePoint, string arrivalPoint)
        {
            _departurePoint = departurePoint;
            _arrivalPoint = arrivalPoint;
        }

        private string _departurePoint;
        private string _arrivalPoint;

        public void ShowDirection()
        {
            Console.WriteLine($"Направление: {_departurePoint} - {_arrivalPoint}");
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
