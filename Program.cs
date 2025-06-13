using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DetailFactory detailFactory = new DetailFactory();
            CarsFactory carsFactory = new CarsFactory();
            StorageFactory storageFactory = new StorageFactory();

            CarService carService = new CarService(carsFactory.Create(detailFactory), storageFactory.Create(5, detailFactory));
            CarServiceHandler carServiceController = new CarServiceHandler(carService);

            carServiceController.Work();
        }
    }

    class CarServiceHandler
    {
        private const string CommandRepair = "1";
        private const string CommandExit = "2";

        private CarService _carService;

        public CarServiceHandler(CarService carService)
        {
            _carService = carService;
        }

        public void Work()
        {
            bool isOpen = true;

            while (isOpen && _carService.BrokenCarsCount > 0)
            {
                Console.Clear();

                Console.WriteLine("Главное меню");

                _carService.ShowCountClient();

                _carService.ShowMoney();

                Console.WriteLine($"Команда {CommandRepair} - приступить к след. клиенту");
                Console.WriteLine($"Команда {CommandExit} - выйти");

                _carService.ShowNextCar();

                _carService.ShowStorage();

                StringDelimiter.DrawLine();

                Console.Write("Введите команду: ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandRepair:
                        _carService.Repair();
                        break;

                    case CommandExit:
                        isOpen = false;
                        break;

                    default:
                        Console.WriteLine("Неверная команда повторите ввод!!!");
                        break;
                }

                Console.ReadKey();
            }

            Console.WriteLine("Работа выполнена");

            Console.ReadKey();
        }
    }

    class CarService
    {
        private Queue<Car> _brokenCars = new Queue<Car>();
        private Storage _storage;

        public CarService(Queue<Car> brokenCars, Storage storage)
        {
            _brokenCars = brokenCars;
            _storage = storage;
        }

        public int Money { get; private set; }
        public int BrokenCarsCount => _brokenCars.Count;

        public void ShowCountClient()
        {
            StringDelimiter.DrawLine();

            Console.WriteLine($"Кол-во клиентов: {BrokenCarsCount}");
        }

        public void Repair()
        {
            const string CommandRepair = "1";
            const string CommandCancell = "2";

            bool isRun = true;

            ShowNextCar();

            Car brokenCar = _brokenCars.Dequeue();

            while (isRun)
            {
                Console.Clear();

                Console.WriteLine($"Команда {CommandRepair} - приступить к починке след. детали");
                Console.WriteLine($"Команда {CommandCancell} - отказать в починке");

                Console.Write("Введите команду: ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandRepair:
                        isRun = ReplaceDetail(brokenCar);
                        break;

                    case CommandCancell:
                        PayPenalty(GetBrokenDetailPrice(brokenCar.GetDetails()));
                        isRun = false;
                        break;

                    default:
                        Console.WriteLine("Неверная команда повторите ввод!!!");
                        break;
                }
            }

            brokenCar.ShowInfo();

            Console.WriteLine("Машина уехала");
        }

        public void ShowMoney()
        {
            StringDelimiter.DrawLine();

            Console.WriteLine($"Ваш счёт: {Money}");
        }

        public void ShowNextCar() =>
            _brokenCars.Peek().ShowInfo();

        public void ShowStorage()
        {
            StringDelimiter.DrawLine();

            _storage.ShowInfo();
        }

        private void PayPenalty(int penalty) =>
            Money -= penalty;

        private void TakeMoney(int sum)
        {
            if (sum > 0)
                Money += sum;
        }

        private bool ReplaceDetail(Car car)
        {
            if (TryGetBrokenDetail(car.GetDetails(), out Detail brokenDetail))
            {
                if (_storage.TryGetNecessaryDetail(brokenDetail.Name, out Detail detail))
                {
                    car.RepairDetail(brokenDetail, detail);

                    TakeMoney(detail.Price);

                    return true;
                }
            }

            return false;
        }

        private bool TryGetBrokenDetail(List<Detail> details, out Detail brokenDetail)
        {
            foreach (var detail in details)
            {
                if (detail.IsBroken)
                {
                    brokenDetail = detail;

                    return true;
                }
            }

            brokenDetail = null;

            return false;
        }

        private int GetBrokenDetailPrice(List<Detail> details)
        {
            if (TryGetBrokenDetail(details, out Detail detail))
                return detail.Price;

            return 0;
        }
    }

    class DetailFactory
    {
        public List<Detail> Create()
        {
            List<Detail> deatails = new List<Detail>()
            {
            new Detail("Fan", 2000),
            new Detail("Windscreen", 2400),
            new Detail("Headlight", 2600),
            new Detail("Bumper", 1800),
            new Detail("Accumulator", 3500),
            new Detail("Engine", 5000),
            new Detail("Filter", 1000),
            };

            return deatails.ToList();
        }
    }

    class CarsFactory
    {
        public Queue<Car> Create(DetailFactory detailFactory)
        {
            Queue<Car> newCars = new Queue<Car>();

            int carsCount = GenerateRandomCount();

            for (int i = 0; i < carsCount; i++)
                newCars.Enqueue(new Car(GenerateRandomTitle(), GetRandomDetails(detailFactory)));

            return newCars;
        }

        private int GenerateRandomCount()
        {
            int minValue = 2;
            int maxValue = 10;

            return Randomizer.GenerateRandomValue(minValue, maxValue + 1);
        }

        private List<Detail> GetRandomDetails(DetailFactory detailFactory)
        {
            List<Detail> parts = detailFactory.Create();

            int maxFailureChance = 100;
            int minFailureChance = 0;
            int failureChance = 25;

            int failureDetails = 0;

            foreach (var part in parts)
            {
                if (failureChance >= Randomizer.GenerateRandomValue(minFailureChance, maxFailureChance))
                {
                    failureDetails++;
                    part.Break();
                }
            }

            if (failureDetails == 0)
            {
                int randomIndex = Randomizer.GenerateRandomValue(0, parts.Count);
                parts[randomIndex].Break();
            }

            return new List<Detail>(parts);
        }

        private string GenerateRandomTitle()
        {
            var carTitles = Enum.GetNames(typeof(CarTitles));
            int carTitlesCount = carTitles.Length;
            string randomCarTitle = carTitles.GetValue(Randomizer.GenerateRandomValue(0, carTitlesCount)).ToString();

            return randomCarTitle;
        }
    }

    class Car
    {
        private List<Detail> _details = new List<Detail>();

        public Car(string name, List<Detail> details)
        {
            Name = name;
            _details = details;
        }

        public string Name { get; private set; }

        public void ShowInfo()
        {
            StringDelimiter.DrawLine();

            Console.WriteLine(Name + "\n");

            foreach (var detail in _details)
                detail.ShowInfo();
        }

        public void RepairDetail(Detail oldDetail, Detail newDetail)
        {
            _details.Remove(oldDetail);
            _details.Add(newDetail);
        }

        public List<Detail> GetDetails()
        {
            return new List<Detail>(_details);
        }
    }

    class StorageFactory
    {
        public Storage Create(int storageCellQuantity, DetailFactory detailFactory)
        {
            return new Storage(CreateStorageCells(storageCellQuantity, detailFactory));
        }

        private List<StorageCell> CreateStorageCells(int storageCellQuantity, DetailFactory detailFactory)
        {
            List<StorageCell> storageCells = new List<StorageCell>();

            List<Detail> details = detailFactory.Create();

            for (int i = 0; i < details.Count; i++)
                storageCells.Add(new StorageCell(details[i], storageCellQuantity, details[i].Price));

            return storageCells;
        }
    }

    class Storage
    {
        private List<StorageCell> _сells;

        public Storage(List<StorageCell> сells)
        {
            _сells = сells;
        }

        public void ShowInfo()
        {
            foreach (var сell in _сells)
            {
                Console.WriteLine("Детали на складе:");

                StringDelimiter.DrawLine();
                сell.ShowInfo();
            }
        }

        public bool TryGetNecessaryDetail(string name, out Detail detail)
        {
            foreach (var storageCell in _сells)
            {
                if (storageCell.Detail.Name == name)
                {
                    detail = storageCell.Detail;

                    storageCell.IssueDetail();

                    return true;
                }
            }

            detail = null;

            return false;
        }
    }

    class StorageCell
    {
        public StorageCell(Detail detail, int quantity, int costPrice)
        {
            Detail = detail;
            Quantity = quantity;
            CostPrice = costPrice;
        }

        public Detail Detail { get; private set; }
        public int Quantity { get; private set; }
        public int CostPrice { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{Detail.Name} - {Quantity}.");
        }

        public void IssueDetail()
        {
            if (Quantity > 0)
                Quantity--;
        }
    }

    class Detail
    {
        public Detail(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public bool IsBroken { get; private set; }
        public int Price { get; protected set; }

        public void ShowInfo()
        {
            string status = GetStatus();

            Console.WriteLine($"{Name} состояние: {status}");
        }

        public void Break()
        {
            IsBroken = true;
        }

        private string GetStatus()
        {
            string bedState = "Плохое";
            string goodState = "Хорошое";

            if (IsBroken)
                return bedState;

            return goodState;
        }
    }

    enum CarTitles
    {
        BMW,
        Lada,
        Tayota,
        Mercedes,
        Wolksvagen
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
