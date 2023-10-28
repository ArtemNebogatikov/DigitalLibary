namespace DigitalLibary
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? YearOfIssue { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}
