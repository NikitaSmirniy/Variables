using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Good iPhone12 = new Good("IPhone 12");
            Good iPhone11 = new Good("IPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 1);

            warehouse.ShowGoods();

            Cart cart = shop.CreateCart();
            cart.Add(iPhone12, 4);
            cart.Add(iPhone11, 3);

            cart.ShowGoods();

            Console.WriteLine(cart.Order().Paylink);

            cart.Add(iPhone12, 9);

            Console.ReadLine();
        }
    }

    public struct Order
    {
        public Order(string paylink)
        {
            Paylink = paylink;
        }

        public string Paylink { get; private set; }
    }

    public class Cart
    {
        private readonly IWarehouse _warehouse;
        private readonly Dictionary<Good, int> _goods = new Dictionary<Good, int>();

        public Cart(IWarehouse warehouse)
        {
            _warehouse = warehouse;
        }

        public void Add(Good good, int count)
        {
            if (_warehouse.Contains(good, count))
            {
                _goods.Add(good, count);
                _warehouse.WriteOffGood(good, count);
            }
        }

        public void ShowGoods()
        {
            foreach (KeyValuePair<Good, int> good in _goods)
            {
                Console.WriteLine($"{good.Key.Name}: {good.Value}");
            }
        }

        public Order Order()
        {
            int paylink = 0;

            foreach (KeyValuePair<Good, int> good in _goods)
                paylink += good.Value;

            return new Order($"Paylink: {paylink}");
        }
    }

    public class Shop
    {
        private readonly Warehouse _warehouse;

        public Shop(Warehouse warehouse)
        {
            _warehouse = warehouse;
        }

        public Cart CreateCart()
        {
            return new Cart(_warehouse);
        }
    }

    public class Warehouse : IWarehouse
    {
        private Dictionary<Good, int> _goods = new Dictionary<Good, int>();

        public void Delive(Good good, int count)
        {
            if (good == null)
                throw new ArgumentNullException(nameof(good));

            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            _goods.Add(good, count);
        }

        public bool Contains(Good good, int count)
        {
            if (_goods.ContainsKey(good) == false)
                throw new ArgumentNullException(nameof(good));

            if (_goods.Count < count)
                throw new ArgumentOutOfRangeException(nameof(count));

            return true;
        }

        public void WriteOffGood(Good good, int count)
        {
            int goodCountInKey = _goods[good];

            if (goodCountInKey >= count)
                _goods[good] -= count;
            else
                _goods.Remove(good);
        }

        public void ShowGoods()
        {
            foreach (KeyValuePair<Good, int> good in _goods)
            {
                Console.WriteLine($"{good.Key.Name}: {good.Value}");
            }
        }
    }

    public interface IWarehouse
    {
        bool Contains(Good good, int count);
        void WriteOffGood(Good good, int count);
    }

    public class Good
    {
        public string Name { get; private set; }

        public Good(string name)
        {
            Name = name;
        }
    }
}
