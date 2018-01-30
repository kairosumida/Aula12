using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aula12.Models
{
    public class Autenticacao
    {
        [Key]
        public int AutenticacaoId { get; set; }
        public DateTime Data { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}