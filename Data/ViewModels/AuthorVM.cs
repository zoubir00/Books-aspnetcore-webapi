namespace My_Books.Data.ViewModels
{
    public class AuthorVM
    {
        public string FullName { get; set; }
    }
    public class AuthorwithBooksVM
    {
        public string FullName { get; set; }
        public List<string> BooksTitle { get; set; }
    }
}
