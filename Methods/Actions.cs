using Newtonsoft.Json;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Services
{
    public class Actions
    {
        private string _path = @"..\..\bookJson.json";
        private List<Book> _books;
        private List<BookTaker> _bookTaker = new List<BookTaker>();

        //read json file to object
        public void Setup(string path = null)
        {
            if (path == null)
            {
                var read = new StreamReader(_path);
                var jsonString = read.ReadToEnd();
                _books = JsonConvert.DeserializeObject<List<Book>>(jsonString) ?? new List<Book>();
                read.Close();
            }
            else
            {
                var read = new StreamReader(path);
                var jsonString = read.ReadToEnd();
                _books = JsonConvert.DeserializeObject<List<Book>>(jsonString) ?? new List<Book>();
                read.Close();
            }
        }

        //add a new book
        public void Add()
        {
            var book = new Book();

            Console.WriteLine("Enter the name of the book: ");
            book.Name = Console.ReadLine();
            Console.WriteLine("Enter the author of the book: ");
            book.Author = Console.ReadLine();
            Console.WriteLine("Enter the category of the book: ");
            book.Category = Console.ReadLine();
            Console.WriteLine("Enter the language of the book: ");
            book.Language = Console.ReadLine();

            //validate date input
            Console.WriteLine("Enter the publication date (yyyy-MM-dd) of the book: ");
            var publicationDate = Console.ReadLine();
            DateTime d;
            while (DateTime.TryParseExact(publicationDate.ToString(), "yyyy-MM-dd",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out d) == false)
            {
                Console.WriteLine("Wrong format! Try again with yyyy-MM-dd: ");
                publicationDate = Console.ReadLine();
            }
            book.PublicationDate = DateTime.ParseExact(publicationDate, "yyyy-MM-dd", CultureInfo.InvariantCulture); ;

            //validate ISBN input
            Console.WriteLine("Enter the ISBN of the book (please follow the required format for the ISBN): ");
            var isbn = Console.ReadLine();
            var regex = new Regex(@"(ISBN[-]*(1[03])*[ ]*(: ){0,1})*(([0-9Xx][- ]*){13}|([0-9Xx][- ]*){10})");
            var match = regex.Match(isbn);
            while (match.Success == false)
            {
                Console.WriteLine("Wrong format... Try again... ISBN: ");
                isbn = Console.ReadLine();
            }

            //check if entered ISBN exists
            while (_books.Find(x => x.ISBN == isbn) != null)
            {
                Console.WriteLine("Such book already exists. Try again with another ISBN: ");
                isbn = Console.ReadLine();
            }
            book.ISBN = isbn;
            book.Availability = "Available";

            _books.Add(book);
        }

        //take a book from the library
        public void Take()
        {
            var bookTaker = new BookTaker();

            Console.WriteLine("Enter your name: ");
            bookTaker.FullName = Console.ReadLine();

            //assure that no more than 3 books are taken
            Console.WriteLine("How many books do you want to take?");
            var quantity = Convert.ToInt32(Console.ReadLine());
            while (quantity > 3)
            {
                Console.WriteLine("Sorry, you cannot take more than 3 books.");
                quantity = Convert.ToInt32(Console.ReadLine());
            }

            for (int i = 0; i < quantity; i++)
            {
                //assure that the book is taken for no longer than 2 months
                Console.WriteLine("Enter the number of months you want to take the book for:");
                var months = Convert.ToInt32(Console.ReadLine());
                while (months > 2)
                {
                    Console.WriteLine("Sorry, you can take the book for no longer than 2 months. Try again:");
                    months = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine("Enter the ISBN of the book you want to take: ");
                var isbn = Console.ReadLine();

                //assure that the book is in the library and is not taken by someone else
                while (_books.Find(y => y.ISBN == isbn) == null
                    || (_books.Find(y => (y.ISBN == isbn) && (y.Availability.Substring(0, 1) == "T")) != null))
                {
                    Console.WriteLine("This book is currently unavailable");
                    Console.WriteLine("Enter another ISBN: ");
                    isbn = Console.ReadLine();
                }

                _books.Where(x => x.ISBN == isbn).FirstOrDefault().Availability = $"Taken by {bookTaker.FullName}";
            }

        }

        //return the book to the library
        public void Return()
        {
            var book = new Book();
            var bookTaker = new BookTaker();
            Console.WriteLine("Enter your name: ");
            bookTaker.FullName = Console.ReadLine();

            //check for the number of books to return
            Console.WriteLine("How many books you want to return?");
            var quantity = Convert.ToInt32(Console.ReadLine());
            while (quantity > 3)
            {
                Console.WriteLine("You cannot have more than 3 books. Enter a valid number");
                quantity = Convert.ToInt32(Console.ReadLine());
            }

            for (int i = 0; i < quantity; i++)
            {
                Console.WriteLine("Enter the ISBN of the book you want to return: ");
                string isbn = Console.ReadLine();

                //check whether the book exists and is taken by the user
                while (_books.Find(y => y.ISBN == isbn) == null ||
                    (_books.Find(y => (y.ISBN == isbn) && (y.Availability != $"Taken by {bookTaker.FullName}")) != null))
                {
                    Console.WriteLine("This book is not taken by you.");
                    Console.WriteLine("Enter another ISBN: ");
                    isbn = Console.ReadLine();
                }

                //check if the book is not returned late
                Console.WriteLine("Enter the date of taking the book: ");
                var startDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                Console.WriteLine("Enter the date of returning the book: ");
                var returnDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                TimeSpan duration = returnDate.Subtract(startDate);
                if (duration.Days > 60)
                    Console.WriteLine("YOU ARE LATE! No worries though...");
                else
                    Console.WriteLine("Hope you liked it!");

                _books.Where(x => x.ISBN == isbn).FirstOrDefault().Availability = "Available";
            }

        }

        //list books by specifying the filter
        public void List()
        {
            Console.WriteLine("Possible filters: Name, Author, Category, " +
                "Language, ISBN, Availability(Taken/Available) or All");
            Console.WriteLine("Enter the filter: ");
            var filter = Console.ReadLine();
            var filterValue = "";

            //validate the filter input
            while (filter != "Name" && filter != "Author"
                && filter != "Category" && filter != "Language"
                && filter != "ISBN" && filter != "Availability" && filter != "All")
            {
                Console.WriteLine("Wrong filter. Try again: ");
                filter = Console.ReadLine();
            }

            //validate the specification of the filter
            if (filter == "All")
                Console.WriteLine(JsonConvert.SerializeObject(_books, Formatting.Indented));
            else if (filter == "Availability")
            {
                Console.WriteLine("Available or Taken?");
                filterValue = Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"Enter {filter}:");
                filterValue = Console.ReadLine();
            }

            if (filter == "Name")
                Console.WriteLine(JsonConvert.SerializeObject(_books.Where(x => x.Name == filterValue), Formatting.Indented));
            else if (filter == "Author")
                Console.WriteLine(JsonConvert.SerializeObject(_books.Where(x => x.Author == filterValue), Formatting.Indented));
            else if (filter == "Category")
                Console.WriteLine(JsonConvert.SerializeObject(_books.Where(x => x.Category == filterValue), Formatting.Indented));
            else if (filter == "Language")
                Console.WriteLine(JsonConvert.SerializeObject(_books.Where(x => x.Language == filterValue), Formatting.Indented));
            else if (filter == "ISBN")
                Console.WriteLine(JsonConvert.SerializeObject(_books.Where(x => x.ISBN == filterValue), Formatting.Indented));
            else if (filterValue == "Available")
                Console.WriteLine(JsonConvert.SerializeObject(_books.Where(x => x.Availability == filterValue), Formatting.Indented));
            else if (filterValue == "Taken")
                Console.WriteLine(JsonConvert.SerializeObject(_books.Where(x => x.Availability.Substring(0, 5) == "Taken"), Formatting.Indented));
        }

        //return all books
        public List<Book> GetBooks()
        {
            return _books;
        }

        //delete a specific book
        public void Delete(string isbn = null)
        {
            Console.WriteLine("Enter the ISBN of the book you want to delete: ");
            if (isbn == null)
            {
                isbn = Console.ReadLine();
            }

            //check whether the book exists in the library
            while (_books.Find(x => x.ISBN == isbn) == null)
            {
                Console.WriteLine("There is no such book. Try again with another ISBN: ");
                isbn = Console.ReadLine();
            }
            _books.Remove(_books.FirstOrDefault(x => x.ISBN == isbn));
        }

        //write the new book list in to the file
        public void Close()
        {
            File.Delete(_path);
            string bookJson = JsonConvert.SerializeObject(_books);
            using (var tw = new StreamWriter(_path, true))
            {
                tw.WriteLine(bookJson.ToString());
                tw.Close();
            }
        }

        public void Success()
        {
            Console.WriteLine("Success!\n");
            Console.WriteLine("The commands are: Add, Take, Return, List, Delete, Exit.\n");
            Console.WriteLine("Your action:");
        }
    }
}
