using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        void ShowMessage(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
        }

        static void FillQueue(List<int> cashSumsClients, int clientsAmount, int minNumber, int maxNumber)
        {
            Random random = new Random();

            for (int i = 0; i < clientsAmount; i++)
                cashSumsClients.Add(random.Next(minNumber, maxNumber + 1));
        }

        static int ServiceQueue(List<int> cashSumsClients)
        {
            int sumResult = 0;

            for (int i = 0; i < cashSumsClients.Count; i++)
            {
                Console.Clear();

                OutputClient(cashSumsClients, i);
                sumResult += cashSumsClients[i];
                ShowCash(sumResult);
                cashSumsClients.Remove(i);

                Console.ReadLine();
            }

            return sumResult;
        }
  
        static void OutputClient(List<int> cashSumsClients, int clientIndex)
        {
            Console.WriteLine($"Клиент №{clientIndex + 1} Cумма покупкт клиента: {cashSumsClients[clientIndex]}");
        }

        static void ShowCash(int cashAccount)
        {
            Console.WriteLine($"Ваш счёт: {cashAccount}");
        }

        static void Main(string[] args)
        {
            List<int> cashSumsClients = new List<int>();
            int minNumber = 5;
            int maxNumber = 100;
            int clientsAmount = 10;

            int cashAccount;

            FillQueue(cashSumsClients, clientsAmount, minNumber, maxNumber);

            cashAccount = ServiceQueue(cashSumsClients);
        }
    }
}
