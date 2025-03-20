using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> cashSumsClients = new Queue<int>();
            int minNumber = 5;
            int maxNumber = 100;
            int clientsAmount = 10;

            int cashAccount;

            FillQueue(cashSumsClients, clientsAmount, minNumber, maxNumber);

            cashAccount = ServiceQueue(cashSumsClients);

            ShowCash(cashAccount);

            Console.ReadLine();
        }

        static void FillQueue(Queue<int> cashSumsClients, int clientsAmount, int minNumber, int maxNumber)
        {
            Random random = new Random();

            for (int i = 0; i < clientsAmount; i++)
                cashSumsClients.Enqueue(random.Next(minNumber, maxNumber + 1));
        }

        static int ServiceQueue(Queue<int> cashSumsClients)
        {
            int sumResult = 0;
            int clientCount = cashSumsClients.Count;

            for (int i = 0; i < clientCount; i++)
            {
                Console.Clear();

                OutputClient(cashSumsClients, i);
                sumResult += cashSumsClients.Peek();
                ShowCash(sumResult);
                cashSumsClients.Dequeue();

                Console.ReadLine();
            }

            return sumResult;
        }
  
        static void OutputClient(Queue<int> cashSumsClients, int clientIndex)
        {
            Console.WriteLine($"Клиент №{clientIndex + 1} Cумма покупкт клиента: {cashSumsClients.Peek()}");
        }

        static void ShowCash(int cashAccount)
        {
            Console.WriteLine($"Ваш счёт: {cashAccount}");
        }
    }
}
