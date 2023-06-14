using System.Security.Principal;

namespace My_Books.Data.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation Property
        public List<Book> Books { get; set; }
    }
}
