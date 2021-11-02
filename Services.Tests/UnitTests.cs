using Xunit;
using System;
using Services;
using Repositories.Entities;
using System.Collections.Generic;

namespace Services.Tests
{
    public class UnitTests
    {
        Actions _methods = new Actions();

        [Fact]
        public void Delete_OneBook_ReturnsUpdatedBook()
        {
            _methods.Setup(@"..\..\bookJson.json");
            List<Book> books = _methods.GetBooks();

            _methods.Delete("123");

            List<Book> books1 = _methods.GetBooks();

            Assert.Equal(books, books1);
        }

    }
}
