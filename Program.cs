using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddEmployee = "add";
            const string CommandDeleteEmployee = "delete";
            const string CommandShowAllEmployee = "show";
            const string CommandExit = "exit";

            Dictionary<string, List<string>> employees = new Dictionary<string, List<string>>();

            bool isOpen = true;

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
                        AddEmpoyee(employees);
                        break;

                    case CommandDeleteEmployee:
                        DeleteEmployee(employees);
                        break;

                    case CommandShowAllEmployee:
                        ShowEmployee(employees);
                        break;

                    case CommandExit:
                        isOpen = false;
                        break;

                    default:
                        Console.WriteLine("Не верная команада");
                        break;
                }

                Console.ReadKey();
            }

            foreach (var manager in employees)
            {
                Console.Write($"{manager.Key} {manager.Value}");
            }

            employees.Remove("gg");
        }

        static void AddEmpoyee(Dictionary<string, List<string>> employees)
        {
            Console.Clear();

            Console.Write("Введите фио сотрудника: ");
            string name = Console.ReadLine();
            Console.Write("Введите должность сотрудника: ");
            string workplace = Console.ReadLine();

            employees.Add(name, new List<string> { workplace });
        }

        static void DeleteEmployee(Dictionary<string, List<string>> employees)
        {
            Console.Clear();

            ShowEmployee(employees);

            Console.Write("Введите имя сотрудника которого нужно удалить - ");
            string userInput = Console.ReadLine();

            foreach (var employee in employees)
            {
                if (employee.Key == userInput)
                {
                    employees.Remove(employee.Key);
                    Console.WriteLine("Сотрудник удалён из базы");
                    break;
                }
                else
                {
                    Console.WriteLine("Данный сотрудник не числится  в базе");
                    break;
                }
            }
        }

        static void ShowEmployee(Dictionary<string, List<string>> employees)
        {
            int emploueeNumber = 0;

            Console.Clear();

            foreach (var employee in employees)
            {
                emploueeNumber++;

                Console.Write($"{emploueeNumber}. {employee.Key} ");

                foreach (var value in employee.Value)
                {
                    Console.WriteLine(value);
                }
            }
        }
        }
    }
}
