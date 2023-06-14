using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Books.Data.Services;
using My_Books.Data.ViewModels;

namespace My_Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublisherService _service;

        public PublishersController(PublisherService service)
        {
            _service = service;
        }

        //Post: AddPublisher

        [HttpPost("Add-publisher")]
        public IActionResult AddPublisher(PublisherVM publisher)
        {
            _service.AddPublisher(publisher);
            return Ok();
        }
    }
}
