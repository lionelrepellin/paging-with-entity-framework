using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PagingWithEntityFramework.Domain
{
    [Table("log")]
    public class Error
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("date")]
        public DateTime CurrentDate { get; set; }

        [Column("severity")]
        public Severity Level { get; set; }

        [Column("message")]
        public string Stacktrace { get; set; }
    }

    public enum Severity
    {
        NotSoBad,
        Warning,
        Error,
        Fatal
    };
}