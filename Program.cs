using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Arena arena = new Arena();

            arena.Work();
        }
    }

    class Arena
    {
        private const string CommandShowFight = "1";
        private const string CommandExit = "2";

        private StringDelimiter _stringDelimiter = new StringDelimiter(30);
        private Randomizer _randomizer = new Randomizer();

        private List<Warrior> _warriors = new List<Warrior>()
        {
            new Necromancer(125, 12, 75,
            "Это колдун из древнего племени 'индейцев' способен вызывать мертвых которые служат своему хозяину."),
            new Viking(200, 15, 50, "Этот викинг пришёл на арену, что-бы выплеснуть свою ярость."),
            new ManTree(400, 0, 30, "Это существо когда-то было человеком, но попав под влияние собственной магии потеряло человеческий облик и обрёл быструю регенирацию."),
            new Magician(100, 6, 80, "Это маг из древнего племени 'индейцев' способен выпускать огненные шары.", 100),
            new Archer(175, 25, 65, "Этот лучник имеет большой опыт в боях, на арену он пришёл исключительно за наживой.")
        };

        public void Work()
        {
            bool isOpen = true;

            Console.WriteLine("Приветствую вас на арене гладиаторских боёв");

            while (isOpen)
            {
                Console.WriteLine($"Команда - {CommandShowFight} посмотреть бой");
                Console.WriteLine($"Команда - {CommandExit} выйти");

                Console.Write("Выберете комнаду: ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandShowFight:
                        ShowFight();
                        break;

                    case CommandExit:
                        isOpen = false;
                        break;

                    default:
                        Console.WriteLine("Вы ввели неверную команду");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowFight()
        {
            ShowWarriors();

            if (ChuseWarrior(out Warrior warrior1) == false || ChuseWarrior(out Warrior warrior2) == false)
                return;

            Console.Clear();

            while (warrior1.Health > 0 && warrior2.Health > 0)
            {
                if (_randomizer.GenerateRndomValue() > 50)


                    warrior1.Attack(warrior2);
                warrior2.ShowCurrentHealth();

                _stringDelimiter.DrawLine();

                warrior2.Attack(warrior1);
                warrior1.ShowCurrentHealth();

                Console.ReadKey();
                Console.Clear();
            }

            ShowBattleOutcome(warrior1, warrior2);
        }

        private bool ChuseWarrior(out Warrior warrior)
        {
            Console.Write("Выберите гладиатора: ");

            if (int.TryParse(Console.ReadLine(), out int userInput))
            {
                if (userInput - 1 >= 0 && userInput - 1 < _warriors.Count)
                {
                    Warrior newWarrior = _warriors[userInput - 1];
                    warrior = newWarrior.Clone();

                    Console.WriteLine($"Вы выбрали гладиатора под номером - {userInput}");
                    return true;
                }
                else
                {
                    Console.WriteLine("Такого бойца нет в списке!");
                }
            }
            else
            {
                Console.WriteLine("Вы ввели неверные символы!");
            }

            Console.ReadKey();
            warrior = null;
            return false;
        }

        private void ShowWarriors()
        {
            Console.WriteLine("**Список гладиаторов**");

            for (int i = 0; i < _warriors.Count; i++)
            {
                _stringDelimiter.DrawLine();

                Console.Write($"Гладиатор №{i + 1}. ");
                _warriors[i].ShowStats();
            }
        }

        private void ShowBattleOutcome(Warrior warrior1, Warrior warrior2)
        {
            if (warrior1.Health <= 0 && warrior2.Health <= 0)
                Console.WriteLine("В этом бою победила ничья");
            else if (warrior1.Health <= 0)
                Console.WriteLine("В этом бою победил второй гладиатор");
            else if (warrior2.Health <= 0)
                Console.WriteLine("В этом бою победил первый гладиатор");
        }
    }

    enum WarriorType { Magician, Archer, Viking, Necromancer, ManTree }

    abstract class Warrior
    {
        protected int _maxHealth;
        protected int _armor;
        protected int _damage;
        protected string _description;

        public Warrior(int maxHealth, int armor, int damage, string description, WarriorType warriorType)
        {
            _maxHealth = GetValue(maxHealth);
            _armor = GetValue(armor);
            _damage = GetValue(damage);

            Health = _maxHealth;
            _description = description;
            _warriorType = warriorType;
        }

        public int Health { get; protected set; }

        public WarriorType _warriorType { get; private set; }

        public virtual void TakeDamage(int damage)
        {
            if (damage > 0)
                Health -= damage - _armor;
            else
                Health--;
        }

        public virtual void Attack(Warrior warrior)
        {
            if (Health > 0)
                warrior.TakeDamage(_damage);
        }

        public void ShowStats()
        {
            Console.WriteLine($"\nХарактеристики\nТип: {_warriorType}\nЗдоровье: {Health}\nБроня: {_armor}\nУрон: {_damage}");
            Console.WriteLine($"Описание: {_description}");
        }

        public void ShowCurrentHealth()
        {
            Console.WriteLine($"{_warriorType } Здоровье: {Health}/{_maxHealth}");
        }

        public abstract Warrior Clone();

        private int GetValue(int value)
        {
            if (value > 0)
                return value;

            return 1;
        }
    }

    class Archer : Warrior
    {
        private int _dodgeChance = 50;

        private Randomizer _randomizer = new Randomizer();

        public Archer(int maxHealth, int armor, int damage, string description) :
            base(maxHealth, armor, damage, description, WarriorType.Archer)
        { }

        public override void TakeDamage(int damage)
        {
            if (_randomizer.GenerateRndomValue() >= _dodgeChance)
                base.TakeDamage(damage);
            else
                Console.WriteLine($"{_warriorType} задоджил атаку");
        }

        public override Warrior Clone()
        {
            return new Archer(Health, _armor, _damage, _description);
        }
    }

    class Necromancer : Warrior
    {
        private int _doubleDamageChance = 50;
        private int _damageModify = 2;

        private Randomizer _randomizer = new Randomizer();

        public Necromancer(int maxHealth, int armor, int damage, string description) :
            base(maxHealth, armor, damage, description, WarriorType.Necromancer)
        { }

        public override void Attack(Warrior warrior)
        {
            if (_randomizer.GenerateRndomValue() > _doubleDamageChance)
            {
                warrior.TakeDamage(_damage * _damageModify);
                Console.WriteLine($"{_warriorType} Наносит крит. урон");
            }
            else
            {
                warrior.TakeDamage(_damage);
            }
        }

        public override Warrior Clone()
        {
            return new Necromancer(Health, _armor, _damage, _description);
        }
    }

    class Magician : Warrior
    {
        private int _mana;
        private int _maxMana;
        private int _abilutyPrice = 25;
        private int _damageModify = 2;

        public Magician(int maxHealth, int armor, int damage, string description, int maxMana) :
            base(maxHealth, armor, damage, description, WarriorType.Magician)
        {
            _maxMana = maxMana;
            _mana = _maxMana;
        }

        public override void Attack(Warrior warrior)
        {
            if (_mana > 0)
            {
                warrior.TakeDamage(_damage * _damageModify);

                Console.WriteLine($"{_warriorType} Нанёс урон огненным шаром");

                _mana -= _abilutyPrice;
            }
            else
            {
                warrior.TakeDamage(_damage);
            }
        }

        public override Warrior Clone()
        {
            return new Magician(Health, _armor, _damage, _description, _maxMana);
        }
    }

    class Viking : Warrior
    {
        private int _rage;
        private int _limitRage = 2;
        private int _rageAttackCount = 2;

        public Viking(int maxHealth, int armor, int damage, string description) :
            base(maxHealth, armor, damage, description, WarriorType.Viking)
        { }

        public override void Attack(Warrior warrior)
        {
            if (_rage >= _limitRage)
            {
                for (int i = 0; i < _rageAttackCount; i++)
                {
                    warrior.TakeDamage(_damage);
                }

                _rage = 0;
                Console.WriteLine($"{_warriorType} Впал в ярость и нанес сразу несколько ударов по сопернику");
            }
            else
            {
                warrior.TakeDamage(_damage);
            }
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            _rage++;
        }

        public override Warrior Clone()
        {
            return new Viking(Health, _armor, _damage, _description);
        }
    }

    class ManTree : Warrior
    {
        private int _rage;
        private int _limitRage = 3;
        private int _healModify = 2;

        public ManTree(int maxHealth, int armor, int damage, string description) :
            base(maxHealth, armor, damage, description, WarriorType.ManTree)
        { }

        private int _healRepair => _maxHealth / _healModify;

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            _rage++;

            CureHealth();
        }

        public override Warrior Clone()
        {
            return new ManTree(Health, _armor, _damage, _description);
        }

        private void CureHealth()
        {
            if (_rage >= _limitRage && Health < _maxHealth)
            {
                int necessaryHealth = _maxHealth - Health;

                if (_healRepair <= necessaryHealth)
                    Health += _healRepair;
                else
                    Health += necessaryHealth;

                _rage = 0;
                Console.WriteLine($"{_warriorType} накопил ярость и вылечил себе часть здоровья");
            }
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
            Console.WriteLine(new string('_', _lineRange));
        }
    }

    class Randomizer
    {
        private Random _random = new Random();
        private int _maxRandomValue = 100;

        public int GenerateRndomValue()
        {
            return _random.Next(0, _maxRandomValue + 1);
        }
    }
}
