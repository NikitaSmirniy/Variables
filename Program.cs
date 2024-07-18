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
            int addNumber = 7;
            int maximumNumber = 103;

            // Используется цикл for, потому-что так код выглядит более компактным, удобным в чтении и не нужно создавать 
            // ещё какие либо переменные в полях, как например пришлось бы это делать для работы с циклом while
            for (int i = 5; i <= maximumNumber; i += addNumber)
            {
                Console.Write(i + "\n");
            }

            Console.ReadLine();
        }
    }
}
