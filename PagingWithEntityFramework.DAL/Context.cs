﻿using PagingWithEntityFramework.Domain;
using PagingWithEntityFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PagingWithEntityFramework.DAL
{
    public class Context : DbContext, IDisposable
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

        #region Queries without search criteria

        public int FindTotalNumberOfErrors()
        {
            // inline query
            return this.Database.SqlQuery<int>("SELECT COUNT(0) FROM [log] WITH(NOLOCK)").SingleOrDefault();
        }

        public IEnumerable<Error> FindErrorsByPageIndex(int pageIndex, int linesPerPage)
        {
            return this.Errors.OrderByDescending(e => e.Id).Skip(pageIndex * linesPerPage).Take(linesPerPage).ToList();
        }

        #endregion

        #region Queries with search criteria

        public IEnumerable<Error> FindErrorsByPageIndexAndCriteria(int pageIndex, int linesPerPage, SearchCriteria criteria)
        {
            var result = (from e in Errors
                          where (string.IsNullOrEmpty(criteria.StackTrace) || e.Stacktrace.Contains(criteria.StackTrace))
                          && (string.IsNullOrEmpty(criteria.ServerName) || e.ServerName.Contains(criteria.ServerName))
                          && (string.IsNullOrEmpty(criteria.Severity) || e.ErrorLevel.Contains(criteria.Severity))
                          orderby e.Id descending                          
                          select e).Skip(pageIndex * linesPerPage).Take(linesPerPage).ToList();

            return result;
        }

        public int FindTotalNumberOfErrorsWithCriteria(SearchCriteria criteria)
        {
            var result = (from e in Errors
                          where (string.IsNullOrEmpty(criteria.StackTrace) || e.Stacktrace.Contains(criteria.StackTrace))
                          && (string.IsNullOrEmpty(criteria.ServerName) || e.ServerName.Contains(criteria.ServerName))
                          && (string.IsNullOrEmpty(criteria.Severity) || e.ErrorLevel.Contains(criteria.Severity))
                          orderby e.Id descending
                          select e).Count();

            return result;
        }

        #endregion

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}