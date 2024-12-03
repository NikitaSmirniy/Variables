using System;

namespace ConsoleApp1
{
    class Program
    {   
        static void Main(string[] args)
        {
            const string CommandAddDossier = "1";
            const string CommandShowAllDossier = "2";
            const string CommandDeleteDosier = "3";
            const string CommandSearchByLastName = "4";
            const string CommandExit = "5";

            string[] names = new string[0];
            string[] jopPosition = new string[0];

            bool isOpen = true;

            while (isOpen)
            {
                Console.Clear();

                Console.WriteLine($"Добавить досье - {CommandAddDossier}");
                Console.WriteLine($"Вывести все досье - {CommandShowAllDossier}");
                Console.WriteLine($"Удалить досье - {CommandDeleteDosier}");
                Console.WriteLine($"Поиск по фамилии - {CommandSearchByLastName}");
                Console.WriteLine($"Выйти из программы - {CommandExit}");

                Console.Write("Выберите команду: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAddDossier:
                        AddDossier(ref names, ref jopPosition);
                        break;

                    case CommandShowAllDossier:
                        ShowAllDossier(names, jopPosition, names.Length);
                        break;

                    case CommandDeleteDosier:
                        DeleteDossier(ref names, ref jopPosition);
                        break;

                    case CommandSearchByLastName:
                        SearchDossier(names, jopPosition);
                        break;

                    case CommandExit:
                        isOpen = false;
                        break;

                    default:
                        Console.WriteLine("Введина неверная команда");
                        break;
                }

                Console.ReadLine();
            }
        }

        static void ShowDossier(string[] names, string[] jopPosition, int index)
        {
            Console.WriteLine($"{index + 1} - ФИО {names[index]}, Должность {jopPosition[index]}");
        }

        static void ShowAllDossier(string[] names, string[] jopPosition, int elementsLength)
        {
            for (int i = 0; i < elementsLength; i++)
                ShowDossier(names, jopPosition, i);
        }

        static string[] RemoveAt(string[] array, int index)
        {
            string[] newNames = new string[array.Length - 1];
            index--;

            for (int i = 0; i < index; i++)
                newNames[i] = array[i];

            for (int i = index + 1; i < array.Length; i++)
                newNames[i - 1] = array[i];

            return newNames;
        }

        static void DeleteDossier(ref string[] names, ref string[] jopPosition)
        {
            bool isOpen = true;

            while (isOpen && names.Length > 0)
            {
                Console.Clear();

                ShowAllDossier(names, jopPosition, names.Length);

                Console.Write("Досье под каким номером вы хотите удалить: ");
                string userInput = Console.ReadLine();

                int shift;
                bool isSuccess = int.TryParse(userInput, out shift);

                Console.WriteLine(shift);

                if (isSuccess && shift > 0 && shift <= names.Length)
                {
                    names = RemoveAt(names, shift);
                    jopPosition = RemoveAt(jopPosition, shift);

                    isOpen = false;
                }
                else
                {
                    Console.WriteLine("Неверная комманда!");
                    Console.ReadLine();
                }
            }
        }

        static string[] CopyElements(string[] copiedArray, string[] array)
        {
            for (int i = 0; i < copiedArray.Length; i++)
                array[i] = copiedArray[i];

            return array;
        }

        static void FillElement(string[] filliedArray, string questionText)
        {
            Console.Write($"{questionText} - ");
            string fillText = Console.ReadLine();
            filliedArray[filliedArray.Length - 1] = fillText;
        }

        static void AddDossier(ref string[] names, ref string[] jobPosition)
        {
            string[] tempNames = new string[names.Length + 1];
            string[] tempJobPosition = new string[jobPosition.Length + 1];

            names = CopyElements(names, tempNames);
            jobPosition = CopyElements(jobPosition, tempJobPosition);

            FillElement(names, "Введите ФИО сотрудника");
            FillElement(jobPosition, "Введите должность сотрудника");
        }

        static void SearchDossier(string[] names, string[] jopPosition)
        {
            const string CommandExit = "Exit";

            bool isOpen = true;

            while (isOpen && names.Length > 0)
            {
                Console.Clear();

                Console.Write("Введите Фамилию сотрудника: ");
                string userInput = Console.ReadLine();
                bool isEmptySurname = true;

                for (int i = 0; i < names.Length; i++)
                {
                    string surname = names[i].Split()[0];

                    if (userInput.ToLower() == surname.ToLower())
                    {
                        ShowDossier(names, jopPosition, i);
                        isEmptySurname = false;
                    }
                }

                if (isEmptySurname)
                    Console.WriteLine("Такой Фамилии нет в списках");

                Console.Write($"Введите команду - {CommandExit} если хотите прекратить поиск: ");
                userInput = Console.ReadLine();

                if (userInput == CommandExit)
                    isOpen = false;
            }
        }
    }
}
