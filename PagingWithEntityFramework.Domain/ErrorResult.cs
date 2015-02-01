using PagingWithEntityFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagingWithEntityFramework.Domain
{
    public class ErrorResult
    {
        public int TotalLines { get; set; }

        public IEnumerable<Error> Errors { get; set; }
    }
}
