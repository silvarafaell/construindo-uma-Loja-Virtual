﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Imagem
    {
        public int Id { get; set; }
        public string Caminho { get; set; }

        //Banco de dados
        public int ProdutoId { get; set; }

        //POO
        [ForeignKey("ProdutoId")]
        public Produto Produto { get; set; }
    }
}
