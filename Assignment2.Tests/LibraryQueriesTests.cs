
using System.Collections.Generic;
using System.Linq;
using LibraryManagement;
using Xunit;

namespace Assignment2.Tests
{
    public class LibraryQueriesTests
    {
        // Shared test data
        private static List<Book> GetBooks()
        {
            return new List<Book>
            {
                new Book("The Hobbit", "J.R.R. Tolkien", "Fantasy", 1937, true),
                new Book("1984", "George Orwell", "Dystopian", 1949, false),
                new Book("Animal Farm", "George Orwell", "Satire", 1945, true),
                new Book("The Great Gatsby", "F. Scott Fitzgerald", "Classic", 1925, true),
                new Book("To Kill a Mockingbird", "Harper Lee", "Classic", 1960, false),
                new Book("Dune", "Frank Herbert", "Science Fiction", 1965, true),
                new Book("Foundation", "Isaac Asimov", "Science Fiction", 1951, true),
                new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling", "Fantasy", 1997, true),
                new Book("The Da Vinci Code", "Dan Brown", "Thriller", 2003, false),
                new Book("The Alchemist", "Paulo Coelho", "Fiction", 1988, true),
                new Book("The Hunger Games", "Suzanne Collins", "Dystopian", 2008, true),
                new Book("The Fault in Our Stars", "John Green", "Romance", 2012, false),
                new Book("The Martian", "Andy Weir", "Science Fiction", 2011, true),
                new Book("Educated", "Tara Westover", "Memoir", 2018, true),
                new Book("Project Hail Mary", "Andy Weir", "Science Fiction", 2021, true),
                new Book("Atomic Habits", "James Clear", "Self-Help", 2018, true)
            };
        }

        //Books by author
        [Fact]
        public void BooksByAuthor_ReturnsOnlyAvailableBooksByAuthor()
        {
            // Arrange
            var books = GetBooks();

            // Act
            var result = BookQueries.BooksByAuthor("Suzanne Collins", books);

            // Assert
            Assert.Single(result);
            Assert.Equal("The Hunger Games", result[0].Title);
            Assert.Equal("Suzanne Collins", result[0].Author);
            Assert.True(result[0].IsAvailable);
        }

        // Group books by genre
        [Fact]
        public void BooksByGenre_ReturnsCorrectGenreCounts()
        {
            // Arrange
            var books = GetBooks();

            // Act
            var result = BookQueries.BooksByGenre(books);

            // Assert
            Assert.Equal(10, result.Count);

            Assert.Equal(2, result.Single(g => g.Genre == "Fantasy").Count);
            Assert.Equal(2, result.Single(g => g.Genre == "Dystopian").Count);
            Assert.Equal(4, result.Single(g => g.Genre == "Science Fiction").Count);
            Assert.Equal(2, result.Single(g => g.Genre == "Classic").Count);
        }

        // Oldest book
        [Fact]
        public void OldestBook_ReturnsBookWithEarliestPublicationYear()
        {
            // Arrange
            var books = GetBooks();

            // Act
            var result = BookQueries.OldestBook(books);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("The Great Gatsby", result.Title);
            Assert.Equal(1925, result.Year);
        }

        // Books published after a given year
        [Fact]
        public void BooksAfterYear_ReturnsBooksAfter2010SortedByTitle()
        {
            // Arrange
            var books = GetBooks();

            // Act
            var result = BookQueries.BooksAfterYear(books, 2010);

            // Assert
            Assert.Equal(5, result.Count);

            Assert.Equal(
                new[]
                {
                    "Atomic Habits",
                    "Educated",
                    "Project Hail Mary",
                    "The Fault in Our Stars",
                    "The Martian"
                },
                result.Select(b => b.Title));

            Assert.All(result, book => Assert.True(book.Year > 2010));
        }

        // Empty library edge case
        [Fact]
        public void OldestBook_ReturnsNullWhenLibraryIsEmpty()
        {
            // Arrange
            var books = new List<Book>();

            // Act
            var result = BookQueries.OldestBook(books);

            // Assert
            Assert.Null(result);
        }
    }
}
