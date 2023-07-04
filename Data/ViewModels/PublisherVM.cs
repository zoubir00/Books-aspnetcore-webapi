namespace My_Books.Data.ViewModels
{
    public class PublisherVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class PublisherwithBooksAndAuthorVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookAuthorVM> BookAuthors { get; set; }


    }
    public class BookAuthorVM
    {
        public string BookName { get; set; }
        public List<string> BookAuthors { get; set; }

    }
}
