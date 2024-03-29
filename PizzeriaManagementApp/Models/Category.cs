﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaManagementApp.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name="Category name")]
        public string Name { get; set; }
    }
}
