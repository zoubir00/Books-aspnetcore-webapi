using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Books.Data.Services;
using My_Books.Data.ViewModels;

namespace My_Books.Controllers
{
    [Authorize]
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


        // get all publishers
        [AllowAnonymous]
        [HttpGet("getPublishres")]
        public IActionResult GetPublishers()
        {
            var publishers = _service.GetPublishers();
            return Ok(publishers);
        }

        //Get:
        [AllowAnonymous]
        [HttpGet("GetPublisher-Data/{publisherId}")]
        public IActionResult GetPublisherData(int publisherId)
        {
           var publisherData= _service.GetPublisherById(publisherId);
            return Ok(publisherData);
        }

        //Delete:
        [HttpDelete("Delete-Publisher-Data/{publisherId}")]
        public IActionResult DeletePublisherData(int publisherId)
        {
            _service.DeletePublisher(publisherId);
            return Ok();
        }
    }
}
