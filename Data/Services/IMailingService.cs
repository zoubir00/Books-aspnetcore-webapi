namespace My_Books.Data.Services
{
    public interface IMailingService
    {
        Task SendMails(string mailTo, string Subject, string Body/*, IList<IFormFile> attchments*/);
    }
}
