using System.Data.Entity;
using System.Linq;
using PagingWithEntityFramework.Domain.Entities;

namespace PagingWithEntityFramework.DAL
{
    public class ErrorContext : DbContext
    {
        static ErrorContext()
        {
            Database.SetInitializer<ErrorContext>(null);
        }

        public ErrorContext()
            : base("LocalConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Error> Errors { get; set; }

        public virtual IQueryable<Error> FindAllErrors()
        {
            return Errors.Select(e => e);
        }
    }
}