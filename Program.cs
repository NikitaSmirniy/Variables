using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Platoon> platoons = new List<Platoon>() { new Platoon(60, "Орлы", new Multipurpose()), new Platoon(45, "Акулы", new HeavySoldier()) };

            WarController warController = new WarController(platoons);

            warController.Work();
        }
    }

    class WarController
    {
        private List<Platoon> _platoons = new List<Platoon>();

        public WarController(List<Platoon> platoons)
        {
            _platoons = platoons;
        }

        public void Work()
        {
            int firstPlatoon = 0;
            int secondPlatoon = 1;
            bool isOpen = true;

            _platoons[firstPlatoon].Fill();
            _platoons[secondPlatoon].Fill();

            while (isOpen)
            {
                Attack(firstPlatoon, secondPlatoon);
                Attack(secondPlatoon, firstPlatoon);

                if (TryGetWinPlatoon(out Platoon deadPlatoon))
                {
                    isOpen = false;

                    _platoons.Remove(deadPlatoon);

                    ShowWinner();
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowWinner()
        {
            int firstPlatoon = 0;
            Platoon winnerPlatoon = _platoons[firstPlatoon];

            Console.WriteLine($"Побеждает взвод под названием {winnerPlatoon.Name}");
            Console.WriteLine("В его отряде остались: ");

            winnerPlatoon.ShowSoldersCondition();
        }

        private void Attack(int striker, int defender)
        {
            _platoons[striker].Attack(_platoons[defender]);

            _platoons[defender].CalculateLosesse();

            _platoons[defender].ShowSoldersCondition();

            Console.ReadKey();
            Console.Clear(); 
        }

        private bool TryGetWinPlatoon(out Platoon deadPlatoon)
        {
            foreach (var platoon in _platoons)
            {
                if (platoon.SoldiersCount <= 0)
                {
                    deadPlatoon = platoon;
                    return true;
                }
            }

            deadPlatoon = null;
            return false;
        }
    }

    class Platoon
    {
        private List<Soldier> _soldiers = new List<Soldier>();
        private Soldier _typeOfTroops;
        private int Capacity;

        public Platoon(int capacity, string name, Soldier typeOfTroops)
        {
            Capacity = capacity;
            Name = name;
            _typeOfTroops = typeOfTroops;
        }

        public string Name { get; private set; }
        public int SoldiersCount => _soldiers.Count;

        public void Attack(Platoon enemy)
        {
            if (SoldiersCount > 0)
            {
                foreach (var soldier in _soldiers)
                    soldier.Attack(enemy._soldiers);
            }
        }

        public void ShowSoldersCondition()
        {
            Console.WriteLine("Выжившие члены взвода");

            for (int i = 0; i < _soldiers.Count; i++)
            {
                Console.Write($"{i + 1}.");
                _soldiers[i].ShowCurrentHealth();
            }
        }

        public void CalculateLosesse()
        {
            for (int i = _soldiers.Count - 1; i >= 0; i--)
            {
                if (_soldiers[i].IsAlive == false)
                    _soldiers.Remove(_soldiers[i]);
            }
        }

        public void Fill()
        {
            for (int i = 0; i < Capacity; i++)
                _soldiers.Add(_typeOfTroops.Clone());
        }
    }

    abstract class Soldier
    {
        protected int _maxHealth = 100;
        protected int Armor = 10;
        protected int Damage = 50;

        public Soldier()
        {
            Health = _maxHealth;
        }

        public int Health { get; private set; }
        public bool IsAlive => Health > 0;

        public virtual void TakeDamage(int damage)
        {
            if (Armor < damage)
                Health -= damage - Armor;
            else
                Health--;
        }

        public abstract void Attack(List<Soldier> enemys);
        public void ShowCurrentHealth() => Console.WriteLine($"Здоровье: {Health}/{_maxHealth}");

        public abstract Soldier Clone();

        protected Soldier GetRandomEnemy(List<Soldier> enemys)
        {
            return enemys[Randomizer.GenerateRandomValue(0, enemys.Count)];
        }
    }

    class CommonSoldier : Soldier
    {
        public override void Attack(List<Soldier> enemys)
        {
            var target = GetRandomEnemy(enemys);

            target.TakeDamage(Damage);
        }

        public override Soldier Clone()
        {
            return new CommonSoldier();
        }
    }

    class HeavySoldier : Soldier
    {
        private int _damageModifier = 2;

        public override void Attack(List<Soldier> enemys)
        {
                var target = GetRandomEnemy(enemys);

                target.TakeDamage(Damage * _damageModifier);
        }

        public override Soldier Clone()
        {
            return new HeavySoldier();
        }
    }

    class Multipurpose : Soldier
    {
        private int _attackCount = 3;

        public override void Attack(List<Soldier> enemys)
        {
            List<Soldier> enemysToAttack = new List<Soldier>(enemys);

            if (_attackCount > enemysToAttack.Count)
                _attackCount = enemysToAttack.Count;

            for (int i = 0; i < _attackCount; i++)
            {
                var enemy = GetRandomEnemy(enemysToAttack);

                enemy.TakeDamage(Damage);

                enemysToAttack.Remove(enemy);
            }
        }

        public override Soldier Clone()
        {
            return new Multipurpose();
        }
    }

    class Viking : Soldier
    {
        private int _attackCount = 3;

        public override void Attack(List<Soldier> enemys)
        {
            for (int i = 0; i < _attackCount; i++)
                GetRandomEnemy(enemys).TakeDamage(Damage);
        }

        public override Soldier Clone()
        {
            return new Viking();
        }
    }

    static class Randomizer
    {
        private static Random _random = new Random();

        public static int GenerateRandomValue(int minRandomValue, int maxRandomValue)
        {
            return _random.Next(minRandomValue, maxRandomValue);
        }
    }
}
