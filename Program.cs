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
        public string Name;
        public float Speed;
        public float Damage;
        public float MaxHealth;
        public float Health;

        public Player(string name, float speed, float damage, float health = 100)
        {
            Name = name;
            Speed = speed;
            Damage = damage;
            MaxHealth = health;
            Health = MaxHealth;
        }

        public void ShowCharacterParameters()
        {
            Console.WriteLine($"Имя: {Name}");
            Console.WriteLine($"Cкорость перемещения: {Speed} км/ч");
            Console.WriteLine($"Урон: {Damage} км/ч");
            Console.WriteLine($"Макс. здоровье: {MaxHealth}");
        }
    }
}
