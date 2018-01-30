﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aula12.Models
{
    public class StatusUsuario
    {
        [Key]
        public int StatusUsuarioId { get; set; }
        [DisplayName("Status")]
        public string NomeStatus { get; set; }
    }
}