using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandAdd = "1";
            const string CommandDelete = "2";
            const string CommandSearch = "3";
            const string CommandShowAll = "4";
            const string CommandExit = "5";

            bool isOpen = true;

            BookDepository bookDepository = new BookDepository();

            while (isOpen)
            {
                Console.WriteLine($"Команда добавить - {CommandAdd}");
                Console.WriteLine($"Команда удалить - {CommandDelete}");
                Console.WriteLine($"Команда поиск - {CommandSearch}");
                Console.WriteLine($"Команда показать всю библиотеку - {CommandShowAll}");
                Console.WriteLine($"Команда выйти - {CommandExit}");

                Console.Write("Введите команду: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAdd:
                        bookDepository.AddBook();
                        break;

                    case CommandDelete:
                        bookDepository.DeleteBook();
                        break;

                    case CommandSearch:
                        bookDepository.SearchBook();
                        break;

                    case CommandShowAll:
                        bookDepository.ShowAllBooks();
                        break;

                    case CommandExit:
                        isOpen = false;
                        break;

                    default:
                        Console.WriteLine("Введина неверная команда");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class BookDepository
    {
        private const string CommandName = "1";
        private const string CommandAuthor = "2";
        private const string CommandYearOfRelease = "3";

        private List<Book> _books = new List<Book>();

        public void AddBook()
        {
            DrawLine();
            Console.WriteLine("Заполнение данных книги");

            _books.Add(new Book(ReadText("\nНазвание: "), ReadText("\nАвтор: "), ReadText("\nГод выпуска: ")));
        }

        public void DeleteBook()
        {
            DrawLine();
            Console.WriteLine("Удаление книги");

            ShowAllBooks();

            Console.Write("Введите номер книги: ");

            if (int.TryParse(Console.ReadLine(), out int userInput))
                _books.RemoveAt(userInput - 1);
            else
                Console.WriteLine("Неверная команда");

            Console.ReadKey();
        }

        public void SearchBook()
        {
            DrawLine();
            Console.WriteLine("Поиск книги");

            ShowCommand();

            Console.Write("Введите команду: ");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case CommandName:
                    ShowBookInfo(GetBookByName());
                    break;

                case CommandAuthor:
                    ShowBookInfo(GetBookByAuthor());
                    break;

                case CommandYearOfRelease:
                    ShowBookInfo(GetBookByDate());
                    break;

                default:
                    Console.WriteLine("Неверная команда");
                    break;
            }

            Console.ReadKey();
        }

        public void ShowAllBooks()
        {
            for (int i = 0; i < _books.Count; i++)
            {
                DrawLine();

                Console.WriteLine($"{i + 1}.");

                ShowBookInfo(_books[i]);
            }
        }

        private void ShowCommand()
        {
            Console.WriteLine("По каким данным вы хотите произвести операцию над произведение: ");
            Console.WriteLine($"По названию - {CommandName}");
            Console.WriteLine($"По автору - {CommandAuthor}");
            Console.WriteLine($"По году выпуска - {CommandYearOfRelease}");
        }

        private string ReadText(string infoText)
        {
            Console.Write(infoText);

            return Console.ReadLine();
        }

        private Book GetBookByName()
        {
            string userInput = Console.ReadLine();

            foreach (var book in _books)
            {
                if (book.Name == userInput)
                    return book;
            }

            Console.WriteLine("Такой книги нету");
            return null;
        }

        private Book GetBookByAuthor()
        {
            string userInput = Console.ReadLine();

            foreach (var book in _books)
            {
                if (book.Author == userInput)
                    return book;
            }

            Console.WriteLine("Такой книги нету");
            return null;
        }

        private Book GetBookByDate()
        {
            string userInput = Console.ReadLine();

            foreach (var book in _books)
            {
                if (book.YearOfRelease == userInput)
                    return book;
            }

            Console.WriteLine("Такой книги нету");
            return null;
        }

        private void ShowBookInfo(Book book)
        {
            if (book != null)
            {
                Console.WriteLine($"Название - {book.Name}");
                Console.WriteLine($"Автор - {book.Author}");
                Console.WriteLine($"Год выпуска - {book.YearOfRelease}");
            }
        }

        private void DrawLine()
        {
            Console.WriteLine(new string('_', 20));
        }
    }

    class Book
    {
        public Book(string name, string author, string yearsOfRelease)
        {
            Name = name;
            Author = author;
            YearOfRelease = yearsOfRelease;
        }

        public string Name { get; private set; }
        public string Author { get; private set; }
        public string YearOfRelease { get; private set; }
    }
}
