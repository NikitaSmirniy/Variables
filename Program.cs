using System;

namespace ConsoleApp1
{
    class Program
    {   
        static void Main(string[] args)
        {
            const string HealthText = "Здоровье: ";
            const string ManaText = "Мана: ";

            int health = 1000;
            int maxHealth = 20;
            int mana = 85;
            int maxMana = 10;

            Console.Write(HealthText);
            DrawBar(health, maxHealth, ConsoleColor.Green, '$', HealthText.Length);

            Console.Write($"\n{ManaText}");
            DrawBar(mana, maxMana, ConsoleColor.Blue, '#', ManaText.Length, 1);

            Console.ReadLine();
        }

        static void DrawBar(float value, int maxValue, ConsoleColor color, char symbolBar = ' ', int cursorPositionX = 0, int cursorPositionY = 0)
        {
            ConsoleColor defaulteColor = Console.BackgroundColor;

            int percentage = 100;
            string bar = "";

            if (value > percentage)
                value = maxValue;
            else if (value <= 0)
                value = 0;
            else
                value = (float)maxValue / percentage * value;

            bar = FillBar(0, (int)value, bar, symbolBar);

            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            Console.Write('[');
            Console.BackgroundColor = color;
            Console.Write(bar);
            Console.BackgroundColor = defaulteColor;

            bar = "";

            bar = FillBar((int)value, maxValue, bar);

            Console.Write(bar + ']');
        }

        static string FillBar(int value, int maxValue, string bar, char symbol = '_')
        {
            for (int i = value; i < maxValue; i++)
            {
                bar += symbol;
            }

            return bar;
        }
    }
}
