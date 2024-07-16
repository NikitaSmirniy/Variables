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
            int gold;
            int diamond;
            int diamondUnitPrice = 15;
            bool isAbleToPay;

            Console.WriteLine("Добрый день, сегодня кристалы стоят: " + diamondUnitPrice + " единиц золота");
            Console.Write("Сколько единиц золота у вас имеется: ");
            gold = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("сколько кристалов вы хотите приобрести: ");
            diamond = Convert.ToInt32(Console.ReadLine());
            isAbleToPay = gold >= diamond * diamondUnitPrice;
            diamond *= Convert.ToInt32(isAbleToPay);
            gold -= diamond * diamondUnitPrice;

            Console.Clear();
            Console.Write($"Покупка прошла успешно, сейчас в вашей сумке {diamond} крисатлов и {gold} золота");
            Console.ReadLine();
        }
    }
}
