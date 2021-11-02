using Xunit;
using System;
using Services;
using Repositories.Entities;
using System.Collections.Generic;

namespace Services.Tests
{
    public class UnitTest1
    {
        Methods _methods = new Methods();

        [Fact]
        public void GetBooks_ReturnsAllBooks()
        {
            _methods.Setup(@"C:\Users\Gvidas\OneDrive\Dokumentai\Visma\Visma Internship Task\bookJson1.json");
            List<Book> books = _methods.GetBooks();

            _methods.Delete("123");

            List<Book> books1 = _methods.GetBooks();

            Assert.Equal(books, books1);

        }

        [Fact]
        public void Delete_OneBook_ReturnsUpdatedBook()
        {
            _methods.Setup(@"C:\Users\Gvidas\OneDrive\Dokumentai\Visma\Visma Internship Task\bookJson1.json");
            List<Book> books = _methods.GetBooks();

            _methods.Delete("123");

            List<Book> books1 = _methods.GetBooks();

            Assert.Equal(books, books1);

        }

    }
}
