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
            string name;
            string age;
            string weight;
            string work;

            Console.Write("Как вас зовут: ");
            name = Console.ReadLine();

            Console.Write("Сколько вам лет: ");
            age = Console.ReadLine();

            Console.Write("Ваш вес: ");
            weight = Console.ReadLine();

            Console.Write("Кем вы работаете: ");
            work = Console.ReadLine();

            Console.WriteLine($"Вас зовут {name}, вам {age} года, вы весите {weight} килограммов и работаете {work}");
            Console.ReadLine();
        }
    }
}
