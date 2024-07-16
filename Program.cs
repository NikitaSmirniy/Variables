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
            int humanAmount;
            int receptionTime = 10;
            int minutes;
            int houres;
            int totalMinutes;
            int minutesInHour = 60;

            Console.Write("Сколько людей в очереди? ");
            humanAmount = Convert.ToInt32(Console.ReadLine());
            
            totalMinutes = humanAmount * receptionTime;
            houres = totalMinutes / minutesInHour;
            minutes = totalMinutes % minutesInHour;

            Console.Write($"Вы должны отстоять в очереди {houres} часов и {minutes} минут!");
            Console.ReadLine();
        }
    }
}
