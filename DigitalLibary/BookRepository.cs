using static System.Reflection.Metadata.BlobBuilder;

namespace DigitalLibary
{
    public class BookRepository
    {
        public List<Book> SelectAll()
        {
            List<Book> books = new List<Book>();
            using (var db = new AppContext())
            {
                books = db.Book.ToList();
                foreach (var book in books)
                {
                    Console.WriteLine(book.Id + " " + book.Title + " " + book.YearOfIssue);
                }
            }
            return books;
        }

        public Book SelectFromId(int idBook)
        {
            Book book = new Book();
            using (var db = new AppContext())
            {
                book = db.Book.Find(idBook);
            }
            if (book == null)
            {
                Console.WriteLine("Книга не найдена");
            }
            else
            {
                Console.WriteLine(book.Title + " " + book.Id + " " + book.YearOfIssue);
            }
            return book;
        }
        public void InsertInDb(Book book)
        {
            if (book != null)
            {
                using (var db = new AppContext())
                {
                    db.Book.Add(book);
                    db.SaveChanges();
                }
            }
            else
            {
                Console.WriteLine("Данные указаны некорректно");
            }
        }
        public void DeleteFromDb(Book book)
        {
            try
            {
                using (var db = new AppContext())
                {
                    db.Remove(book);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Книга не удалена");
                Console.WriteLine(ex.Message);
            }

        }

        public void UpdateFromId(Book book)
        {
            try
            {
                using (var db = new AppContext())
                {
                    var BookFromDb = db.Book.Find(book.Id);
                    if (BookFromDb == null)
                    {
                        Console.WriteLine("Не удалось найти книгу");
                    }
                    else
                    {
                        BookFromDb.Title = book.Title;
                        BookFromDb.YearOfIssue = book.YearOfIssue;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось обновить данные о книге\n" + ex.Message);
            }
        }

        public List<Book> GetBookFromGenreAndReleaseYear(string genre, DateTime? releaseYearStart, DateTime? realiseDateEnd)
        {
            List<Book> books = new List<Book>();
            try
            {
                if (genre != null && releaseYearStart != null && realiseDateEnd != null)
                {
                    using (var db = new AppContext())
                    {
                        books = db.Book.Where(b => b.Genre == genre && b.YearOfIssue >= releaseYearStart && b.YearOfIssue <= realiseDateEnd).ToList();
                    }
                }
                else
                {
                    Console.WriteLine("Не указано обязательное поле");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Некорректные данные");
            }
            return books;
        }

        public int GetCountBooksFromAuthor(string author)
        {
            int countBooks = 0;
            try
            {
                if (author != null)
                {
                    using (var db = new AppContext())
                    {
                        countBooks = db.Book.Count(b => b.Author.ToLower() == author.ToLower());
                    }
                }
                else
                {
                    Console.WriteLine("Не указано обязательное поле");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Некорректные данные");
            }
            return countBooks;
        }
        public int GetCountBooksFromGenre(string genre)
        {
            int countBooks = 0;
            try
            {
                if (genre != null)
                {
                    using (var db = new AppContext())
                    {
                        countBooks = db.Book.Count(b => b.Author.ToLower() == genre.ToLower());
                    }
                }
                else
                {
                    Console.WriteLine("Не указано обязательное поле");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Некорректные данные");
            }
            return countBooks;
        }
        public bool isBookFromAuthorAndTitle(string author, string title)
        {
            bool isBook = false;
            try
            {
                if (author != null && title != null)
                {
                    using (var db = new AppContext())
                    {
                        isBook = db.Book.Any(b => b.Author.ToLower() == author.ToLower() && b.Title.ToLower() == author.ToLower());
                    }
                }
                else
                {
                    Console.WriteLine("Не указано обязательное поле");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Некорректные данные");
            }
            return isBook;
        }

    }
}
