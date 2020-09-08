﻿using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace MobileApp.Models.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

       
    }
}