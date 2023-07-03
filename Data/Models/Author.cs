namespace My_Books.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ImageURL { get; set; }

        // navigation properties

        public List<Book_Authors> Book_Authors  { get; set; }
    }
}
