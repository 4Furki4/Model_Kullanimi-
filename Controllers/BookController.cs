using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
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
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks ()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result =query.Handler();
            return Ok(result);
        }
        [HttpGet("{id}")] //FromRoute
        public IActionResult GetById (int id)
        {
            
            BookDetailViewModel result;
            GetBookDetailQuery query= new GetBookDetailQuery(_context, _mapper);
            query.bookID=id;
            try
            {
                GetBookDetailQueryValidation validations = new GetBookDetailQueryValidation();
                validations.ValidateAndThrow(query);
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
            CreateBookCommand command = new CreateBookCommand(_context, _mapper); //mapper kullanılacağı için cons metoda eklendi.
            try
            {
                command.Model= newBook;

                CreateBookCommandValidator validations = new CreateBookCommandValidator();
                // ValidationResult result = validations.Validate(command);
                // if (!result.IsValid)
                // {
                //     foreach (var item in result.Errors)
                //     {
                //         System.Console.WriteLine("Özellik:" +item.PropertyName+ "- Error Message: "+ item.ErrorMessage);
                //     }
                // }
                // else
                // {
                //     command.Handler();
                // } TÜM KODLARI ALTTAKİ KODLA YAPTIK ve son kullanıcı da görebiliyor ;)
                validations.ValidateAndThrow(command); // hem valide edecek hem de hata bulursa bunu alttaki catch ifadesindeki ex'e atacak ve orada mesaj zaten dönecek.
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
                UpdateBookCommandValidator validations= new UpdateBookCommandValidator();
                validations.ValidateAndThrow(command);
                command.Handler();
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId=id;
                DeleteBookCommandValidator validations = new DeleteBookCommandValidator();
                validations.ValidateAndThrow(command);
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