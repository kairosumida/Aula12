using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Aula12.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email incorreto")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime UltimoAcesso { get; set; } = DateTime.Now;

        public int StatusUsuarioId { get; set; } = 1;
        public virtual StatusUsuario StatusUsuario {get;set;}

        #region(Não pertence ao BD)
        [NotMapped]
        [DataType(DataType.Password)]
        public string SenhaAntiga { get; set; }
        [NotMapped]
        [DataType(DataType.Password)]
        public string SenhaRepete { get; set; }
        [NotMapped]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; }
        /* Não da certo pois quando vai atualizar, precisa ser o mesmo email, causando problemas
        [NotMapped]
        public string EmailBanco {
            get {
                Aula12Context db = new Aula12Context();
                if(db.Usuarios.FirstOrDefault(x => x.Email == this.Email) == null)
                {
                    return Email;
                }
                else
                {
                    return "";
                }
            }
        }
        */
        #endregion
    }
}