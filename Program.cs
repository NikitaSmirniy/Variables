using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddEmployee = "1";
            const string CommandDeleteEmployee = "2";
            const string CommandShowAllEmployee = "3";
            const string CommandExit = "exit";

            Dictionary<string, List<string>> employees = new Dictionary<string, List<string>>();

            bool isOpen = true;

            employees["Cleaner"] = new List<string>() { "Slav", "Foker" };
            employees["Slesar"] = new List<string>() { "Slav", "Rooter" };

            while (isOpen)
            {
                Console.Clear();

                Console.WriteLine($"Комманда {CommandAddEmployee} - добавить сотрудника");
                Console.WriteLine($"Комманда {CommandDeleteEmployee} - удалить сотрудника");
                Console.WriteLine($"Комманда {CommandShowAllEmployee} - показать всех сотрудников");
                Console.WriteLine($"Комманда {CommandExit} - выйти из программы");

                string userIput = Console.ReadLine();

                switch (userIput.ToLower())
                {
                    case CommandAddEmployee:
                        AddEmployee(employees);
                        break;

                    case CommandDeleteEmployee:
                        DeleteEmployee(employees);
                        break;

                    case CommandShowAllEmployee:
                        ShowEmployees(employees);
                        break;

                    case CommandExit:
                        isOpen = false;
                        break;

                    default:
                        Console.WriteLine("Не верная команада");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        static void AddEmployee(Dictionary<string, List<string>> employees)
        {
            Console.Write("Введите должность сотрудника: ");
            string workplace = Console.ReadLine();
            Console.Write("Введите фио сотрудника: ");
            string name = Console.ReadLine();

            if (employees.ContainsKey(workplace) == false)
                employees.Add(workplace, new List<string>());

            employees[workplace].Add(name);
        }

        static void DeleteEmployee(Dictionary<string, List<string>> employees)
        {
            ShowWorkPlace(employees);

            Console.Write("Введите должность сотрудника: ");
            string workplace = Console.ReadLine();

            if (employees.ContainsKey(workplace))
            {
                List<string> fullNames = employees[workplace];

                for (int i = 0; i < fullNames.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {fullNames[i]}");
                }

                Console.WriteLine("Введите порядковый номер сотрудника: ");
                int.TryParse(Console.ReadLine(), out int userInput);

                if (userInput > 0 && userInput <= fullNames.Count)
                {
                    fullNames.RemoveAt(userInput - 1);

                    if (fullNames.Count == 0)
                        employees.Remove(workplace);

                    Console.WriteLine("Сотрудник удалён из базы");
                }
                else
                {
                    Console.WriteLine("Cотрудник под этим индексом не числится в базе");
                }
            }
        }

        static void ShowWorkPlace(Dictionary<string, List<string>> employees)
        {
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.Key);
            }
        }

        static void ShowEmployees(Dictionary<string, List<string>> employees)
        {
            foreach (var employee in employees)
            {
                int emploueeNumber = 0;

                Console.WriteLine("Должности:");
                Console.Write($"{employee.Key}\n");

                foreach (var value in employee.Value)
                {
                    emploueeNumber++;
                    Console.WriteLine($"{emploueeNumber}. {value}");
                }

                emploueeNumber = 0;
            }
        }
    }
}
