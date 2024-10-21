using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string DamageAttack = "1";
            const string FireBallReload = "2";
            const string FireBallActive = "3";
            const string Healing = "4";

            //Player Health & Mana & Damage Characteristics
            int playerMaxHealth = 100;
            int playerHealth = playerMaxHealth;
            int playerMaxMana = 100;
            int playerMana = playerMaxMana;
            int fireBallCost = 50;
            int healingAmount = 3;

            int playerDamage;
            int playerMinDamage = 50;
            int playerMaxDamage = 125;
            int fireBallDamage = 300;
            bool isHaveFireBall = false;

            //Enemy Health & Damage Characteristics
            int enemyMaxHealth = 1000;
            int enemyHealth = enemyMaxHealth;
            int enemyDamage;
            int enemyMinDamage = 20;
            int enemyMaxDamage = 40;

            //Others
            Random random = new Random();

            while (playerHealth > 0 && enemyHealth > 0)
            {
                Console.Clear();

                Console.WriteLine($"Здоровье героя {playerHealth} / {playerMaxHealth}\nМана героя {playerMana} / {playerMaxMana}\n");

                Console.WriteLine("Ваш ход, выберите способность:");

                Console.WriteLine($"Способность {DamageAttack} - наносит урон врагу от {playerMinDamage} до {playerMaxDamage}");

                if (isHaveFireBall == false)
                {
                    Console.WriteLine($"Способность {FireBallReload} - создает огненный шар");
                }
                else
                {
                    Console.WriteLine($"Способность {FireBallReload} - Вы уже создали огненный шар, осталось только взорвать его способностью - {FireBallActive}");
                }

                if (isHaveFireBall)
                {
                    Console.WriteLine($"Способность {FireBallActive} - взрывает огненный шар и наносит {fireBallDamage} урона врагу");
                }
                else
                {
                    Console.WriteLine($"Способность {FireBallActive} - взрывает огненный шар(!сначала воспользуйтесь способностью {FireBallReload}) и наносит {fireBallDamage} урона врагу");
                }
                
                Console.WriteLine($"Способность {Healing} - полность восстанавливает здоровье и ману герою(у вас осталось {healingAmount} применений)");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case DamageAttack:
                        playerDamage = random.Next(playerMinDamage, playerMaxDamage + 1);
                        Console.WriteLine($"Вы нанесли - {playerDamage} урона\nЗдоровье врага: {enemyHealth -= playerDamage} / {enemyMaxHealth}");
                        break;

                    case FireBallReload:
                        if (playerMana >= fireBallCost)
                        {
                            isHaveFireBall = true;
                            Console.WriteLine($"Вы создали огненный шар и потратили ману!\nМана {playerMana -= fireBallCost} / {playerMaxMana}");
                        }
                        else
                        {
                            Console.WriteLine("У вас не хватает маны");
                        }
                        break;

                    case FireBallActive:
                        if (isHaveFireBall)
                        {
                            Console.WriteLine($"Вы взорвали огненный шар и нанесли - {fireBallDamage} урона\nЗдоровье врага: {enemyHealth -= fireBallDamage} / {enemyMaxHealth}");
                            isHaveFireBall = false;
                        }
                        else
                        {
                            Console.WriteLine("Сначала создайте огненный шар");
                        }
                        break;

                    case Healing:
                        if (healingAmount > 0)
                        {
                            healingAmount--;
                            Console.WriteLine($"Вы восстановили себе ману и здоровье, а так же потратили одно восстановление\nЗдоровье {playerHealth = playerMaxHealth} / {playerMaxHealth}\nМана { playerMana = playerMaxMana} / {playerMaxMana}");
                        }
                        else
                        {
                            Console.WriteLine("У вас закончились восстановления!");
                        }
                        break;

                    default:
                        Console.WriteLine("Вы промахнулись!");
                        break;
                }

                Console.ReadLine();

                Console.WriteLine("Теперь ход врага");

                enemyDamage = random.Next(enemyMinDamage, enemyMaxDamage + 1);
                Console.Write($"Враг использовал атаку наносящую урон от {enemyMinDamage} до {enemyMaxDamage}\nВраг нанес вам {enemyDamage} урона\nЗдоровье героя {playerHealth -= enemyDamage} / {playerMaxHealth}");

                Console.ReadLine();
            }

            if (playerHealth <= 0 && enemyHealth <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Ничья!!!");
            }
            else if (playerHealth <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Вы проиграли!!!");
            }
            else if (enemyHealth <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Вы одержали победу над боссом!!!");
            }

            Console.ReadLine();
        }
    }
}
