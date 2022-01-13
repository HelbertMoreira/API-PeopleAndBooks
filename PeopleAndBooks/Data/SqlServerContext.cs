using Microsoft.EntityFrameworkCore;
using PeopleAndBooks.Model;

namespace PeopleAndBooks.Data
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext() { }
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options) { }

        public DbSet<Person> Person { get; set; }
    }
}
