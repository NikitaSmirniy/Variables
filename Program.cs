using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            static void Main(string[] args)
        {
            const string CommandRubToUsd = "1";
            const string CommandRubToEur = "2";
            const string CommandUsdToRub = "3";
            const string CommandUsdToEur = "4";
            const string CommandEurToRub = "5";
            const string CommandEurToUsd = "6";
            const string CommandExit = "0";

            const string CurrencyUsd = "Usd";
            const string CurrencyRub = "Rub";
            const string CurrencyEur = "Eur";

            float rublesInWallent;
            float dollarsInWallent;
            float euroInWallent;

            float usdToRub = 85;
            float rubToUsd = 1 / usdToRub;
            float eurToRub = 92;
            float rubToEur = 1 / eurToRub;
            float usdToEur = 1.5f;
            float eurToUsd = 1.5f;
            float exchangeCurrencyCount;

            bool isOpen = true;
            string userInput;

            Console.Write("Введите ваш балланс рублей: ");
            rublesInWallent = Convert.ToSingle(Console.ReadLine());
            Console.Write("Введите ваш балланс долларов: ");
            dollarsInWallent = Convert.ToSingle(Console.ReadLine());
            Console.Write("Введите ваш балланс евро: ");
            euroInWallent = Convert.ToSingle(Console.ReadLine());

            while (isOpen)
            {
                Console.WriteLine($"\nКурс доллара составляет {rubToUsd} рублей\nКурс евро составляет {rubToEur} рублей\nКурс евро составляет {usdToEur} долларов\n");

                Console.Write($"Команда {CommandRubToUsd} сконвертировать валюту {CurrencyRub} в валюту {CurrencyUsd}\n");
                Console.Write($"Команда {CommandRubToEur} сконвертировать валюту {CurrencyRub} в валюту {CurrencyEur}\n");
                Console.Write($"Команда {CommandUsdToRub} сконвертировать валюту {CurrencyUsd} в валюту {CurrencyRub}\n");
                Console.Write($"Команда {CommandUsdToEur} сконвертировать валюту {CurrencyUsd} в валюту {CurrencyEur}\n");
                Console.Write($"Команда {CommandEurToRub} сконвертировать валюту {CurrencyEur} в валюту {CurrencyRub}\n");
                Console.Write($"Команда {CommandEurToUsd} сконвертировать валюту {CurrencyEur} в валюту {CurrencyUsd}\n");
                Console.Write($"Команда {CommandExit} выйти из программы!\n\n");

                Console.Write("Введина команда: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandRubToUsd:
                        Console.WriteLine("Обмен рублей на доллары.");
                        Console.Write("Сколько вы хотите обменять? ");
                        exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

                        if (rublesInWallent >= exchangeCurrencyCount)
                        {
                            rublesInWallent -= exchangeCurrencyCount;
                            dollarsInWallent += exchangeCurrencyCount * rubToUsd;
                        }
                        else
                        {
                            Console.WriteLine("Средств для операции не достаточно!");
                        }

                        break;

                    case CommandRubToEur:
                        Console.WriteLine("Обмен рублей на евры.");
                        Console.Write("Сколько вы хотите обменять? ");
                        exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

                        if (rublesInWallent >= exchangeCurrencyCount)
                        {
                            rublesInWallent -= exchangeCurrencyCount;
                            euroInWallent += exchangeCurrencyCount * rubToEur;
                        }
                        else
                        {
                            Console.WriteLine("Средств для операции не достаточно!");
                        }

                        break;

                    case CommandUsdToRub:
                        Console.WriteLine("Обмен долларов на рубли.");
                        Console.Write("Сколько вы хотите обменять? ");
                        exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

                        if (dollarsInWallent >= exchangeCurrencyCount)
                        {
                            dollarsInWallent -= exchangeCurrencyCount;
                            rublesInWallent += exchangeCurrencyCount * usdToRub;
                        }
                        else
                        {
                            Console.WriteLine("Средств для операции не достаточно!");
                        }

                        break;

                    case CommandUsdToEur:
                        Console.WriteLine("Обмен долларов на евро.");
                        Console.Write("Сколько вы хотите обменять? ");
                        exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

                        if (dollarsInWallent >= exchangeCurrencyCount)
                        {
                            dollarsInWallent -= exchangeCurrencyCount;
                            euroInWallent += exchangeCurrencyCount * usdToEur;
                        }
                        else
                        {
                            Console.WriteLine("Средств для операции не достаточно!");
                        }

                        break;

                    case CommandEurToRub:
                        Console.WriteLine("Обмен евро на рубли.");
                        Console.Write("Сколько вы хотите обменять? ");
                        exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

                        if (euroInWallent >= exchangeCurrencyCount)
                        {
                            euroInWallent -= exchangeCurrencyCount;
                            rublesInWallent += exchangeCurrencyCount * eurToRub;
                        }
                        else
                        {
                            Console.WriteLine("Средств для операции не достаточно!");
                        }

                        break;

                    case CommandEurToUsd:
                        Console.WriteLine("Обмен евро на доллараы.");
                        Console.Write("Сколько вы хотите обменять? ");
                        exchangeCurrencyCount = Convert.ToSingle(Console.ReadLine());

                        if (euroInWallent >= exchangeCurrencyCount)
                        {
                            euroInWallent -= exchangeCurrencyCount;
                            dollarsInWallent += exchangeCurrencyCount * eurToUsd;
                        }
                        else
                        {
                            Console.WriteLine("Средств для операции не достаточно!");
                        }

                        break;

                    case CommandExit:
                        isOpen = false;
                        Console.WriteLine("Вы вышли из программы");
                        break;

                    default:
                        Console.WriteLine("Такой команды не существует!\n");
                        break;
                }

                Console.WriteLine($"\nВаш балланс рублей: {rublesInWallent}");
                Console.WriteLine($"Ваш балланс долларов: {dollarsInWallent}");
                Console.WriteLine($"Ваш балланс евро: {euroInWallent}");
            }

            Console.ReadLine();
        }
    }
}
