using PagingWithEntityFramework.Domain;
using PagingWithEntityFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PagingWithEntityFramework.DAL
{
    public class ErrorContext : DbContext
    {
        static ErrorContext()
        {
            System.Data.Entity.Database.SetInitializer<ErrorContext>(null);
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