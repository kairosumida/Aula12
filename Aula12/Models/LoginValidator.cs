using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aula12.Models
{
    public class LoginValidator
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email incorreto")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}