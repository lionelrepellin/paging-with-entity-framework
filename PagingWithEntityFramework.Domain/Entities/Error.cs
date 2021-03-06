﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PagingWithEntityFramework.Domain.Entities
{
    [Table("log")]
    public class Error
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("date")]
        public DateTime CurrentDate { get; set; }

        [Column("server")]
        public string ServerName { get; set; }

        [Column("severity")]
        public string ErrorLevel { get; set; }

        [Column("message")]
        public string Stacktrace { get; set; }
    }    
}