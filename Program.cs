using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();

            shop.StartWork();
        }
    }

    class Shop
    {
        private const string CommandSell = "1";
        private const string CommandShowMerchantMoney = "2";
        private const string CommandShowAllMerchantItems = "3";
        private const string CommandShowAllClientItems = "4";
        private const string CommandExit = "5";

        private Client _client = new Client(new Item[0]);

        private Merchant _merchant = new Merchant(new Item[] { new Item("Apple", 49), new Item("Gum", 99),
                new Item("Milk", 129), new Item("Toilet paper", 299), new Item("Knife", 599) });

        public void StartWork()
        {
            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine($"Команда продать - {CommandSell}");
                Console.WriteLine($"Команда показа средств торговца - {CommandShowMerchantMoney}");
                Console.WriteLine($"Команда показать все товары торговца - {CommandShowAllMerchantItems}");
                Console.WriteLine($"Команда показать все товары и средства покупателя - {CommandShowAllClientItems}");
                Console.WriteLine($"Команда выйти - {CommandExit}");

                Console.Write("Введите команду: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandSell:
                        SellItem();
                        break;

                    case CommandShowMerchantMoney:
                        _merchant.ShowMoney();
                        break;

                    case CommandShowAllClientItems:
                        _client.ShowAllInfo();
                        break;

                    case CommandShowAllMerchantItems:
                        _merchant.ShowAllInfo();
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

        private void SellItem()
        {
            Console.WriteLine("Продукт под каким номером из списка вы хотите продать");

            _merchant.ShowAllInfo();

            int.TryParse(Console.ReadLine(), out int userInput);

            if (_merchant.TryGetItem(out Item item, userInput))
            {
                int itemPrice = item.Price;

                if (_client.TryPayMoney(itemPrice))
                {
                    _client.AddItem(item);

                    _merchant.TakeMoney(itemPrice);
                    _merchant.DeleteItemByIndex(userInput - 1);

                    Console.WriteLine("Покупатель купил у вас продукт");

                    _merchant.ShowMoney();
                }
                else
                {
                    Console.WriteLine("У покупателя недостаточно средств");
                }
            }
            else
            {
                Console.WriteLine("Вы ввели неверную команду");
            }
        }
    }

    class Person
    {
        protected List<Item> Items;

        public Person(Item[] items)
        {
            Items = new List<Item>(items);
        }

        public int Money { get; protected set; }
        public int ItemsCount => Items.Count;

        public virtual void ShowAllInfo()
        {
            Console.WriteLine("Продукты");

            for (int i = 0; i < ItemsCount; i++)
            {
                DrawLine();
                Console.WriteLine($"{i + 1}. {Items[i].Name} цена: {Items[i].Price}");
            }
        }

        private void DrawLine()
        {
            Console.WriteLine(new string('_', 20));
        }
    }

    class Merchant : Person
    {
        public Merchant(Item[] items) : base(items) { }

        public void DeleteItemByIndex(int index)
        {
            Items.RemoveAt(index);
        }

        public void TakeMoney(int sumMoney)
        {
            if (sumMoney > 0)
                Money += sumMoney;
        }

        public void ShowMoney()
        {
            Console.WriteLine($"Деньги торговца: {Money}");
        }

        public bool TryGetItem(out Item item, int index)
        {
            if (index > 0 && index <= ItemsCount)
            {
                item = Items[index - 1];
                return true;
            }
            else
            {
                item = null;
                return false;
            }
        }
    }

    class Client : Person
    {
        public Client(Item[] items, int money = 1250) : base(items)
        {
            Money = money;
        }

        public bool TryPayMoney(int itemPrice)
        {
            if (Money >= itemPrice)
            {
                Money -= itemPrice;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public override void ShowAllInfo()
        {
            Console.WriteLine($"Средства покупателя: {Money}");

            base.ShowAllInfo();
        }
    }

    class Item
    {
        public Item(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public int Price { get; private set; }
    }
}
