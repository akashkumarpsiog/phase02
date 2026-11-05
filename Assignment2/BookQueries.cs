using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement
{
    public record GenreCount(string Genre, int Count);

    public static class BookQueries
    {
        public static List<Book> BooksByAuthor(string authorName, List<Book> library)
        {
            return library
                .Where(b => b.Author == authorName && b.IsAvailable)
                .ToList();
        }

        public static List<GenreCount> BooksByGenre(List<Book> library)
        {
            return library
                .GroupBy(b => b.Genre)
                .Select(group => new GenreCount(
                    group.Key,
                    group.Count()))
                .ToList();
        }

        public static Book? OldestBook(List<Book> library)
        {
            return library
                .OrderBy(b => b.Year)
                .FirstOrDefault();
        }

        public static List<Book> BooksAfterYear(List<Book> library, int year)
        {
            return library
                .Where(b => b.Year > year)
                .OrderBy(b => b.Title)
                .ToList();
        }
    }
}