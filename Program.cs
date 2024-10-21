using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandDamageAttack = "1";
            const string CommandFireBallReload = "2";
            const string CommandFireBallActive = "3";
            const string CommandHealing = "4";

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

            int enemyMaxHealth = 1000;
            int enemyHealth = enemyMaxHealth;
            int enemyDamage;
            int enemyMinDamage = 20;
            int enemyMaxDamage = 40;

            Random random = new Random();

            while (playerHealth > 0 && enemyHealth > 0)
            {
                Console.Clear();

                Console.WriteLine($"Здоровье героя {playerHealth} / {playerMaxHealth}\nМана героя {playerMana} / {playerMaxMana}\n");


                Console.WriteLine($"Здоровье Босса: {enemyHealth} / {enemyMaxHealth}\n");

                Console.WriteLine("Ваш ход, выберите способность:");

                Console.WriteLine($"Способность {CommandDamageAttack} - наносит урон врагу от {playerMinDamage} до {playerMaxDamage}");

                if (isHaveFireBall == false)
                {
                    Console.WriteLine($"Способность {CommandFireBallReload} - создает огненный шар");
                }
                else
                {
                    Console.WriteLine($"Способность {CommandFireBallReload} - Вы уже создали огненный шар, осталось только взорвать его способностью - {CommandFireBallActive}");
                }

                if (isHaveFireBall)
                {
                    Console.WriteLine($"Способность {CommandFireBallActive} - взрывает огненный шар и наносит {fireBallDamage} урона врагу");
                }
                else
                {
                    Console.WriteLine($"Способность {CommandFireBallActive} - взрывает огненный шар(!сначала воспользуйтесь способностью {CommandFireBallReload}) и наносит {fireBallDamage} урона врагу");
                }
                Console.WriteLine($"Способность {CommandHealing} - полность восстанавливает здоровье и ману герою(у вас осталось {healingAmount} применений)");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandDamageAttack:
                        playerDamage = random.Next(playerMinDamage, playerMaxDamage + 1);
                        enemyHealth -= playerDamage;
                        Console.WriteLine($"Вы нанесли - {playerDamage} урона\nЗдоровье врага: {enemyHealth} / {enemyMaxHealth}");
                        break;

                    case CommandFireBallReload:
                        if (playerMana >= fireBallCost)
                        {
                            isHaveFireBall = true;
                            playerMana -= fireBallCost;
                            Console.WriteLine($"Вы создали огненный шар и потратили ману!\nМана {playerMana} / {playerMaxMana}");
                        }
                        else
                        {
                            Console.WriteLine("У вас не хватает маны");
                        }
                        break;

                    case CommandFireBallActive:
                        if (isHaveFireBall)
                        {
                            isHaveFireBall = false;
                            enemyHealth -= fireBallDamage;
                            Console.WriteLine($"Вы взорвали огненный шар и нанесли - {fireBallDamage} урона\nЗдоровье врага: {enemyHealth} / {enemyMaxHealth}");
                        }
                        else
                        {
                            Console.WriteLine("Сначала создайте огненный шар");
                        }
                        break;

                    case CommandHealing:
                        if (healingAmount > 0)
                        {
                            healingAmount--;
                            playerHealth = playerMaxHealth;
                            playerMana = playerMaxMana;
                            Console.WriteLine($"Вы восстановили себе ману и здоровье, а так же потратили одно восстановление\nЗдоровье {playerHealth} / {playerMaxHealth}\nМана {playerMana} / {playerMaxMana}");
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
                playerHealth -= enemyDamage;
                Console.Write($"Враг использовал атаку наносящую урон от {enemyMinDamage} до {enemyMaxDamage}\nВраг нанес вам {enemyDamage} урона\nЗдоровье героя {playerHealth} / {playerMaxHealth}");

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
