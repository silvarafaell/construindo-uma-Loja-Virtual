using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;  //para usar as regras de validação

namespace LojaVirtual.Models
{
    public class Contato
    {
        [Required] //deixa o campo obrigatorio
        [MinLength(4)] //quantidade minima de caracteres
        public string Nome { get; set; }

        [Required] //deixa o campo obrigatorio
        [EmailAddress] //validacao especifica do email
        public string Email { get; set; }

        [Required] //deixa o campo obrigatorio
        [MinLength(10)]
        [MaxLength(1000)]
        public string Texto { get; set; }

    }
}
