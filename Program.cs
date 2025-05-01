using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            PlatoonCreater platoonCreater = new PlatoonCreater();

            BattleField warController = new BattleField(platoonCreater);

            warController.Work();
        }
    }

    class BattleField
    {
        private PlatoonCreater _platoonCreater;

        public BattleField(PlatoonCreater platoonCreater)
        {
            _platoonCreater = platoonCreater;
        }

        public void Work()
        {
            bool isOpen = true;
            int capacityPlatoon = 60;

            Platoon platoon1 = new Platoon(_platoonCreater.Create(capacityPlatoon), "Первая команда");
            Platoon platoon2 = new Platoon(_platoonCreater.Create(capacityPlatoon), "Вторая команда");

            while (isOpen)
            {
                Attack(platoon1, platoon2);
                platoon2.RemoveDeadSoldiers();
                platoon2.ShowSoldersCondition();

                Attack(platoon2, platoon1);
                platoon1.RemoveDeadSoldiers();
                platoon1.ShowSoldersCondition();

                if (TryGetDeadPlatoon(platoon1, platoon2, out Platoon deadPlatoon))
                {
                    isOpen = false;

                    ShowWinner(deadPlatoon);
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowWinner(Platoon winnerPlatoon)
        {
            Console.WriteLine($"Побеждает взвод под названием {winnerPlatoon.Name}");
            Console.WriteLine("В его отряде остались: ");

            winnerPlatoon.ShowSoldersCondition();
        }

        private void Attack(Platoon striker, Platoon defender)
        {
            striker.Attack(defender.GetSoldiers());

            Console.ReadKey();
            Console.Clear();
        }

        private bool TryGetDeadPlatoon(Platoon platoon1, Platoon platoon2, out Platoon deadPlatoon)
        {
            if (platoon1.SoldiersCount <= 0)
            {
                deadPlatoon = platoon1;
                return true;
            }
            else if (platoon2.SoldiersCount <= 0)
            {
                deadPlatoon = platoon2;
                return true;
            }

            deadPlatoon = null;
            return false;
        }
    }

    class PlatoonCreater
    {
        private List<Soldier> _soldiers = new List<Soldier>() { new CommonSoldier(100, 15, 25), new HeavySoldier(100, 15, 20),
            new Multipurpose(100, 15, 10), new Viking(100, 15, 10) };

        public List<Soldier> Create(int count)
        {
            List<Soldier> soldiers = new List<Soldier>();

            for (int i = 0; i < count; i++)
                soldiers.Add(_soldiers[Randomizer.GenerateRandomValue(0, _soldiers.Count)]);

            return soldiers;
        }
    }

    class Platoon
    {
        private List<Soldier> _soldiers = new List<Soldier>();

        public Platoon(List<Soldier> soldiers, string name)
        {
            Name = name;
            _soldiers = soldiers;
        }

        public string Name { get; private set; }
        public int SoldiersCount => _soldiers.Count;

        public void Attack(List<Soldier> enemies)
        {
            if (SoldiersCount > 0)
            {
                foreach (var soldier in _soldiers)
                    soldier.Attack(enemies);
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

        public void RemoveDeadSoldiers()
        {
            for (int i = _soldiers.Count - 1; i >= 0; i--)
            {
                if (_soldiers[i].IsAlive == false)
                    _soldiers.Remove(_soldiers[i]);
            }
        }

        public List<Soldier> GetSoldiers()
        {
            return new List<Soldier>(_soldiers);
        }
    }

    abstract class Soldier
    {
        protected int MaxHealth;
        protected int Armor;
        protected int Damage;

        public Soldier(int maxHealth, int armor, int damage)
        {
            MaxHealth = maxHealth;
            Armor = armor;
            Damage = damage;
            Health = MaxHealth;
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
        public void ShowCurrentHealth() => Console.WriteLine($"Здоровье: {Health}/{MaxHealth}");

        public abstract Soldier Clone();

        protected Soldier GetRandomEnemy(List<Soldier> enemys)
        {
            return enemys[Randomizer.GenerateRandomValue(0, enemys.Count)];
        }
    }

    class CommonSoldier : Soldier
    {
        public CommonSoldier(int maxHealth, int armor, int damage) : base(maxHealth, armor, damage) { }

        public override void Attack(List<Soldier> enemys)
        {
            var target = GetRandomEnemy(enemys);

            target.TakeDamage(Damage);
        }

        public override Soldier Clone()
        {
            return new CommonSoldier(MaxHealth, Armor, Damage);
        }
    }

    class HeavySoldier : Soldier
    {
        private int _damageModifier = 2;

        public HeavySoldier(int maxHealth, int armor, int damage) : base(maxHealth, armor, damage) { }

        public override void Attack(List<Soldier> enemys)
        {
            var target = GetRandomEnemy(enemys);

            target.TakeDamage(Damage * _damageModifier);
        }

        public override Soldier Clone()
        {
            return new HeavySoldier(MaxHealth, Armor, Damage);
        }
    }

    class Multipurpose : Soldier
    {
        private int _attackCount = 3;

        public Multipurpose(int maxHealth, int armor, int damage) : base(maxHealth, armor, damage) { }

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
            return new Multipurpose(MaxHealth, Armor, Damage);
        }
    }

    class Viking : Soldier
    {
        private int _attackCount = 3;

        public Viking(int maxHealth, int armor, int damage) : base(maxHealth, armor, damage) { }

        public override void Attack(List<Soldier> enemys)
        {
            for (int i = 0; i < _attackCount; i++)
                GetRandomEnemy(enemys).TakeDamage(Damage);
        }

        public override Soldier Clone()
        {
            return new Viking(MaxHealth, Armor, Damage);
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
