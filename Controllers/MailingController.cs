using Microsoft.AspNetCore.Mvc;
using My_Books.Data.Services;
using My_Books.DTO;

namespace My_Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailingController : ControllerBase
    {
        private readonly IMailingService _mailingService;

        public MailingController(IMailingService mailingService)
        {
            _mailingService = mailingService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendMail([FromForm] mailingDto dto)
        {
            await _mailingService.SendMails(dto.ToEmail, dto.Subject, dto.Body/*, dto.attachments*/);
            return Ok();
        }
    }
}
