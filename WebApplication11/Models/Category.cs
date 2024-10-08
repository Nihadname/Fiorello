﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using WebApplication11.Models.Common;

namespace WebApplication11.Models
{
    public class Category:BaseEntity
    {
        [MaxLength(40)]
        [Required]
        public string Name { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }
      public  List<Product> products { get; set; }    }
}
