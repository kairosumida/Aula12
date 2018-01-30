using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Aula12.Models
{
    public class Aula12Context : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<StatusUsuario> StatusUsuarios { get; set; }
        public DbSet<Autenticacao> Autenticacoes { get; set; }
    }
}