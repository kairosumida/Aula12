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
        [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[*+\\/|!\"£$%^&*()#[\\]@~'?><,.=-_]).{6,10}", ErrorMessage = "a senha deve ter de 6 a 10 caracteres e deve conter números, caracteres normais e especiais")]
        public string Senha { get; set; }
    }
}