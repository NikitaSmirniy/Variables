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

                _carService. Show();

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

                _carService.TakeOutClient();

                Console.ReadKey();
            }
        }
    }

    class CarService
    {
        private Queue<Car> _brokenCars = new Queue<Car>();
        private Storage _storage;

        private int _costFixedPenalty = 1000;

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

        public void Show()
        {
            foreach (var storageCell in _storage.GetCells())
            {
                storageCell.ShowInfo();
            }
        }

        private void PayPenalty(int penalty) =>
            Money -= penalty;

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

            Car brokenCar = _brokenCars.Peek();

            Console.WriteLine($"Если хотите начать ремонт машины введите {CommandRepair}");
            Console.WriteLine($"Если не хотите начинать ремонт машины введите {CommandCancell}");

            Console.Write("Введите команду: ");

            string userInput = Console.ReadLine();

            if (userInput == CommandRepair)
            {
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandRepair:
                        ReplaceDetail(brokenCar.GetDetails());
                        break;

                    case CommandCancell:
                        PayPenalty(GetBrokenDetailsSum(brokenCar.GetDetails()));
                        break;

                    default:
                        Console.WriteLine("Неверная команда повторите ввод!!!");
                        break;
                }

                Console.ReadKey();
            }
            else
            {
                PayPenalty(_costFixedPenalty);

                Console.WriteLine($"Вы отказались ремонтировать машину и платите фексированный штраф в размере: {_costFixedPenalty}");
            }

            brokenCar.ShowInfo();

            Console.WriteLine("Машина уехала");
        }

        public void TakeOutClient()
        {
            if (BrokenCarsCount > 0)
                _brokenCars.Dequeue();
        }

        private void ReplaceDetail(List<Detail> details)
        {
            if (TryGetBrokenDetail(details, out Detail brokenDetail))
            {
                foreach (var storageCell in _storage.GetCells())
                {
                    if (storageCell.Quantity > 0 && storageCell.Detail == brokenDetail)
                    {
                        brokenDetail = storageCell.Detail.Clone();

                        storageCell.IssuePart();

                        return;
                    }
                    else if(storageCell.Quantity == 0 && storageCell.Detail != brokenDetail)
                    {
                        Console.WriteLine("На складе закончились такие детали");
                    }
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

        private int GetBrokenDetailsSum(List<Detail> details)
        {
            int sum = 0;

            foreach (var detail in details)
                sum += detail.CostPrice;

            return sum;
        }
    }

    class DetailFactory
    {
        public List<Detail> GetDetails()
        {
            List<Detail> deatails = new List<Detail>()
            {
            new Fan(),
            new Windscreen(),
            new Headlight(),
            new Bumper(),
            new Accumulator(),
            new Engine(),
            new Filter(),
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

        public void ShowInfo()
        {
            Console.WriteLine(Name + "\n");

            foreach (var detail in _details)
                detail.ShowInfo();
        }

        public List<Detail> GetDetails()
        {
            return _details.ToList();
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
                storageCells.Add(new StorageCell(details[i], storageCellQuantity, details[i].CostPrice));

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

        public List<StorageCell> GetCells()
        {
            return _storageCells;
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

        public void IssuePart()
        {
            if (Quantity > 0)
                Quantity--;
        }
    }

    abstract class Detail
    {
        public Detail()
        {
            Name = GetType().Name;
        }

        public string Name { get; private set; }
        public bool IsBroken { get; private set; }
        public int CostPrice { get; protected set; }

        public void ShowInfo()
        {
            string status = GetStatus();

            Console.WriteLine($"{Name} состояние: {status}");
        }

        public abstract Detail Clone();

        private string GetStatus()
        {
            string bedState = "Плохое";
            string goodState = "Хорошое";

            if (IsBroken)
                return bedState;

            return goodState;
        }

        public void Break()
        {
            IsBroken = true;
        }
    }

    class Fan : Detail
    {
        public Fan()
        {
            CostPrice = 3000;
        }

        public override Detail Clone()
        {
            return new Fan();
        }
    }

    class Windscreen : Detail
    {
        public Windscreen()
        {
            CostPrice = 30000;
        }

        public override Detail Clone()
        {
            return new Windscreen();
        }
    }

    class Headlight : Detail
    {
        public Headlight()
        {
            CostPrice = 8000;
        }

        public override Detail Clone()
        {
            return new Headlight();
        }
    }

    class Bumper : Detail
    {
        public Bumper()
        {
            CostPrice = 17000;
        }

        public override Detail Clone()
        {
            return new Bumper();
        }
    }

    class Accumulator : Detail
    {
        public Accumulator()
        {
            CostPrice = 23000;
        }

        public override Detail Clone()
        {
            return new Accumulator();
        }
    }

    class Engine : Detail
    {
        public Engine()
        {
            CostPrice = 230000;
        }

        public override Detail Clone()
        {
            return new Engine();
        }
    }

    class Filter : Detail
    {
        public Filter()
        {
            CostPrice = 4000;
        }

        public override Detail Clone()
        {
            return new Filter();
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
}
