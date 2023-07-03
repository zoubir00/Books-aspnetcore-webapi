﻿using Microsoft.AspNetCore.Mvc;
using My_Books.Data.Services;
using My_Books.Data.ViewModels;

namespace My_Books.Controllers
{
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

        // Get 
        [HttpGet("Get-Authors-with-Books/{authorId}")]
        public IActionResult GetAuthorsById(int authorId)
        {
            var response=_service.GetAuthorById(authorId);
            return Ok(response);
        }  
        // Get 
        [HttpGet("AuthorById/{authorId}")]
        public IActionResult AuthorsById(int authorId)
        {
            var response=_service.AuthorById(authorId);
            return Ok(response);
        }

        // Get 
        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            var response = _service.GetAllAuthors();
            return Ok(response);
        }

        // post edit authors
        [HttpPut("Edit/{id}")]
        public IActionResult EditAuthors(int id,[FromBody]AuthorVM author)
        {
            var authorToEdit = _service.EditAuthor(id, author);
            return Ok(authorToEdit);
        }
    }
}
