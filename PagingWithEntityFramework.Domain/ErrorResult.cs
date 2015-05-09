using System.Collections.Generic;
using PagingWithEntityFramework.Domain.Entities;

namespace PagingWithEntityFramework.Domain
{
    public class ErrorResult
    {
        public int TotalLines { get; set; }

        public IEnumerable<Error> Errors { get; set; }
    }
}
