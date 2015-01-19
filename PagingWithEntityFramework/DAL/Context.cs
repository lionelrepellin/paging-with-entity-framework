using PagingWithEntityFramework.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PagingWithEntityFramework.DAL
{
    public class Context : DbContext
    {
        static Context()
        {
            System.Data.Entity.Database.SetInitializer<Context>(null);
        }

        public Context()
            : base("LocalConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Error> Errors { get; set; }

        public int GetTotalNumberOfErrors()
        {
            return this.Database.SqlQuery<int>("SELECT COUNT(0) FROM [log] WITH(NOLOCK)").SingleOrDefault();
        }

        public IEnumerable<Error> GetErrorsByPageIndex(int pageIndex, int linesPerPage)
        {
            return this.Errors.OrderByDescending(e => e.Id).Skip(pageIndex * linesPerPage).Take(linesPerPage).ToList();
        }
    }
}