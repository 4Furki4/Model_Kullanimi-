using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;
namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookID { get; set; }
        public UpdateBookModel Model { get; set; }

        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handler()
        {
            var book = _dbContext.Books.SingleOrDefault(x=> x.Id == BookID);
            if (book is null)
                throw new InvalidCastException("Güncellemek istediğiniz kitap bulunamadı !"); //eğer girilen id'yle uyuşan kitap yoksa güncellenecek kitap bulunamamıştır demektir ve Bad Request(400 kodlu) hata döndürülecek.

            book.GenreId= Model.GenreId != default ? Model.GenreId : book.GenreId; // bir kitap alınacağı için tüm bilgiler FromBody ile alınacak lakin
            //book.PageCount= uptatedBook.PageCount != default ? uptatedBook.PageCount : book.PageCount; //değişmeyen değerler olursa bunları güncellenmeden önceki kitaptan
            book.Title= Model.Title != default ? Model.Title :book.Title; //buralarda yazılan gibi if-else yapısıyla propertylere atanacak.
            //book.PublishDate= uptatedBook.PublishDate != default ? uptatedBook.PublishDate :book.PublishDate;
            _dbContext.SaveChanges();//database kullanıldığı için değişiklikler kaydedilmeli.
        }

        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            // public int PageCount { get; set; }
            // public string PublishDate { get; set; }
            

        }
    }
}