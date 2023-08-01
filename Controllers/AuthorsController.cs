using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Books.Data.Services;
using My_Books.Data.ViewModels;
using System.IO;
using System.Text;
using System.Xml.Serialization;
//using System.Reflection.Metadata;


namespace My_Books.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _service;

        public AuthorsController(AuthorService service)
        {
            _service = service;
        }

        [HttpPost("Add-Author")]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            _service.AddAuthor(author);
            return Ok();
        }

        [AllowAnonymous]
        // Get 
        [HttpGet("Get-Authors-with-Books/{authorId}")]
        public IActionResult GetAuthorsById(int authorId)
        {
            var response = _service.GetAuthorById(authorId);
            return Ok(response);
        }

        [AllowAnonymous]
        // Get 
        [HttpGet("AuthorById/{authorId}")]
        public IActionResult AuthorsById(int authorId)
        {
            var response = _service.AuthorById(authorId);
            return Ok(response);
        }

        [AllowAnonymous]
        // Get 
        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            var response = _service.GetAllAuthors();
            return Ok(response);
        }


        // post edit authors
        [HttpPut("Edit/{id}")]
        public IActionResult EditAuthors(int id, [FromBody] AuthorVM author)
        {
            var authorToEdit = _service.EditAuthor(id, author);
            return Ok(authorToEdit);
        }

        // PDF
        [HttpGet("PdfAuthors")]
        [AllowAnonymous]
        public IActionResult GetAuthorPDF()
        {
            try
            {
                var _authors = _service.GetAllAuthors();

                var document = new Document(PageSize.A4, 50, 50, 25, 25);
                var memoryStream = new MemoryStream();
                var writer = PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                // Ajoutez le contenu du PDF
                var font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED, 12);
                var paragraph = new Paragraph("Author :", font);
                document.Add(paragraph);

                foreach (var author in _authors)
                {
                    paragraph = new Paragraph($"ID: {author.Id}, Name: {author.FullName}, Image URL: {author.ImageURL}", font);
                    document.Add(paragraph);
                }

                //document.Close();
                Random random = new Random();
                string fileName = $"author{random.Next()}.pdf";
                string contentType = "application/pdf";
                //Retourner le conteu sous forme fichier
                memoryStream.Position = 0;
                return File(memoryStream, contentType, fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }

   
}
