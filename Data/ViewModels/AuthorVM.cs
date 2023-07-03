namespace My_Books.Data.ViewModels
{
    public class AuthorVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ImageURL { get; set; }
    }
    public class AuthorwithBooksVM
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string ImageURL { get; set; }
        public List<string> BooksTitle { get; set; }
    }
}
