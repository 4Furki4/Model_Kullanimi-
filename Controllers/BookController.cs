using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetBookDetail.GetBookDetailQuery;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.AddControllers
{
    [ApiController] // response döndeceğini belirtir
    [Route("[controller]s")] // gelen isteğin hangi resource karşılayacak bilgisi
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController (BookStoreDbContext context)
        {
            _context=context;
        }

        [HttpGet]
        public IActionResult GetBooks ()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result =query.Handler();
            return Ok(result);
        }
        [HttpGet("{id}")] //FromRoute
        public IActionResult GetById (int id)
        {
            BookDetailViewModel result;
            GetBookDetailQuery query= new GetBookDetailQuery(_context);
            query.bookID=id;
            try
            {
                result =query.Handler();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result); //IActionResult tipinde Ok mesajını nesneyle birlikte döndürüyoruz.
        }
        /*[HttpGet]
        public Book Get ([FromQuery] string id)
        {
            var book = BookList.Where(book=>book.Id==int.Parse(id)).SingleOrDefault();
            return book;
        }*/
        // Post ile yeni kitaplar eklicez
        // Put ile bir kitabın bilgilerini güncelleyeceğiz

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model= newBook;
                command.Handler();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);   
            }
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel uptatedBook)
        {
            
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookID=id;
                command.Model=uptatedBook;
                command.Handler();
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("{title}")]
        public IActionResult DeleteBook(string title)
        {
            
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.Title=title;
                command.Handler();
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }

            return Ok();
        }

    }
}