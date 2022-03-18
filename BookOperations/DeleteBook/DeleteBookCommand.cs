using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;
namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbContext;

        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string Title { get; set; }
        public void Handler()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title ==Title );
            if(book is null)
                throw new InvalidOperationException("Silmek istediğiniz kitap bulunamadı!");
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
        
    }
}