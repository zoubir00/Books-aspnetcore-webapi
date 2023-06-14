﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Books.Data;
using My_Books.Data.Models;
using My_Books.Data.Services;
using My_Books.Data.ViewModels;

namespace My_Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _service;

        public BooksController(BooksService service)
        {
            _service = service;
        }

        
        //Get: api/Books/GetAllBooks
        [HttpGet("Get-All-Books")]
        public IActionResult GetAllBooks()
        {
            var AllBooks = _service.GetAllBooks();
            return Ok(AllBooks);
        }


        //Get: api/Books/GetBookById
        [HttpGet("get-book-by-id/{bookId}")]
        public IActionResult GetBookById(int bookId)
        {
            var book = _service.GetBookById(bookId);
            return Ok(book);
        }

        // Post: api/Books/AddBook
        [HttpPost]
        public IActionResult AddBook([FromBody] BookVM book)
        {
            _service.Add(book);
            return Ok();
        }

        // put: api/Books/EditBook
        [HttpPut("edit-book-/{Id}")]
        public IActionResult EditBook(int Id, [FromBody] BookVM book)
        {
           var bookupdated= _service.UpdateBook(Id, book);
            return Ok(bookupdated);
        }


        // Post: api/Books/AddBook
        [HttpDelete("delete-book-/{Id}")]
        public IActionResult DeleteBook(int Id)
        {
            _service.DelteBook(Id);
            return Ok();
        }
    }
}
