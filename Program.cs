using System;

namespace ConsoleApp1
{
    class Program
    {   
        static void Main(string[] args)
        {
            const string HealthText = "Здоровье: ";
            const string ManaText = "Мана: ";

            int health = 5;
            int maxHealth = 10;
            int mana = 5;
            int maxMana = 8;

            Console.Write(HealthText);
            DrawBar(health, maxHealth, ConsoleColor.Green, '$', HealthText.Length);

            Console.Write($"\n{ManaText}");
            DrawBar(mana, maxMana, ConsoleColor.Blue, '#', ManaText.Length, 1);

            Console.ReadLine();
        }

        static void DrawBar(float value, float maxValue, ConsoleColor color, char symbolBar = ' ', int cursorPositionX = 0, int cursorPositionY = 0)
        {
            ConsoleColor defaulteColor = Console.BackgroundColor;

            string bar = "";

            value /= maxValue;
            maxValue /= maxValue;

            FillingBar(0, value, ref bar, symbolBar);

            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            Console.Write('[');
            Console.BackgroundColor = color;
            Console.Write(bar);
            Console.BackgroundColor = defaulteColor;

            bar = "";

            FillingBar(value, maxValue, ref bar);

            Console.Write(bar + ']');
        }

        static void FillingBar(float value, float maxValue, ref string bar, char symbol = '_', float fillMultiplier = 0.1f)
        {
            for (float i = value; i < maxValue; i += fillMultiplier)
            {
                bar += symbol;
            }
        }
    }
}
