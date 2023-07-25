using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Books.Data;
using My_Books.Data.Interface;
using My_Books.Data.Models;
using My_Books.Data.Services;
using My_Books.Data.ViewModels;

namespace My_Books.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class BookFilesController : ControllerBase
    {
        private readonly IBlobStorageService _blobStorageService;
        private readonly ApplicationDbContext _context;
        private readonly bookFilesService _service;


        public BookFilesController(IBlobStorageService blobStorageService, ApplicationDbContext context, bookFilesService service)
        {
            _blobStorageService = blobStorageService;
            _context = context;
            _service = service;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> AddBookFiles(BookFilesVM model)
        {
            if (model.blobUrl == null || model.blobUrl.Length == 0)
                return BadRequest("File is required");

            var fileUrl = await _blobStorageService.UploadFileAsync(model.blobUrl);
            var _bookFile = new BookFiles()
            {
                Id = model.Id,
                FileName =model.FileName,  
                blobUrl = fileUrl,
                             
                BookId = model.BookId 
            };
            await _context.BookFiles.AddAsync(_bookFile);
            _context.SaveChanges();

            return Ok();
        }
        [HttpGet("GetFiles")]
        public IActionResult GetBookFiles()
        {
            var bookFiles = _service.GetBookFiles();
            return Ok(bookFiles);
        }
    }
}
