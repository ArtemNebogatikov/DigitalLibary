namespace DigitalLibary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserRepository userRepository = new UserRepository();
            BookRepository bookRepository = new BookRepository();
            using (var db = new AppContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var user1 = new User { Name = "A", Email = "gmail1@gmail.com" };
                var user2 = new User { Name = "B", Email = "gmail2@gmail.com" };
                var user3 = new User { Name = "C", Email = "gmail3@gmail.com" };

                var book1 = new Book { Title = "Дюна", Genre = "фантастика", Author = "Фрэнк Герберт", YearOfIssue = new DateTime(1963, 01, 01) };
                var book2 = new Book { Title = "Гарри Поттер", Genre = "фентези", Author = "Джоан Роулинг", YearOfIssue = new DateTime(1997, 01, 01) };
                var book3 = new Book { Title = "Гарри Поттер 2", Genre = "фентези", Author = "Джоан Роулинг", YearOfIssue = new DateTime(1999, 01, 01) };
                var book4 = new Book { Title = "Сказать жизни Да", Genre = "Автобиография", Author = "Виктор Франкл", YearOfIssue = new DateTime(1946, 01, 01) };

                user1.Books.AddRange(new[] { book1, book2, book3 });
                user2.Books.AddRange(new[] { book2, book3, book4 });
                user3.Books.AddRange(new[] { book3, book4 });
                db.Book.AddRange(new[] { book1, book2, book3, book4 });
                db.User.AddRange(new[] { user1, user2, user3 });
                db.SaveChanges();
            };
            Console.WriteLine("Количество книг Джоан Роулинг: {0}", bookRepository.GetCountBooksFromAuthor("джоан роулинг"));
            Console.WriteLine("Количество книг у пользователя B на руках: {0}", userRepository.GetCountBooksFromUser("b"));
            var lastBook = bookRepository.GetBookLastRealease();
            Console.WriteLine(lastBook.Title + lastBook.Genre + lastBook.YearOfIssue);
            bookRepository.GetAllBookByAsc();
            bookRepository.GetAllBookByYearDesc();
            Console.ReadLine();
        }
    }
}