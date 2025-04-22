using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Dispatcher dispatcher = new Dispatcher();

            dispatcher.Work();
        }
    }

    class Dispatcher
    {
        private const string CommandCreateTrain = "1";
        private const string CommandExit = "2";

        private List<Train> _trains = new List<Train>();
        private StringDelimiter _stringDelimiter = new StringDelimiter(40);
        private Random _random = new Random();

        public void Work()
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
                        CreateTrain();
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

        private void CreateTrain()
        {
            int maxNumberOfSeats = 30;
            _stringDelimiter.DrawLine();

            Direction direction = CreateDirection();
            int tickets = SellTickets();
            List<Wagon> wagons = CreateWagons(maxNumberOfSeats, tickets);

            _trains.Add(new Train(direction, tickets, wagons));
        }

        private Direction CreateDirection()
        {
            string arrivalPoint = "";
            string departurePoint = "";

            Console.WriteLine("Создание маршрута");

            while (departurePoint.ToLower() == arrivalPoint.ToLower())
            {
                Console.Write("Добавьте начало маршрута поезда: ");
                departurePoint = Console.ReadLine();

                _stringDelimiter.DrawLine();

                Console.Write("Добавьте конец маршрута поезда: ");
                arrivalPoint = Console.ReadLine();
            }

            return new Direction(departurePoint, arrivalPoint);
        }

        private int SellTickets()
        {
            int minRandomValue = 100;
            int maxRandomValue = 700;
            
            return _random.Next(minRandomValue, maxRandomValue + 1);
        }

        private List<Wagon> CreateWagons(int wagonCapacity, int tickets)
        {
            List<Wagon> wagons = new List<Wagon>();

            int wagonsCount = tickets / wagonCapacity;

            if (IsNeedAdditionalWagon(tickets, wagonCapacity))
                wagonsCount++;

            for (int i = 0; i < wagonsCount; i++)
            {
                wagons.Add(new Wagon(wagonCapacity));
            }

            return wagons;
        }

        private bool IsNeedAdditionalWagon(int tickets, int wagonCapacity)
        {
            return tickets % wagonCapacity != 0;
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
        private List<Wagon> _wagons = new List<Wagon>();
        private StringDelimiter _stringDelimiter = new StringDelimiter(40);
        private Direction _direction;
        private int _passangers;

        public Train(Direction direction, int passangers, List<Wagon> wagons)
        {
            _direction = direction;
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
        private string _departure;
        private string _arrival;

        public Direction(string departure, string arrival)
        {
            _departure = departure;
            _arrival = arrival;
        }

        public void ShowDirection()
        {
            Console.WriteLine($"Направление: {_departure} - {_arrival}");
        }
    }

    class Wagon
    {
        private int _capacity;

        public Wagon(int capacity)
        {
            _capacity = capacity;
        }

        public void ShowAllInfo()
        {
            Console.WriteLine($"Вместимость вагона: {_capacity}");
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
