using Microsoft.EntityFrameworkCore;
namespace WebApi.DBOperations
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options){}
        public DbSet<Book>Books{get;set;} // DBdeki nesnelerle koddaki nesneler arasında köprüyu kurmak için Book, DBdeki books'un replikası
        
    }
}