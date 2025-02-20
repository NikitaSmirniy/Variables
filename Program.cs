using System;

namespace ConsoleApp1
{
    class Program
    {   
        static void Main(string[] args)
        {
            List<int> cashSumsClients = new List<int>();
            int clientsAmount = 10;
            int cashAccount = 0;

            Random random = new Random();
            int minNumber = 5;
            int maxNumber = 100;

            for (int i = 0; i < clientsAmount; i++)
                cashSumsClients.Add(random.Next(minNumber, maxNumber + 1));

            for (int i = 0; i < cashSumsClients.Count; i++)
            {
                Console.Clear();

                Console.WriteLine($"Клиент №{i + 1} Cумма покупкт клиента: {cashSumsClients[i]}");
                Console.WriteLine($"Ваш счёт: {cashAccount}");
                cashAccount += cashSumsClients[i];
                cashSumsClients.Remove(i);

                Console.ReadLine();
            }
        }
    }
}
