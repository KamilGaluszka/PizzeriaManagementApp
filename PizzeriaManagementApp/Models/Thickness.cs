﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaManagementApp.Models
{
    public class Thickness
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
