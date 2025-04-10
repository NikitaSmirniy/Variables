using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("Agent-47", 28.0f, 50.0f, 120);

            player.ShowCharacterParameters();

            Console.ReadKey();
        }
    }

    class Player
    {
        private string _name;
        private float _speed;
        private float _damage;
        private float _maxHealth;
        private float _health;

        public Player(string name, float speed, float damage, float health = 100)
        {
            _name = name;
            _speed = speed;
            _damage = damage;
            _maxHealth = health;
            _health = _maxHealth;
        }

        public void ShowCharacterParameters()
        {
            Console.WriteLine($"Имя: {_name}");
            Console.WriteLine($"Cкорость перемещения: {_speed} км/ч");
            Console.WriteLine($"Урон: {_damage} км/ч");
            Console.WriteLine($"Макс. здоровье: {_maxHealth}");
        }
    }
}
