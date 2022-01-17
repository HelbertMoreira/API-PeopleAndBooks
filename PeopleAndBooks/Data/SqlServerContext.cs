using Microsoft.EntityFrameworkCore;
using PeopleAndBooks.Model;

namespace PeopleAndBooks.Data
{
    public class SqlServerContext : DbContext // Classe que herda da DBContext
    {

        
        public SqlServerContext() { }
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options) { }
        

        public DbSet<Person> Person { get; set; }
        public DbSet<Book> Book { get; set; }
    }
}
