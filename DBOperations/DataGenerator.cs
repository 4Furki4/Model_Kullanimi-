using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialise(IServiceProvider serviceProvider) // Program.csye bağlanacak, programcsnin serviseproviderı bunu çalıştıracak ve uygulama ilk ayağa kalktığında çalışan bir yapı olacak
        {
            using (var context= new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }


                context.Books.AddRange(
                new Book
                {
                //Id=1,
                Title= "Lean Startup",
                GenreId=1, // Personal Growth
                PageCount= 200,
                PublishDate= new DateTime(2001, 11,07)
                },
                new Book{
                //    Id=2,
                    Title="Brave New World",
                    GenreId=2, //Science Fiction
                    PageCount= 180,
                    PublishDate= new DateTime(1932,6,12)

                },
                new Book{
                  //  Id=3,
                    Title="Our Inner Ape",
                    GenreId=2, //Popular Science
                    PageCount= 250,
                    PublishDate= new DateTime(2016,1,30)
                }
                                        );
                context.SaveChanges();
            }

        }
    }
}