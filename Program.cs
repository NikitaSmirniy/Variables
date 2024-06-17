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
            int appleAmount, applePrice;
            float radius, speed;
            bool isOpen, haveApple;
            string appleAmountText, applePriceText;
            double weight = 0.25;
            char firstLetterAlphabet = 'A';

            appleAmount = 3;
            applePrice = Convert.ToInt32(weight) * appleAmount;

            radius = 2;
            speed = 13.125f;

            isOpen = true;
            haveApple = appleAmount > 0;

            appleAmountText = "Apple: " + appleAmount;
            applePriceText = applePrice.ToString();

        }
    }
}
