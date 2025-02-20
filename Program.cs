using System;

namespace ConsoleApp1
{
    class Program
    {   
        static void FillQueue(List<int> cashSumsClients, int clientsAmount, int minNumber, int maxNumber)
        {
            Random random = new Random();
            int minNumber = 5;
            int maxNumber = 100;

            for (int i = 0; i < clientsAmount; i++)
                cashSumsClients.Add(random.Next(minNumber, maxNumber + 1));
        }

        static void ServiceQueue(List<int> cashSumsClients, ref int cashAccount)
        {
            for (int i = 0; i < cashSumsClients.Count; i++)
            {
                Console.Clear();

                OutputResult(cashSumsClients, i, ref cashAccount);

                Console.ReadLine();
            }
        }

        static void OutputResult(List<int> cashSumsClients, int clientIndex, ref int cashAccount)
        {
            Console.WriteLine($"Клиент №{clientIndex + 1} Cумма покупкт клиента: {cashSumsClients[clientIndex]}");
            Console.WriteLine($"Ваш счёт: {cashAccount}");
            cashAccount += cashSumsClients[clientIndex];
            cashSumsClients.Remove(clientIndex);
        }

        static void Main(string[] args)
        {
            List<int> cashSumsClients = new List<int>();
            int minNumber = 5;
            int maxNumber = 100;
            int clientsAmount = 10;

            int cashAccount = 0;

            FillQueue(cashSumsClients, clientsAmount, minNumber, maxNumber);

            ServiceQueue(cashSumsClients, ref cashAccount);
        }
    }
}
