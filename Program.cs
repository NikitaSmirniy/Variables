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
            int waitingTime;

            Console.Write("Сколько людей в очереди? ");
            humanAmount = Convert.ToInt32(Console.ReadLine());

            waitingTime = humanAmount * receptionTime;
            Console.Write($"Вы должны отстоять в очереди {waitingTime} минут!");
            Console.ReadLine();
        }
    }
}
