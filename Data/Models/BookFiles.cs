namespace My_Books.Data.Models
{
    public class BookFiles
    {
        public int Id { get; set; }
        public string blobUrl { get; set; }
        public string FileName { get; set; }

        // nvigation
        public int BookId { get; set; }
        public Book book { get; set; }
    }
}
