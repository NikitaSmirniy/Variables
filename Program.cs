using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Supermarket supermark = new Supermarket(10);

            supermark.Work();
        }
    }

    class Supermarket
    {
        private StringDelimiter stringDelimiter = new StringDelimiter(30);
        private List<Item> _items = new List<Item>();
        private Queue<Client> _clients = new Queue<Client>();
        private int _clientsCount;
        private int _money;

        public Supermarket(int clientsCount)
        {
            _clientsCount = clientsCount;

            AddItems();

            AddClients();
        }

        public void Work()
        {
            ShowItems();

            stringDelimiter.DrawLine();

            ServeClients();

            stringDelimiter.DrawLine();

            Console.WriteLine($"Работа завершена\n за сегодня мы заработали: {_money} долларс");
            Console.ReadKey();
        }

        private void AddClients()
        {
            Random random = new Random();
            int minRandomMoney = 1000;
            int maxRandomMoney = 2500;

            for (int i = 0; i < _clientsCount; i++)
                _clients.Enqueue(new Client(FillClientCart(), random.Next(minRandomMoney, maxRandomMoney)));
        }

        private void AddItems()
        {
            _items.Add(new Item("Яблоко 'Забродившее'", 50));
            _items.Add(new Item("Водка 'Тундра'", 300));
            _items.Add(new Item("Чупс со вкусом пива", 10));
            _items.Add(new Item("Чипсы 'Пивные'", 400));
            _items.Add(new Item("Вино", 500));
            _items.Add(new Item("Пиво 'Гиннесс'", 200));
        }

        private void ServeClients()
        {
            while (_clients.Count > 0)
            {
                _money += _clients.Dequeue().Buy();

                stringDelimiter.DrawLine();

                Console.WriteLine("Клиент обслужен");
                Console.ReadKey();

                stringDelimiter.DrawLine();
            }
        }

        private List<Item> FillClientCart()
        {
            List<Item> items = new List<Item>();

            int minItemCount = 1;
            int maxItemCount = 10;
            int randomItemsCount = Randomizer.GenerateRandomValue(minItemCount, maxItemCount);

            for (int i = 0; i < randomItemsCount; i++)
                items.Add(_items[Randomizer.GenerateRandomValue(0, _items.Count - 1)].Clone());

            return items;
        }

        private void ShowItems()
        {
            Console.WriteLine("Ассортимент товаров");

            foreach (var item in _items)
                item.ShowInfo();
        }
    }

    class Client
    {
        private List<Item> _cartItems = new List<Item>();
        private List<Item> _bagItems;

        public Client(List<Item> products, int money)
        {
            _cartItems = products;
            Money = money;
        }

        public int Money { get; private set; }

        public int Buy()
        {
            while (_cartItems.Count > 0)
            {
                if (Money >= GetSumBuy(out int sumBuy))
                {
                    Console.WriteLine("Я купил продукты");
                    Console.ReadKey();

                    _bagItems = new List<Item>(_cartItems);
                    _cartItems.Clear();
                    ShowBag();

                    return sumBuy;
                }
                else
                {
                    Item dropItem = GetRandomItemFromCart();

                    Console.WriteLine("Мне не хватает денег для покупки и мне пришлось убрать один продукт из корзины ");
                    dropItem.ShowInfo();


                    _cartItems.Remove(dropItem);

                    Console.ReadKey();
                }
            }

            Console.WriteLine("Я не смог ничего купить");
            Console.ReadKey();

            return 0;
        }

        public int GetSumBuy(out int sumBuy)
        {
            sumBuy = 0;

            foreach (var item in _cartItems)
                sumBuy += item.Price;

            return sumBuy;
        }

        private Item GetRandomItemFromCart()
        {
            return _cartItems[Randomizer.GenerateRandomValue(0, _cartItems.Count - 1)];
        }

        private void ShowBag()
        {
            Console.WriteLine("**Вот список мои купленных продуктов**");

            foreach (var bagItem in _bagItems)
                bagItem.ShowInfo();
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

        public void ShowInfo()
        {
            Console.WriteLine($"{Name} цена: {Price}");
        }

        public Item Clone()
        {
            return new Item(Name, Price);
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
            Console.WriteLine(new string('-', _lineRange));
        }
    }

    static class Randomizer
    {
        private static Random _random = new Random();

        public static int GenerateRandomValue(int minRandomValue, int maxRandomValue)
        {
            return _random.Next(minRandomValue, maxRandomValue + 1);
        }
    }
}
