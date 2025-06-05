using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            CarsFactory carsFactory = new CarsFactory();
            StorageFactory storageFactory = new StorageFactory();

            CarService carService = new CarService(carsFactory.Create(), storageFactory.Create(5));
            CarServiceController carServiceController = new CarServiceController(carService);

            carServiceController.Work();
        }
    }

    class CarServiceController
    {
        private const string CommandRepair = "1";
        private const string CommandExit = "2";

        private CarService _carService;

        public CarServiceController(CarService carService)
        {
            _carService = carService;
        }

        public void Work()
        {
            bool isOpen = true;

            while (isOpen && _carService.BrokenCarsCount > 0)
            {
                Console.Clear();

                _carService.ShowClients();

                Console.WriteLine($"Команда {CommandRepair} - приступить к след. клиенту");
                Console.WriteLine($"Команда {CommandExit} - выйти");

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

        public void TakeMoney(int sum)
        {
            if (sum > 0)
                Money += sum;
            else
                Money++;
        }

        public void ShowMoney() =>
            Console.WriteLine($"Ваш счёт: {Money}");

        public void ShowClients()
        {
            foreach (var brokenCar in _brokenCars)
            {
                brokenCar.ShowInfo();
                StringDelimiter.DrawLine();
            }
        }

        public void Repair()
        {
            const string CommandRepair = "1";
            const string CommandCancell = "2";

            var brokenCar = _brokenCars.Peek();

            for (int i = 0; i < brokenCar.DetailsCount; i++)
            {
                Console.Clear();

                Console.WriteLine($"Если хотите начать ремонт машины введите {CommandRepair}");
                Console.WriteLine($"Если не хотите начинать ремонт машины введите {CommandCancell}");

                Console.Write("Введите команду: ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandRepair:
                        ReplaceDetail(brokenCar.GetDetails(), brokenCar);
                        break;

                    case CommandCancell:
                        PayPenalty(GetBrokenDetailPrice(brokenCar.GetDetails()));
                        break;

                    default:
                        Console.WriteLine("Неверная команда повторите ввод!!!");
                        break;
                }

                ShowMoney();

                _storage.ShowInfo();

                Console.ReadKey();
            }

            brokenCar.ShowInfo();

            TakeOutClient();

            Console.WriteLine("Машина уехала");
        }

        private void PayPenalty(int penalty) =>
            Money -= penalty;

        private void TakeOutClient()
        {
            if (BrokenCarsCount > 0)
                _brokenCars.Dequeue();
        }

        private void ReplaceDetail(List<Detail> details, Car car)
        {
            if (TryGetBrokenDetail(details, out Detail brokenDetail))
            {
                if (_storage.TryTakeDetail(brokenDetail.Name, out Detail detail))
                {
                    car.RepairDetail(brokenDetail, detail);

                    TakeMoney(detail.Price);
                }
            }
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
        public List<Detail> GetDetails()
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
        public Queue<Car> Create()
        {
            Queue<Car> newCars = new Queue<Car>();

            int carsCount = GetRandomCount();

            for (int i = 0; i < carsCount; i++)
                newCars.Enqueue(new Car(GetRandomTitle(), GetRandomDetails()));

            return newCars;
        }

        private int GetRandomCount()
        {
            int minValue = 2;
            int maxValue = 10;

            return Randomizer.GenerateRandomValue(minValue, maxValue + 1);
        }

        private List<Detail> GetRandomDetails()
        {
            DetailFactory detailFactory = new DetailFactory();
            List<Detail> parts = detailFactory.GetDetails();

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

            List<Detail> newParts = new List<Detail>(parts);

            return newParts;
        }

        private string GetRandomTitle()
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
        public int DetailsCount => _details.Count;

        public void ShowInfo()
        {
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
            return _details;
        }
    }

    class StorageFactory
    {
        public Storage Create(int storageCellQuantity)
        {
            return new Storage(CreateStorageCells(storageCellQuantity));
        }

        private List<StorageCell> CreateStorageCells(int storageCellQuantity)
        {
            DetailFactory detailFactory = new DetailFactory();
            List<StorageCell> storageCells = new List<StorageCell>();

            List<Detail> details = detailFactory.GetDetails();

            for (int i = 0; i < details.Count; i++)
                storageCells.Add(new StorageCell(details[i], storageCellQuantity, details[i].Price));

            return storageCells;
        }
    }

    class Storage
    {
        private List<StorageCell> _storageCells;

        public Storage(List<StorageCell> storageCells)
        {
            _storageCells = storageCells;
        }

        public void ShowInfo()
        {
            foreach (var storageCell in _storageCells)
            {
                Console.WriteLine("Детали на складе:");

                StringDelimiter.DrawLine();
                storageCell.ShowInfo();
            }
        }

        public bool TryTakeDetail(string name, out Detail detail)
        {
            foreach (var storageCell in _storageCells)
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
