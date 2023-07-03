using My_Books.Data.Models;

namespace My_Books.Account.UserManager
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? Exparedate { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
