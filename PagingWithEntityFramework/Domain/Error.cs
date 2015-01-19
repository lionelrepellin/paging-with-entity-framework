using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PagingWithEntityFramework.Domain
{
    [Table("erreur")]
    public class Error
    {
        [Key, Column("id")]
        public int Id { get; set; }

        public int ErrorNumber { get; set; }

        public int ErrorSeverity { get; set; }

        public int ErrorState { get; set; }

        [MaxLength(1000)]
        public string ErrorProcedure { get; set; }

        public int ErrorLine { get; set; }

        [MaxLength(1000)]
        public string ErrorMessage { get; set; }
    }
}