using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public CreateBookModel Model { get; set; }

        public CreateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handler()
        {
            var book = _dbContext.Books.SingleOrDefault(x=> x.Title == Model.Title);
            //BookList.Contains(newBook);
            if (book is not null) // != bu şekilde de yapabilirsin
                throw new InvalidOperationException("Eklemeye çalıştığınız kitap zaten mevcut!"); // eğer aynı kitaptan önceden varsa tekrardan eklemeyiz ve BadRequest döndürüp işlemi yapmayız.
            book= new Book();
            book.Title=Model.Title;
            book.GenreId=Model.GenreId;
            book.PageCount=Model.PageCount;
            book.PublishDate=Model.PublishDate;
            _dbContext.Books.Add(book); //kitaptan yoksa işlemi yapıp
            _dbContext.SaveChanges();
        }        
        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}