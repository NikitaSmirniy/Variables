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
            char userSymbol;
            string userName;

            Console.Write("Введите ваш символ: ");
            userSymbol = Convert.ToChar(Console.ReadLine());

            Console.Write("Введите ваше имя: ");
            userName = Console.ReadLine();

            for (int i = 0; i < userName.Length; i++)
            {
                Console.Write(userSymbol);
            }

            Console.ReadLine();
        }
    }
}
