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
            //Картинки
            int pictures;
            int rowPictures = 3;

            Console.Write("Сколько картинок должно быть в вашем альбоме: ");
            pictures = Convert.ToInt32(Console.ReadLine());
            
            Console.Write($"Картинок всего: {pictures / rowPictures}\n");
            Console.Write($"Картинок сверх нормы: {pictures % rowPictures} ");
            Console.ReadLine();
        }
    }
}
