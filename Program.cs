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
                        bookDepository.Add();
                        break;

                    case CommandDelete:
                        bookDepository.Delete();
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

            Console.ReadKey();
        }
    }

    class BookDepository
    {
        private const string CommandName = "1";
        private const string CommandAuthor = "2";
        private const string CommandYearOfRelease = "3";
        private const string CommandExit = "4";

        private List<Book> _books = new List<Book>();

        public void Add()
        {
            IndentLine();
            Console.WriteLine("Заполнение данных книги");

            _books.Add(new Book(SetText("\nНазвание: "), SetText("\nАвтор: "), SetText("\nГод выпуска: ")));
        }

        public void Delete()
        {
            IndentLine();
            Console.WriteLine("Удаление книги");

            ShowCommand();

            Console.Write("Введите команду: ");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case CommandName:
                    _books.Remove(GetBookByName());
                    break;

                case CommandAuthor:
                    _books.Remove(GetBookByAuthor());
                    break;

                case CommandYearOfRelease:
                    _books.Remove(GetBookByDate());
                    break;

                default:
                    Console.WriteLine("Неверная команда");
                    break;
            }

            Console.WriteLine("Вы удалили книгу");
            Console.ReadKey();
        }

        public void SearchBook()
        {
            IndentLine();
            Console.WriteLine("Поиск книги");

            ShowCommand();

            Console.Write("Введите команду: ");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case CommandName:
                    ShowBooksByNameFound();
                    break;

                case CommandAuthor:
                    ShowBooksByAuthorFound();
                    break;

                case CommandYearOfRelease:
                    ShowBooksByDateFound();
                    break;

                default:
                    Console.WriteLine("Неверная команда");
                    break;
            }

            Console.ReadKey();
        }

        public void ShowAllBooks()
        {
            foreach (var book in _books)
            {
                IndentLine();

                book.ShowInfo();
            }
        }

        private void ShowCommand()
        {
            Console.WriteLine("По каким данным вы хотите произвести операцию над произведение: ");
            Console.WriteLine($"По названию - {CommandName}");
            Console.WriteLine($"По автору - {CommandAuthor}");
            Console.WriteLine($"По году выпуска - {CommandYearOfRelease}");
            Console.WriteLine($"Выйти - {CommandExit}");
        }

        private string SetText(string infoText)
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
                else
                    Console.WriteLine("Вы ввели несуществующие данные");
            }

            return null;
        }

        private Book GetBookByAuthor()
        {
            string userInput = Console.ReadLine();

            foreach (var book in _books)
            {
                if (book.Author == userInput)
                    return book;
                else
                    Console.WriteLine("Вы ввели несуществующие данные");
            }

            return null;
        }

        private Book GetBookByDate()
        {
            string userInput = Console.ReadLine();

            foreach (var book in _books)
            {
                if (book.YearOfRelease == userInput)
                    return book;
                else
                    Console.WriteLine("Вы ввели несуществующие данные");
            }

            return null;
        }

        private void ShowBooksByNameFound()
        {
            string userInput = Console.ReadLine();

            foreach (var book in _books)
            {
                if (book.Name == userInput)
                    book.ShowInfo();
                else
                    Console.WriteLine("Вы ввели несуществующие данные");
            }
        }

        private void ShowBooksByAuthorFound()
        {
            string userInput = Console.ReadLine();

            foreach (var book in _books)
            {
                if (book.Author == userInput)
                    book.ShowInfo();
                else
                    Console.WriteLine("Вы ввели несуществующие данные");
            }
        }

        private void ShowBooksByDateFound()
        {
            string userInput = Console.ReadLine();

            foreach (var book in _books)
            {
                if (book.YearOfRelease == userInput)
                    book.ShowInfo();
                else
                    Console.WriteLine("Вы ввели несуществующие данные");
            }
        }

        private void IndentLine()
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

        public void ShowInfo()
        {
            Console.WriteLine($"Название - {Name}");
            Console.WriteLine($"Автор - {Author}");
            Console.WriteLine($"Год выпуска - {YearOfRelease}");
        }
    }
}
