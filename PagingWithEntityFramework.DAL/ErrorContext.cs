using System.Data.Entity;
using System.Linq;
using PagingWithEntityFramework.Domain.Entities;

namespace PagingWithEntityFramework.DAL
{
    public class ErrorContext : DbContext
    {
        public DbSet<Error> Errors { get; set; }

        static ErrorContext()
        {
            Database.SetInitializer<ErrorContext>(null);
        }

        public ErrorContext()
            : base("LocalConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        /// <summary>
        /// Don't forget to call .ToList()
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<Error> FindAllErrors()
        {
            return Errors.Select(e => e);
        }
    }
}