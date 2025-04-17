using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandSell = "1";
            const string CommandShowMagazineMoney = "2";
            const string CommandShowAllClientItems = "3";
            const string CommandShowAllMerchantItems = "4";
            const string CommandExit = "5";

            bool isOpen = true;

            Magazine magazine = new Magazine();

            Client client = new Client(new Item[0]);

            Merchant merchant = new Merchant(new Item[] { new Item("Apple", 49), new Item("Gum", 99),
                new Item("Milk", 129), new Item("Toilet paper", 299), new Item("Knife", 599) });

            while (isOpen)
            {
                Console.WriteLine($"Команда продать - {CommandSell}");
                Console.WriteLine($"Команда показа ваших средств - {CommandShowMagazineMoney}");
                Console.WriteLine($"Команда показать все товары и средства покупателя - {CommandShowAllClientItems}");
                Console.WriteLine($"Команда показать все товары торговца - {CommandShowAllMerchantItems}");
                Console.WriteLine($"Команда выйти - {CommandExit}");

                Console.Write("Введите команду: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandSell:
                        merchant.SellItem(client, magazine);
                        break;

                    case CommandShowMagazineMoney:
                        magazine.ShowMoney();
                        break;

                    case CommandShowAllClientItems:
                        client.ShowAllItems();
                        break;

                    case CommandShowAllMerchantItems:
                        merchant.ShowAllItems();
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
    }

    class Magazine
    {
        public int Money { get; private set; }

        public void TakeMoney(int sumMoney)
        {
            Money += sumMoney;
        }

        public void ShowMoney()
        {
            Console.WriteLine($"Ваши деньги: {Money}");
        }
    }

    class Person
    {
        protected List<Item> _items;

        public Person(Item[] items)
        {
            _items = new List<Item>(items);
        }

        public virtual void ShowAllItems()
        {
            Console.WriteLine("Продукты");

            for (int i = 0; i < _items.Count; i++)
            {
                DrawLine();
                Console.WriteLine($"{i + 1}. {_items[i].Name} цена: {_items[i].Price}");
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

        public void SellItem(Client client, Magazine magazine)
        {
            Console.WriteLine("Продукт под каким номером из списка вы хотите продать");

            ShowAllItems();

            int.TryParse(Console.ReadLine(), out int userInput);

            if (TryGetItem(out Item item, userInput))
            {
                if (client.Money >= item.Price)
                {
                    client.PayMoney(item.Price);
                    client.AddItem(item);

                    _items.RemoveAt(userInput - 1);
                    magazine.TakeMoney(item.Price);

                    Console.WriteLine("Покупатель купил у вас продукт");
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

        private bool TryGetItem(out Item item, int index)
        {
            if (index > 0 && index <= _items.Count)
            {
                item = _items[index - 1];
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

        public int Money { get; private set; }

        public void PayMoney(int price)
        {
            Money -= price;
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public override void ShowAllItems()
        {
            Console.WriteLine($"Средства покупателя: {Money}");

            base.ShowAllItems();
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
