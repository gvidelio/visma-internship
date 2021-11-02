using Repositories.Entities;
using System.Collections.Generic;
using Xunit;

namespace Services.Tests
{
    public class UnitTests
    {
        Actions _methods = new Actions();

        [Fact]
        public void Delete_OneBook_ReturnsUpdatedBook()
        {
            _methods.Setup(@"..\..\bookJson1.json");
            List<Book> books = _methods.GetBooks();

            _methods.Delete("123");

            List<Book> booksAfter = _methods.GetBooks();

            Assert.Equal(books, booksAfter);
        }

    }
}
