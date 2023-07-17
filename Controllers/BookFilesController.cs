using Microsoft.AspNetCore.Mvc;
using My_Books.Data;
using My_Books.Data.Interface;
using My_Books.Data.Models;
using My_Books.Data.ViewModels;

namespace My_Books.Controllers
{
    public class BookFilesController : Controller
    {
        private readonly IBlobStorageService _blobStorageService;
        private readonly ApplicationDbContext _context;


        public BookFilesController(IBlobStorageService blobStorageService, ApplicationDbContext context)
        {
            _blobStorageService = blobStorageService;
            _context = context;
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
    }
}
