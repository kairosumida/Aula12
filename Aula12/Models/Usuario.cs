using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[*+\\/|!\"£$%^&*()#[\\]@~'?><,.=-_]).{6,10}", ErrorMessage = "a senha deve ter de 6 a 10 caracteres e deve conter números, caracteres normais e especiais")]
        public string Senha { get; set; }
        [DisplayName("Data de Cadastro")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        [DisplayName("Último Acesso")]
        public DateTime UltimoAcesso { get; set; } = DateTime.Now;

        public int StatusUsuarioId { get; set; } = 1;
        public virtual StatusUsuario StatusUsuario {get;set;}

        #region(Não pertence ao BD)
        [NotMapped]
        [DataType(DataType.Password)]
        [DisplayName("Senha antiga")]
        public string SenhaAntiga { get; set; }
        [NotMapped]
        [DataType(DataType.Password)]
        [DisplayName("Repetir Senha")]
        public string SenhaRepete { get; set; }
        [NotMapped]
        [DisplayName("Nova Senha")]
        [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[*+\\/|!\"£$%^&*()#[\\]@~'?><,.=-_]).{6,10}", ErrorMessage = "a senha deve ter de 6 a 10 caracteres e deve conter números, caracteres normais e especiais")]
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