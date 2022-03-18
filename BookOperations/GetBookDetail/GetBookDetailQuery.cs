using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;
namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public int bookID {get; set;}
        
        public GetBookDetailQuery(BookStoreDbContext dbContext)
        {
            _dbContext=dbContext;
        }
            public BookDetailViewModel Handler()
            {
                
                var book = _dbContext.Books.Where(book=>book.Id==bookID).SingleOrDefault();
                if (book is null)
                {
                    throw new InvalidOperationException("Aradığınız IDye sahip kitap bulunamadı!");
                }
                BookDetailViewModel vm = new BookDetailViewModel();
                vm.Title= book.Title;
                vm.PageCount= book.PageCount;
                vm.PublishDate=book.PublishDate.Date.ToString("dd/MMM/yyyy");
                vm.Genre=((GenreEnum)book.GenreId).ToString();
                return vm;
            }
        
        public class BookDetailViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }

        }
            
    }
}