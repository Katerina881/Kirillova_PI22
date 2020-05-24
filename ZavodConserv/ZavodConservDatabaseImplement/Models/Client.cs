﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZavodConservDatabaseImplement.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        public string FIO { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public List<Order> Orders { get; set; }
    }
}
