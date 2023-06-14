namespace My_Books.Data.Models
{
    public class Book_Authors
    {
        public int Id { get; set; }

        public int bookId { get; set; }
        public Book Book { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
